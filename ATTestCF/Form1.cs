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
			txtResponse.Text = new string('-', 10) + "\r\n" + txtResponse.Text;
			try
			{
				using (var phone = new Phone("COM" + (int) numericUpDown1.Value))
				{
					phone.CommandSent +=
						(snd, args) => AddResponseText(string.Format("Sent: {0}\r\n", args.Arg));
					phone.ResponseReceived +=
						(snd, args) => AddResponseText(string.Format("Received: {0}\r\n", args.Arg));
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

		private void AddResponseText(string txt)
		{
			txtResponse.Text = txt + txtResponse.Text;
		}

		private void HandleException(Exception exc)
		{
			AddResponseText(string.Format("!Err: {0}\r\n{1}\r\n", exc.Message, exc.StackTrace));
			MessageBox.Show(string.Format("{0}: {1}", exc.GetType().Name, exc.Message));
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			InvokePhone(phone =>
			            	{
			            		var phonebook = EnumerableHelper.ToList(phone.ListPhonebook());
								phonebook.Sort((a,b) => a.Name.CompareTo(b.Name));
			            		var sb = new StringBuilder();
								EnumerableHelper.ForEach(phonebook, p => sb.Append(string.Format("{0}: {1}\r\n", p.Name, p.PhoneNumber)));
								AddResponseText("Phonebook entries:\r\n" + sb.ToString());
			            	});
		}
	}
}