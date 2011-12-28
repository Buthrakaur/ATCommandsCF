using System;

namespace ATTestCF.Tests
{
	public static class Assert
	{
		public static void Equal<T>(T expected, T actual)
		{
			if (!expected.Equals(actual))
				throw new Exception(string.Format("expected:\r\n{0}\r\nactual:\r\n{1}", expected, actual));
		}

		public static void True(bool value)
		{
			Equal(true, value);
		}

		public static void False(bool value)
		{
			Equal(false, value);
		}
	}
}