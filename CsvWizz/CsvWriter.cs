using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsvWizz
{
	public class CsvWriter
	{
		/// <summary>
		/// Writes a collection of objects to a .csv file. Optinaly appending to or overwriting any existing file.
		/// </summary>
		/// <param name="rows">Object collection to write to the csv</param>
		/// <param name="path">Path of the .csv file</param>
		/// <param name="appendIfFileExists">Append data if the file already exists. Otherwise overwrite. (default true)</param>
		/// <param name="delimiter">The string to use as a delimiter. (default ",")</param>
		/// <param name="includeHeaders">Wether to include property names as headers. (default true)
		/// <para>Will be ignored if 'appendIfFileExists' is true and the file exists.</para></param>
		/// <returns></returns>
		public static bool Write(IEnumerable<object> rows, string path, bool appendIfFileExists = true, string delimiter = ",", bool includeHeaders = true)
		{
			// Quit here if no data provided
			if (rows?.Any() == false)
				return false;

			var sb = new StringBuilder();

			// Add headers if required
			if (includeHeaders)
			{
				try
				{
					/*
					 * Nobody wants headers in the middle of their dataset.
					 * If the file exists and we are told to append data we need to skip adding headers.
					 */
					if (!File.Exists(path) || !appendIfFileExists)
					{
						var headers = string.Join(delimiter, rows.First().GetType().GetProperties().Select(p => p.Name));
						sb.AppendLine(headers);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Parsing headers failed! {ex.Message}");
					return false;
				}
			}

			// Get row values
			try
			{
				foreach (var row in rows)
				{
					var values = row.GetType().GetProperties().Select(p => p.GetValue(row)?.ToString())?.ToArray();
					sb.AppendLine(string.Join(delimiter, values));
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Parsing rows failed!: {ex.Message}");
				return false;
			}

			// Create final string and write or append to file
			var csvText = sb.ToString();
			try
			{
				if (!appendIfFileExists)
				{
					File.WriteAllText(path, csvText);
				}
				else
				{
					File.AppendAllText(path, csvText);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Writing csv failed! {ex.Message}");
				return false;
			}

			// Yay, we succeeded!
			return true;
		}
	}
}
