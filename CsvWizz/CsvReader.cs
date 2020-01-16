using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CsvWizz
{
	public class CsvReader
	{
		/// <summary>
		/// Reads a .csv file into JSON format
		/// </summary>
		/// <param name="path">The path to the .csv file.</param>
		/// <param name="delimiter">The string to use as the value delimiter. (default ",")</param>
		/// <returns></returns>
		public static string ReadToJSON(string path, string delimiter = ",")
		{
			// Can't parse a file that doesn't exist
			if (!File.Exists(path))
				return null;

			// Read text as an array to save splitting on new lines
			var csvText = File.ReadAllLines(path);

			// If .csv includes headers they should be the first row.
			var propNames = SplitRow(csvText[0], delimiter);

			// Convert .csv values into JSON objects and add to collection
			var objStrings = new List<string>();
			for (var r = 1; r < csvText.Length; r++)
			{
				var row = csvText[r];
				var values = SplitRow(row, delimiter);
				var objProps = new List<string>();
				for (int i = 0; i < propNames.Length; i++)
				{
					var name = propNames[i];
					var value = values[i];

					string insertValue;

					// Need to determine the datatype
					if (int.TryParse(value, out int intResult))
					{
						// No quotes on numbers
						insertValue = $"\"{name}\":{intResult}";
					}
					else if (double.TryParse(value, out double doubleResult))
					{
						// No quotes on numbers
						insertValue = $"\"{name}\":{doubleResult}";
					}
					else if (decimal.TryParse(value, out decimal decimalResult))
					{
						// No quotes on numbers
						insertValue = $"\"{name}\":{decimalResult}";
					}
					else if (DateTime.TryParse(value, out DateTime dateResult))
					{
						// JSON Dates do not match .Net dates, so correct them
						var h = dateResult.Hour < 10 ? $"0{dateResult.Hour}" : dateResult.Hour.ToString();
						var m = dateResult.Minute < 10 ? $"0{dateResult.Minute}" : dateResult.Minute.ToString();
						var s = dateResult.Second < 10 ? $"0{dateResult.Second}" : dateResult.Second.ToString();

						var dateVal = $"{dateResult.Year}-{dateResult.Month}-{dateResult.Day}T{h}:{m}:{s}";
						insertValue = $"\"{name}\":\"{dateVal}\"";
					}
					else if (Boolean.TryParse(value, out bool boolResult))
					{
						// No quotes and lower case for bool
						insertValue = $"\"{name}\":{boolResult.ToString().ToLower()}";
					}
					else
					{
						// Assume string or string-compatible datatype
						insertValue = $"\"{name}\":\"{value}\"";

					}

					objProps.Add(insertValue);

				}

				var valsSb = new StringBuilder();
				valsSb.Append("{");
				valsSb.Append(string.Join(",", objProps));
				valsSb.Append("}");
				objStrings.Add(valsSb.ToString());
			}

			// Join all objects with comma and wrap in square brackets to create JSON array
			var sb = new StringBuilder();
			sb.Append("[");
			sb.Append(string.Join(",", objStrings));
			sb.Append("]");

			return sb.ToString();
		}

		public static T[] ReadToObject<T>(string path, string delimiter = ",")
		{
			var json = ReadToJSON(path, delimiter);
			var deserialised = JsonConvert.DeserializeObject<T[]>(json);
			return deserialised;
		}

		private static string[] SplitRow(string row, string delimiter)
		{
			// Split on delimiter, don't remove empty entries, trim results
			return row.Split(new string[] { delimiter }, StringSplitOptions.None)
				.Select(p => p.Trim()).ToArray();
		}
	}
}
