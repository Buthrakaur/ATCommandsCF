using System;
using System.Collections.Generic;
using System.Text;

namespace ATTestCF.Sms
{
	public static class PduEncoder
	{
		public static IEnumerable<string> Encode(Sms sms)
		{
			if (sms.Message.Length <= 160)
			{
				yield return EncodeShortMessage(sms.Recipient, sms.Message);
			}
			else
			{
				var offset = 0;
				byte idx = 0;
				var parts = (byte)Math.Ceiling(sms.Message.Length/154f);
				while (offset < sms.Message.Length)
				{
					var messagePart = sms.Message.Substring(offset, Math.Min(154, sms.Message.Length-offset));
					yield return EncodeMultipartMessage(sms.Recipient, messagePart, idx, parts);
					offset += 154;
					idx++;
				}
			}
		}

		private static string EncodeMultipartMessage(string recipient, string messagePart, byte idx, byte parts)
		{
			var sb = new StringBuilder();
			sb.Append("00"); //no SCMS info
			sb.Append("41"); //PDU type | 0x40 for multipart => So for an SMS-SUBMIT with UDH present we set the PDU type to 0×41.
			sb.Append(ToHexStr(idx)); //message reference - incremented in multipart messages
			sb.Append(EncodePhoneNumber(recipient));
			sb.Append("00"); //Protocol identifier (Short Message Type 0))
			sb.Append("00"); //7bit encoding
			sb.Append(ToHexStr((byte)(messagePart.Length + 6)));//Total payload length in septets (including UDH which is 6)
			sb.Append("05"); //User data header length - always 5
			//UDH:
			//IEI = 0×00 - Information element identifier for a concatenated short message
			sb.Append("00");
			//IEDL = 0×03 - Information element data length
			sb.Append("03");
			//Ref nr = 0×00 - A reference number (must be the same for all parts of the same larger messages)
			sb.Append("00");
			//Total  = 0×03 - count of parts of this message
			sb.Append(ToHexStr(parts));
			//This part = 0×01 - this part index
			sb.Append(ToHexStr((byte)(idx+1)));
			//7bit message
			GetMessageBytesAsHexString(messagePart, sb, true);

			return sb.ToString().ToUpper();
		}

		private static string EncodeShortMessage(string recipient, string message)
		{
			var sb = new StringBuilder();
			sb.Append("00"); //no SCMS info
			sb.Append("11"); //PDU type | 0x40 for multipart => So for an SMS-SUBMIT with UDH present we set the PDU type to 0×41.
			sb.Append("00"); //message reference - incremented in multipart messages
			sb.Append(EncodePhoneNumber(recipient));
			sb.Append("00"); //Protocol identifier (Short Message Type 0))
			sb.Append("00"); //7bit encoding
			sb.Append("AA"); //validity period
			sb.Append(ToHexStr((byte) message.Length)); //message length
			//7bit message
			GetMessageBytesAsHexString(message, sb, false);

			return sb.ToString().ToUpper();
		}

		private static void GetMessageBytesAsHexString(string message, StringBuilder sb, bool withPadding)
		{
			EnumerableHelper.ForEach(GetMessageBytes(message, withPadding), b => sb.Append(ToHexStr(b)));
		}

		public static string GetMessageBytesAsHexString(string message, bool withPadding)
		{
			var sb = new StringBuilder();
			GetMessageBytesAsHexString(message, sb, withPadding);
			return sb.ToString().ToUpper();
		}

		/// <summary>
		/// 7bit encoding
		/// </summary>
		/// <param name="message"></param>
		/// <param name="withPadding">used for long messages with UDH header</param>
		/// <returns></returns>
		private static byte[] GetMessageBytes(string message, bool withPadding)
		{
			//Get bytes for this SMS string
			byte[] smsBytes = Encoding.ASCII.GetBytes(message);
			byte mask = 0;
			byte shiftedMask = 0;
			byte bitsRequired = 0;
			byte invertMask = 0;
			int encodedMessageIndex = 0;
			int shiftCount = withPadding ? -1 : 0;
			//calculating new encoded message length
			int arrayLength = (int)(message.Length - Math.Ceiling(((double)message.Length / 8f)) + 1);
			byte[] encodedMessage = new byte[arrayLength];
			int i = 0;
			for (i = 0; i < smsBytes.Length; i++)
			{
				mask = (byte)((mask * 2) + 1);
				if (i < smsBytes.Length-1)
				{
					bitsRequired = (byte)(smsBytes[i + 1] & mask);
					shiftedMask = (byte)(bitsRequired << 7 - shiftCount);
					encodedMessage[encodedMessageIndex] =
						(byte) (shiftCount < 0
						        	? smsBytes[i] << (-1*shiftCount)
						        	: smsBytes[i] >> shiftCount);
					encodedMessage[encodedMessageIndex] = (byte)(encodedMessage[encodedMessageIndex] | shiftedMask);
				}
				else
				{
					//last byte
					encodedMessage[encodedMessageIndex] = (byte)(smsBytes[i] >> shiftCount);
				}
				encodedMessageIndex++;
				shiftCount++;
				//reseting the cycle when 1 ASCII is completely packed
				if (shiftCount == 7)
				{
					i++;
					mask = 0;
					shiftCount = 0;
				}
			}
			//Encoded Message
			return encodedMessage;
		}

		private static string EncodePhoneNumber(string recipient)
		{
			var phoneNumber = recipient;
			var isInternational = phoneNumber.StartsWith("+");

			if (isInternational)
				phoneNumber = phoneNumber.Remove(0, 1);

			var header = (phoneNumber.Length << 8) + 0x81 | (isInternational ? 0x10 : 0x20);

			if (phoneNumber.Length % 2 == 1)
				phoneNumber = phoneNumber.PadRight(phoneNumber.Length + 1, 'F');

			phoneNumber = ReverseBits(phoneNumber);

			return ToHexStr(header) + phoneNumber;
		}

		private static string ReverseBits(string phoneNumber)
		{
			var result = string.Empty;

			for (var i = 0; i < phoneNumber.Length; i++)
				result = result.Insert(i % 2 == 0 ? i : i - 1, phoneNumber[i].ToString());

			return result;
		}

		private static string ToHexStr(byte num)
		{
			return Convert.ToString(num, 16).PadLeft(2, '0');
		}

		private static string ToHexStr(int header)
		{
			return Convert.ToString(header, 16).PadLeft(4, '0');
		}
	}
}