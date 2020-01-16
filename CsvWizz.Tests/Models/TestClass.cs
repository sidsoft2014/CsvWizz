using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CsvWizz.Tests.Models
{
	public class TestClass : IEquatable<TestClass>
	{
		public int IntegerProperty { get; set; }
		public string StringProperty { get; set; }
		public bool BooleanProperty { get; set; }
		public DateTime DateTimeProperty { get; set; }
		public TestEnum EnumProperty { get; set; }

		public override bool Equals(object obj)
		{
			return Equals(obj as TestClass);
		}

		public bool Equals([AllowNull] TestClass other)
		{
			return other != null &&
				   IntegerProperty == other.IntegerProperty &&
				   StringProperty == other.StringProperty &&
				   BooleanProperty == other.BooleanProperty &&
				   DateTimeProperty == other.DateTimeProperty &&
				   EnumProperty == other.EnumProperty;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(IntegerProperty, StringProperty, BooleanProperty, DateTimeProperty, EnumProperty);
		}

		public static bool operator ==(TestClass left, TestClass right)
		{
			return EqualityComparer<TestClass>.Default.Equals(left, right);
		}

		public static bool operator !=(TestClass left, TestClass right)
		{
			return !(left == right);
		}
	}
}
