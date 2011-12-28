using System;

namespace ATTestCF.Sms
{
	public class Sms
	{
		public string Recipient { get; private set; }
		public string Message { get; private set; }

		public Sms(string recipient, string message)
		{
			EnsureRecipientValid(recipient);
			Recipient = recipient;
			Message = message;
		}

		private void EnsureRecipientValid(string recipient)
		{
			if (recipient.Length != 13 || 
				!recipient.StartsWith("+") ||
				EnumerableHelper.Any(EnumerableHelper.FindAll(recipient.Substring(1), c => !char.IsNumber(c))))
			{
				throw new ArgumentException("Recipient must be in international +420123456789 format.", "recipient");
			}
		}
	}
}
