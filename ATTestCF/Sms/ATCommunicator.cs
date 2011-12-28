using System;

using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ATTestCF.Sms
{
	public class ATCommunicator
	{
		private readonly SerialPort port;
		private readonly Action<string> sentData;
		private readonly Action<string> receivedData;
		public const char CtrlZ = (char)0x1A;

		public ATCommunicator(SerialPort port, Action<string> sentData, Action<string> receivedData)
		{
			this.port = port;
			this.sentData = sentData;
			this.receivedData = receivedData;
		}

		public void At()
		{
			EnsureSuccessResponse(GetATCommandResponse("AT\r"));
		}

		public void Cmgf(int type)
		{
			EnsureSuccessResponse(GetATCommandResponse(string.Format("AT+CMGF={0}\r", type)));
		}

		public enum CmgwStoreLocation
		{
			//0. It refers to the message status "received unread".
			ReceivedUnread = 0,

			//1. It refers to the message status "received read".
			ReceivedRead = 1,

			//2. It refers to the message status "stored unsent". This is the default value.
			StoredUnsent = 2,

			//3. It refers to the message status "stored sent".
			StoredSent = 3
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pduData"></param>
		/// <param name="storeLocation"></param>
		/// <returns>index of stored message</returns>
		public int Cmgw(string pduData, CmgwStoreLocation storeLocation)
		{
			var length = GetPduDataLength(pduData);
			var response = ParseResponse(GetATCommandResponse(string.Format("AT+CMGW={0},{1}\r", length, (int)storeLocation)));
			//response should be ">"
			if (response != ">")
			{
				throw new IOException("Didn't get success response from device.");
			}

			response = ParseResponse(GetATCommandResponse(pduData + CtrlZ));
			//response is "+CMGW: 5"
			var m = Regex.Match(response, @"^\+CMGW: (?<idx>\d+)$");
			if (!m.Success || 
				!m.Groups["idx"].Success || 
				string.IsNullOrEmpty(m.Groups["idx"].Value))
			{
				throw new IOException("Didn't get success response from device.");
			}
			try
			{
				return int.Parse(m.Groups["idx"].Value);
			}
			catch
			{
				throw new IOException("Didn't get success response from device.");
			}
		}

		public void CpmsQuery(out CpmsStorageArea[] readAndDelete, out CpmsStorageArea[] sendAndWrite, out CpmsStorageArea[] receivedMessages)
		{
			//+CPMS: ("ME","SM"),("ME","SM"),("MT")
			var response = GetATCommandResponse("AT+CPMS=?\r");
			var m = Regex.Match(response,
			            @"\+CPMS: \((?<1>('[A-Z]+',?)+)\),\((?<2>('[A-Z]+',?)+)\),\((?<3>('[A-Z]+',?)+)\)".Replace("'", "\""));
			if (!m.Success ||
				!m.Groups["1"].Success ||
				!m.Groups["2"].Success ||
				!m.Groups["3"].Success)
			{
				throw new IOException("Didn't get success response from device.");
			}

			readAndDelete = EnumerableHelper.ToArray(ParseCpmsGroup(m.Groups["1"].Value));
			sendAndWrite = EnumerableHelper.ToArray(ParseCpmsGroup(m.Groups["2"].Value));
			receivedMessages = EnumerableHelper.ToArray(ParseCpmsGroup(m.Groups["3"].Value));
		}

		private IEnumerable<CpmsStorageArea> ParseCpmsGroup(string group)
		{
			foreach(Match m in Regex.Matches(group, "[A-Z]+"))
			{
				if (!m.Success) continue;
				CpmsStorageArea value;
				try
				{
					value = (CpmsStorageArea)Enum.Parse(typeof(CpmsStorageArea), m.Value, true);
				}
				catch
				{
					continue;
				}
				yield return value;
			}
		}

		public enum CpmsStorageArea
		{
			/// <summary>
			/// It refers to the message storage area on the SIM card.
			/// </summary>
			SM,

			/// <summary>
			///It refers to the message storage area on the GSM/GPRS modem or mobile phone. Usually its storage space is larger than that of the message storage area on the SIM card.
			/// </summary>
			ME,

			/// <summary>
			/// It refers to all message storage areas associated with the GSM/GPRS modem or mobile phone. For example, suppose a mobile phone can access two message storage areas: "SM" and "ME". The "MT" message storage area refers to the "SM" message storage area and the "ME" message storage area combined together.
			/// </summary>
			MT,

			/// <summary>
			/// It refers to the broadcast message storage area. It is used to store cell broadcast messages.
			/// </summary>
			BM,

			/// <summary>
			/// It refers to the status report message storage area. It is used to store status reports.
			/// </summary>
			SR,

			/// <summary>
			/// It refers to the terminal adaptor message storage area.
			/// </summary>
			TA
		}

		/// <summary>
		/// http://www.developershome.com/sms/cpmsCommand.asp
		/// </summary>
		public void Cpms(CpmsStorageArea readAndDelete, CpmsStorageArea? sendAndWrite, CpmsStorageArea? receivedMessages)
		{
			//AT+CPMS="ME","SM","MT"
			var commands = new List<string> {string.Format("\"{0}\"", readAndDelete)};
			if (sendAndWrite.HasValue)
				commands.Add(string.Format("\"{0}\"", sendAndWrite));
			if (receivedMessages.HasValue)
				commands.Add(string.Format("\"{0}\"", receivedMessages));
			GetATCommandResponse(string.Format("AT+CPMS={0}\r", string.Join(",", commands.ToArray())));
		}

		private static int GetPduDataLength(string pduData)
		{
			return (pduData.Length - 2)/2;
		}

		public void Cmgs(string pduData)
		{
			var length = GetPduDataLength(pduData);
			var response = ParseResponse(GetATCommandResponse(string.Format("AT+CMGS={0}\r", length)));
			//response should be ">"
			if (response != ">")
			{
				throw new IOException("Didn't get success response from device.");
			}
			response = ParseResponse(GetATCommandResponse(pduData + CtrlZ));
			//response should be echo or?
			if (response.IndexOf("ERROR", StringComparison.InvariantCultureIgnoreCase) != -1)
			{
				throw new IOException("Didn't get success response from device.");
			}
		}

		private static void EnsureSuccessResponse(string res)
		{
			if (IsSuccessResponse(res)) return;
			throw new IOException("Didn't get success response from device.");
		}

		public static bool IsSuccessResponse(string res)
		{
			var response = ParseResponse(res);
			if (response == null) return false;
			return response == "OK";
		}

		private static readonly Regex rxResponseWithEcho = new Regex(@"^(?<input>.+)[\r\n]*(?<response>.+)[\r\n]*$");
		private static readonly Regex rxResponseWithEchoAndStatus = new Regex(@"^(?<input>.+)[\r\n]*(?<response>.+)[\r\n]*(?<status>.+)[\r\n]*$");
		private static readonly Regex rxResponseSimple = new Regex(@"^[\r\n]*(?<response>.+)[\r\n]*$");
		public static string ParseResponse(string res)
		{
			var m = rxResponseSimple.Match(res);
			if (!m.Success) m = rxResponseWithEcho.Match(res);
			if (!m.Success) m = rxResponseWithEchoAndStatus.Match(res);
			if (!m.Success) return null;

			var g = m.Groups["response"];
			if (g == null || !g.Success) return null;
			var response = g.Value.Trim();
			return response;
		}

		private string GetATCommandResponse(string command)
		{
			if (sentData != null) sentData(GetPrintableString(command));
			port.Write(command);
			var res = ReadResponse();
			if (receivedData != null) receivedData(GetPrintableString(res));
			Thread.Sleep(1000);
			return res;
		}

		private static string GetPrintableString(string res)
		{
			return res
				.Replace("\r", "<CR>")
				.Replace("\n", "<LF>")
				.Replace("" + CtrlZ, "<Ctrl+Z>");
		}

		private string ReadResponse()
		{
			var attempts = 0;
			while (port.BytesToRead == 0 && attempts < 5 * 10)
			{
				Thread.Sleep(200);
				attempts++;
			}

			var sb = new StringBuilder();
			while (port.BytesToRead > 0)
			{
				var buf = new byte[512];
				var bytesRead = port.Read(buf, 0, buf.Length);
				sb.Append(Encoding.ASCII.GetString(buf, 0, bytesRead));
			}
			return sb.ToString();
		}
	}
}
