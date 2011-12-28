using System;

using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;

namespace ATTestCF.Sms
{
	public class Phone: IDisposable
	{
		private readonly SerialPort port;
		private readonly ATCommunicator comm;

		public event EventHandler<GenericEventArgs<string>> CommandSent;
		public event EventHandler<GenericEventArgs<string>> ResponseReceived;

		public Phone(string portName)
		{
			port = new SerialPort(portName);
			port.Open();
			if (!port.IsOpen)
			{
				throw new IOException("Unable to open port " + port);
			}
			comm = new ATCommunicator(port,
			                          d =>
			                          	{
			                          		if (CommandSent != null)
			                          			CommandSent(this, new GenericEventArgs<string>(d));
			                          	},
			                          d =>
			                          	{
			                          		if (ResponseReceived != null)
			                          			ResponseReceived(this, new GenericEventArgs<string>(d));
			                          	});
		}

		public void Dispose()
		{
			port.Dispose();
		}

		public void SendTextMessage(Sms sms)
		{
			comm.At();
			comm.Cmgf(0);
			EnumerableHelper.ForEach(PduEncoder.Encode(sms), pdu => comm.Cmgs(pdu));
		}

		public void SaveTextMessage(Sms sms, ATCommunicator.CmgwStoreLocation location)
		{
			comm.At();
			ATCommunicator.CpmsStorageArea[] s1, s2, s3;
			comm.CpmsQuery(out s1, out s2, out s3);
			comm.Cpms(FindPreferredLocation(s1), FindPreferredLocation(s2), FindPreferredLocation(s3));
			comm.Cmgf(0);
			EnumerableHelper.ForEach(PduEncoder.Encode(sms), pdu => comm.Cmgw(pdu, location));
		}

		private ATCommunicator.CpmsStorageArea FindPreferredLocation(IEnumerable<ATCommunicator.CpmsStorageArea> s)
		{
			Func<ATCommunicator.CpmsStorageArea, bool> isMatch = x =>
			                                                     	{
			                                                     		foreach (var store in s)
			                                                     		{
			                                                     			if (store == x) return true;
			                                                     		}
			                                                     		return false;
			                                                     	};
			if (isMatch(ATCommunicator.CpmsStorageArea.ME)) return ATCommunicator.CpmsStorageArea.ME;
			if (isMatch(ATCommunicator.CpmsStorageArea.MT)) return ATCommunicator.CpmsStorageArea.MT;
			return ATCommunicator.CpmsStorageArea.SM;
		}
	}
}
