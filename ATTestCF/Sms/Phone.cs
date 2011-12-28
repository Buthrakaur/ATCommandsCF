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
			comm.Cmgs(PduEncoder.Encode(sms));
		}

		public void SaveTextMessage(Sms sms, ATCommunicator.CmgwStoreLocation location)
		{
			comm.At();
			comm.Cpms(ATCommunicator.CpmsStorageArea.ME, ATCommunicator.CpmsStorageArea.ME, ATCommunicator.CpmsStorageArea.ME);
			comm.Cmgf(0);
			comm.Cmgw(PduEncoder.Encode(sms), location);
		}
	}
}
