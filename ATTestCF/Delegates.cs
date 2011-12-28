namespace ATTestCF
{
	public delegate void Action<T>(T obj);
	public delegate void Action<T,T2>(T obj, T2 obj2);
	public delegate void Action<T,T2,T3>(T obj, T2 obj2, T3 obj3);
	public delegate void Action<T,T2,T3,T4>(T obj, T2 obj2, T3 obj3, T4 obj4);

	public delegate TRes Func<TRes>();
	public delegate TRes Func<T, TRes>(T arg);
	public delegate TRes Func<T, T2, TRes>(T arg, T2 arg2);
	public delegate TRes Func<T, T2, T3, TRes>(T arg, T2 arg2, T3 arg3);
}
