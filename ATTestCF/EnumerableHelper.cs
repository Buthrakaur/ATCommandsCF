using System;
using System.Collections;
using System.Collections.Generic;

namespace ATTestCF
{
	public static class EnumerableHelper
	{
		public static List<T> ToList<T>(IEnumerable<T> e)
		{
			var res = new List<T>();
			foreach (var i in e)
			{
				res.Add(i);
			}
			return res;
		}

		public static T[] ToArray<T>(IEnumerable<T> e)
		{
			return ToList(e).ToArray();
		}

		public static bool Any<T>(IEnumerable<T> e)
		{
			foreach (var i in e)
			{
				return true;
			}
			return false;
		}

		public static bool Empty<T>(IEnumerable<T> e)
		{
			return !Any(e);
		}

		public static bool Any<T>(IEnumerable<T> e, Predicate<T> match)
			where T : class
		{
			return Find(e, match) != null;
		}

		public static bool Empty<T>(IEnumerable<T> e, Predicate<T> match)
			where T : class
		{
			return !Any(e, match);
		}

		public static T Find<T>(IEnumerable<T> e, Predicate<T> match)
			where T : class
		{
			foreach (var i in e)
			{
				if (match(i)) return i;
			}
			return null;
		}

		public static IEnumerable<T> FindAll<T>(IEnumerable<T> e, Predicate<T> match)
		{
			foreach (var i in e)
			{
				if (match(i)) yield return i;
			}
		}

		public static void ForEach<T>(IEnumerable<T> e, Action<T> action)
		{
			foreach (var i in e)
			{
				action(i);
			}
		}

		public static int Count<T>(IEnumerable<T> e)
		{
			var cnt = 0;
			var collection = e as ICollection;
			if (collection != null) return collection.Count;
			var collectionT = e as ICollection<T>;
			if (collectionT != null) return collectionT.Count;

			foreach (var i in e)
			{
				cnt++;
			}
			return cnt;
		}

		public static T FirstOrDefault<T>(IEnumerable<T> e)
		{
			foreach (var i in e)
			{
				return i;
			}
			return default(T);
		}

		public static IEnumerable<TRes> Select<T, TRes>(IEnumerable<T> e, Func<T, TRes> selector)
		{
			foreach (var i in e)
			{
				yield return selector(i);
			}
		}

		public static IEnumerable<TResult> SelectMany<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
		{
			foreach (var i in source)
			{
				foreach (var result in selector(i))
					yield return result;
			}
		}

		public static IEnumerable<T> Concatenate<T>(IEnumerable<T> a, IEnumerable<T> b)
		{
			foreach (var x in a) yield return x;
			foreach (var x in b) yield return x;
		}

		public static T Single<T>(IEnumerable<T> a)
		{
			var res = default(T);
			var idx = 0;
			foreach (var i in a)
			{
				if (idx > 0) throw new ArgumentException("There's more than single one element in collection.", "a");
				res = i;
				idx++;
			}
			if (idx == 0) throw new ArgumentException("Collection is empty.", "a");
			return res;
		}
	}
}
