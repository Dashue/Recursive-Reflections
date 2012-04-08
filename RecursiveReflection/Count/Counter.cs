using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RecursiveReflection.Attributes;

namespace RecursiveReflection.Count
{
	internal static class Counter
	{
		internal static int CountProperties(Type modelType)
		{
			var inf = modelType;
			var count = 0;
			var properties = inf.GetProperties();

			count += CountProperties(properties);

			return count;
		}

		private static int CountProperties(IEnumerable<PropertyInfo> properties)
		{
			var count = 0;
			foreach (PropertyInfo property in properties)
			{
				var subProperties = property.PropertyType.GetProperties();
				if (subProperties.Any())
				{
					count += CountProperties(subProperties);
				}

				if (property.GetCustomAttributes(typeof(RecursiveReflectionAttribute), false).Any())
				{
					count++;
				}
			}

			return count;
		}
	}
}