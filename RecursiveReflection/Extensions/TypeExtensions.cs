using System;
using System.Collections;
using System.Collections.Generic;

namespace RecursiveReflection.Extensions
{
	internal static class TypeExtensions
	{
		internal static bool IsCollection(this Type type)
		{
			return typeof(ICollection).IsAssignableFrom(type)
				   || typeof(ICollection<>).IsAssignableFrom(type);
		}

		internal static bool IsDictionary(this Type type)
		{
			return typeof(IDictionary).IsAssignableFrom(type);
		}

		internal static bool IsList(this Type type)
		{
			return typeof(IList).IsAssignableFrom(type)
				   || typeof(IList<>).IsAssignableFrom(type);
		}
	}
}