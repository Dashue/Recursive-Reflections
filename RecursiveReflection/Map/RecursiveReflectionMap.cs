using System;
using System.Collections.Generic;

namespace RecursiveReflection.Map
{
	internal class RecursiveReflectionMap
	{
		public bool IsDictionary { get; set; }
		public bool IsList { get; set; }

		public List<RecursiveReflectionMap> Maps { get; set; }

		public string Name { get; set; }
		public List<string> Properties { get; set; }

		public Type Type { get; set; }
	}
}