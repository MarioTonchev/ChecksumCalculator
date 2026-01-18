using ChecksumCalculator.Model;

namespace ChecksumCalculator.Reporting
{
	internal class TextReportWriter : IReportWriter
	{
		public void Write(ChecksumResult result)
		{
			Console.WriteLine($"{result.Checksum} *{result.Path}");
		}
	}
}
