using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsvWizz;
using System;
using System.Collections.Generic;
using System.IO;

namespace CsvWizz.Tests
{
	[TestClass]
	public class WritingTests
	{
		public class TestClass
		{
			public int IntegerProperty { get; set; }
			public string StringProperty { get; set; }
			public bool BooleanProperty { get; set; }
			public DateTime DateTimeProperty { get; set; }
			public TestEnum EnumProperty { get; set; }
		}

		public enum TestEnum
		{
			Value1,
			Value2,
			Value3
		}

		[TestMethod]
		public void CanWriteData()
		{
			var data = new List<TestClass>();

			for (int x = 0; x < 10; x++)
			{
				data.Add(new TestClass
				{
					IntegerProperty = x,
					StringProperty = $"This is row {x}",
					BooleanProperty = x % 2 == 0 ? true : false,
					DateTimeProperty = DateTime.Now,
					EnumProperty = x % 2 == 0 ? TestEnum.Value1 : TestEnum.Value2,
				});
			}

			if (!CsvWriter.Write(data, "test.csv"))
			{
				Assert.Fail("Could not write csv file");
			}
		}

		[TestMethod]
		public void CanOverwriteData()
		{
			var data = new List<TestClass>();
			var date = DateTime.Now;
			for (int x = 0; x < 10; x++)
			{
				data.Add(new TestClass
				{
					IntegerProperty = x,
					StringProperty = $"This is row {x}",
					BooleanProperty = x % 2 == 0 ? true : false,
					DateTimeProperty = date,
					EnumProperty = x % 2 == 0 ? TestEnum.Value1 : TestEnum.Value2,
				});
			}

			var expectedText = "IntegerProperty,StringProperty,BooleanProperty,DateTimeProperty,EnumProperty\r\n" +
								$"0,This is row 0,True,{date},Value1\r\n" +
								$"1,This is row 1,False,{date},Value2\r\n" +
								$"2,This is row 2,True,{date},Value1\r\n" +
								$"3,This is row 3,False,{date},Value2\r\n" +
								$"4,This is row 4,True,{date},Value1\r\n" +
								$"5,This is row 5,False,{date},Value2\r\n" +
								$"6,This is row 6,True,{date},Value1\r\n" +
								$"7,This is row 7,False,{date},Value2\r\n" +
								$"8,This is row 8,True,{date},Value1\r\n" +
								$"9,This is row 9,False,{date},Value2\r\n" +
								"";

			if (!CsvWriter.Write(data, "test.csv", false))
			{
				Assert.Fail("Could not write csv file");
			}

			var resultText = File.ReadAllText("test.csv");

			Assert.AreEqual<string>(expectedText, resultText);
		}

		[TestMethod]
		public void CanAppendData()
		{
			var data = new List<TestClass>();
			var date = DateTime.Now;
			for (int x = 0; x < 10; x++)
			{
				data.Add(new TestClass
				{
					IntegerProperty = x,
					StringProperty = $"This is row {x}",
					BooleanProperty = x % 2 == 0 ? true : false,
					DateTimeProperty = date,
					EnumProperty = x % 2 == 0 ? TestEnum.Value1 : TestEnum.Value2,
				});
			}

			var expectedText = "IntegerProperty,StringProperty,BooleanProperty,DateTimeProperty,EnumProperty\r\n" +
								$"0,This is row 0,True,{date},Value1\r\n" +
								$"1,This is row 1,False,{date},Value2\r\n" +
								$"2,This is row 2,True,{date},Value1\r\n" +
								$"3,This is row 3,False,{date},Value2\r\n" +
								$"4,This is row 4,True,{date},Value1\r\n" +
								$"5,This is row 5,False,{date},Value2\r\n" +
								$"6,This is row 6,True,{date},Value1\r\n" +
								$"7,This is row 7,False,{date},Value2\r\n" +
								$"8,This is row 8,True,{date},Value1\r\n" +
								$"9,This is row 9,False,{date},Value2\r\n" +
								"";

			if (!CsvWriter.Write(data, "test.csv", false))
			{
				Assert.Fail("Could not write csv file");
			}

			var resultText = File.ReadAllText("test.csv");
			if (expectedText != resultText)
			{
				Assert.Fail("Initial write did not produce expected results.");
			}

			if (!CsvWriter.Write(data, "test.csv"))
			{
				Assert.Fail("Could not write csv file");
			}

			expectedText = "IntegerProperty,StringProperty,BooleanProperty,DateTimeProperty,EnumProperty\r\n" +
								$"0,This is row 0,True,{date},Value1\r\n" +
								$"1,This is row 1,False,{date},Value2\r\n" +
								$"2,This is row 2,True,{date},Value1\r\n" +
								$"3,This is row 3,False,{date},Value2\r\n" +
								$"4,This is row 4,True,{date},Value1\r\n" +
								$"5,This is row 5,False,{date},Value2\r\n" +
								$"6,This is row 6,True,{date},Value1\r\n" +
								$"7,This is row 7,False,{date},Value2\r\n" +
								$"8,This is row 8,True,{date},Value1\r\n" +
								$"9,This is row 9,False,{date},Value2\r\n" +
								$"0,This is row 0,True,{date},Value1\r\n" +
								$"1,This is row 1,False,{date},Value2\r\n" +
								$"2,This is row 2,True,{date},Value1\r\n" +
								$"3,This is row 3,False,{date},Value2\r\n" +
								$"4,This is row 4,True,{date},Value1\r\n" +
								$"5,This is row 5,False,{date},Value2\r\n" +
								$"6,This is row 6,True,{date},Value1\r\n" +
								$"7,This is row 7,False,{date},Value2\r\n" +
								$"8,This is row 8,True,{date},Value1\r\n" +
								$"9,This is row 9,False,{date},Value2\r\n" +
								"";

			resultText = File.ReadAllText("test.csv");
			Assert.AreEqual<string>(expectedText, resultText);
		}
	}
}
