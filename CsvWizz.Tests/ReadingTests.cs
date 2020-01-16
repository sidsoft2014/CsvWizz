using System;
using CsvWizz.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CsvWizz.Tests
{
	[TestClass]
	public class ReadingTests
	{
		private static string csvPath = "readTest.csv";
		private static TestClass model = new TestClass()
		{
			BooleanProperty = true,
			DateTimeProperty = new DateTime(1980, 10, 27),
			EnumProperty = TestEnum.Value2,
			IntegerProperty = 2262,
			StringProperty = "This is a string"
		};

		[TestMethod]
		public void CanReadToJSON()
		{
			var data = new TestClass[] { model };
			if (!CsvWriter.Write(data, csvPath, false))
			{
				Assert.Fail("Could not write .csv to read back from");
			}

			try
			{
				var json = CsvReader.ReadToJSON(csvPath).Trim();
				var result = JsonConvert.DeserializeObject<TestClass[]>(json)?[0];
				var comp = data[0];

				// Check each property, easier and safer than comparing lists
				if (comp.BooleanProperty != result.BooleanProperty)
					Assert.Fail("Boolean properties did not match");
				if (comp.DateTimeProperty != result.DateTimeProperty)
					Assert.Fail("DateTime properties did not match");
				if (comp.EnumProperty != result.EnumProperty)
					Assert.Fail("Enum properties did not match");
				if (comp.IntegerProperty != result.IntegerProperty)
					Assert.Fail("Integer properties did not match");
				if (comp.StringProperty != result.StringProperty)
					Assert.Fail("String properties did not match");
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod]
		public void CanReadToObject()
		{
			var data = new TestClass[] { model };
			if (!CsvWriter.Write(data, csvPath, false))
			{
				Assert.Fail("Could not write .csv to read back from");
			}

			try
			{
				var json = CsvReader.ReadToJSON(csvPath);
				var result = JsonConvert.DeserializeObject<TestClass[]>(json)?[0];
				var comp = data[0];

				// Check each property, easier and safer than comparing lists
				if (comp.BooleanProperty != result.BooleanProperty)
					Assert.Fail("Boolean properties did not match");
				if (comp.DateTimeProperty != result.DateTimeProperty)
					Assert.Fail("DateTime properties did not match");
				if (comp.EnumProperty != result.EnumProperty)
					Assert.Fail("Enum properties did not match");
				if (comp.IntegerProperty != result.IntegerProperty)
					Assert.Fail("Integer properties did not match");
				if (comp.StringProperty != result.StringProperty)
					Assert.Fail("String properties did not match");
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}
