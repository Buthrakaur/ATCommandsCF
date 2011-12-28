using System;

using System.Collections.Generic;
using System.Text;
using ATTestCF.Sms;

namespace ATTestCF.Tests
{
	public class ATCommunicator_Tests
	{
		public void IsSuccessResponse()
		{
			Assert.True(ATCommunicator.IsSuccessResponse("AT+CMGF=0\r\r\nOK\r\n"));
			Assert.True(ATCommunicator.IsSuccessResponse("OK"));
			Assert.False(ATCommunicator.IsSuccessResponse("AT+CMGW=26,0\r\r\nERROR\r\n"));
			Assert.False(ATCommunicator.IsSuccessResponse("AT+CMGW=26,0\r\r\n+CMS ERROR: 322\r\n"));
			Assert.False(ATCommunicator.IsSuccessResponse(""));
			Assert.False(ATCommunicator.IsSuccessResponse("asldfasdlkdsaj"));
		}

		public void ParseResponse()
		{
			Assert.Equal("OK", ATCommunicator.ParseResponse("AT+CMGF=0\r\r\nOK\r\n"));
			Assert.Equal(">", ATCommunicator.ParseResponse("AT+CMGS=25\r\r\n>"));
			Assert.Equal("00110000910000AA0CC8F71D14969741F977FD07" + ATCommunicator.CtrlZ, ATCommunicator.ParseResponse("00110000910000AA0CC8F71D14969741F977FD07" + ATCommunicator.CtrlZ));
			Assert.Equal("+CMGW: 5",
			             ATCommunicator.ParseResponse(
			             	"AT+CMGW=42,2\r07915892000000F001000B915892214365F7000021493A283D0795C3F33C88FE06CDCB6E32885EC6D341EDF27C1E3E97E72E" +
			             	ATCommunicator.CtrlZ
			             	+ "\r\r\n+CMGW: 5\r\n"));

			Assert.Equal("+CMGW: 19",
			             ATCommunicator.ParseResponse("0011000C912410325476980000AA05F4F29C1E03" +
			                                          ATCommunicator.CtrlZ +
			                                          "\r\n+CMGW: 19\r\n\r\nOK\r\n"));
		}
	}
}
