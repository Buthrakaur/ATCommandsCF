using System;
using System.Text;

namespace ATTestCF.Sms
{
	public static class PduEncoder
	{
		public static string Encode(Sms sms)
		{
			var sb = new StringBuilder();
			sb.Append("00");//no SCMS info
			sb.Append("11");
			sb.Append("00");
			sb.Append(EncodePhoneNumber(sms.Recipient));
			sb.Append("00"); //Protocol identifier (Short Message Type 0))
			sb.Append("00"); //7bit encoding
			sb.Append("AA"); //validity period
			sb.Append(ToHexStr((byte) sms.Message.Length));//message length
			//7bit message
			EnumerableHelper.ForEach(GetMessageBytes(sms.Message), b => sb.Append(ToHexStr(b)));

			return sb.ToString().ToUpper();
		}

		/// <summary>
		/// 7bit encoding
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		private static byte[] GetMessageBytes(string message)
		{
			//Get bytes for this SMS string
			byte[] smsBytes = Encoding.ASCII.GetBytes(message);
			byte mask = 0;
			byte shiftedMask = 0;
			byte bitsRequired = 0;
			byte invertMask = 0;
			int encodedMessageIndex = 0;
			int shiftCount = 0;
			//calculating new encoded message length
			int arrayLength = (int)(message.Length - Math.Floor(((double)message.Length / 8)));
			byte[] encodedMessage = new byte[arrayLength];
			int i = 0;
			for (i = 0; i < smsBytes.Length; i++)
			{
				mask = (byte)((mask * 2) + 1);
				if (i < smsBytes.Length-1)
				{
					bitsRequired = (byte)(smsBytes[i + 1] & mask);
					shiftedMask = (byte)(bitsRequired << 7 - shiftCount);
					encodedMessage[encodedMessageIndex] = (byte)(smsBytes[i] >> shiftCount);
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