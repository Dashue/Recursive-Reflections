using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecursiveReflection.Attributes;

namespace RecursiveReflection.Tests
{
	[TestClass]
	public class RecursiveReflectionTests
	{
		[TestMethod]
		public void Usage()
		{
			var dto = new TestDto { MyText = "MyTextValue", MyList = new List<TestDto2> { new TestDto2 { Text = "MyText" } } };
			var reflector = new RecursiveReflector();
			reflector.Reflect(dto);
		}
	}

	public class TestDto2
	{
		[WriteValueToConsole]
		public string Text { get; set; }
	}

	public class TestDto
	{
		public List<TestDto2> MyList { get; set; }

		[WriteValueToConsole]
		public string MyText { get; set; }
	}
}
