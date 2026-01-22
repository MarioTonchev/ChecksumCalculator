using ChecksumCalculator.Model;
using System.Text.Json;


namespace ChecksumCalculator.Reporting
{
	public class JsonReportWriter : IReportWriter
	{
		public void Write(IEnumerable<ChecksumResult> results, TextWriter output)
		{
			var options = new JsonSerializerOptions
			{
				WriteIndented = true
			};

			string json = JsonSerializer.Serialize(results, options);

			output.Write(json);
		}
	}
}
