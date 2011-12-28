using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ATTestCF.Sms;

namespace ATTestCF
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			cbStoreLocation.Items.Add("ReceivedUnread");
			cbStoreLocation.Items.Add("ReceivedRead");
			cbStoreLocation.Items.Add("StoredUnsent");
			cbStoreLocation.Items.Add("StoredSent");
			cbStoreLocation.SelectedIndex = 2;
		}

		private Sms.Sms BuildSms()
		{
			return new Sms.Sms(txtRecipient.Text, txtMessage.Text);
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			InvokePhone(phone => phone.SendTextMessage(BuildSms()));
		}

		private void btnSave_Click(object sender, EventArgs e)
		{

			InvokePhone(phone => phone.SaveTextMessage(BuildSms(), (ATCommunicator.CmgwStoreLocation) cbStoreLocation.SelectedIndex));
		}

		private void InvokePhone(Action<Phone> phoneAction)
		{
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				using (var phone = new Phone("COM" + (int) numericUpDown1.Value))
				{
					phone.CommandSent +=
						(snd, args) => txtResponse.Text = string.Format("Sent: {0}\r\n{1}", args.Arg, txtResponse.Text);
					phone.ResponseReceived +=
						(snd, args) => txtResponse.Text = string.Format("Received: {0}\r\n{1}", args.Arg, txtResponse.Text);
					phoneAction(phone);
				}
			}
			catch (Exception exc)
			{
				Cursor.Current = Cursors.Default;
				HandleException(exc);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}

		private void HandleException(Exception exc)
		{
			txtResponse.Text = string.Format("!Err: {0}\r\n{1}\r\n{2}", exc.Message, exc.StackTrace, txtResponse.Text);
			MessageBox.Show(string.Format("{0}: {1}", exc.GetType().Name, exc.Message));
		}
	}
}