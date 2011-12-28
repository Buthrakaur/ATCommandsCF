using System;

namespace ATTestCF
{
	public class GenericEventArgs<T> : EventArgs
	{
		public T Arg { get; set; }

		public GenericEventArgs(T arg)
		{
			Arg = arg;
		}
	}
}