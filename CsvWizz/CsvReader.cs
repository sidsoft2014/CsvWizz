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
					objProps.Add($"\"{name}\": \"{value}\"");

				}

				var valsSb = new StringBuilder();
				valsSb.AppendLine("{");
				valsSb.AppendLine(string.Join(",\r\n", objProps));
				valsSb.AppendLine("}");
				objStrings.Add(valsSb.ToString());
			}

			// Join all objects with comma and wrap in square brackets to create JSON array
			var sb = new StringBuilder();
			sb.AppendLine("[");
			sb.Append(string.Join(",\r\n", objStrings));
			sb.AppendLine("]");

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
