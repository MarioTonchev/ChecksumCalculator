using ChecksumCalculator.Visitor;

namespace ChecksumCalculator.Reporting
{
	public class TextReportWriter : IReportWriter
	{
		public void Write(IEnumerable<ChecksumResult> results, TextWriter output)
		{
			foreach (ChecksumResult result in results)
			{
				output.WriteLine($"{result.Checksum} *{result.Path}");
			}
		}
	}
}
