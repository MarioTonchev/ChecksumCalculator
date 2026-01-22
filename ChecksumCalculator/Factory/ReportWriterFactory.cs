using ChecksumCalculator.Reporting;

namespace ChecksumCalculator.Factory
{
	public static class ReportWriterFactory
	{
		public static IReportWriter Create(string format)
		{
			switch(format.ToLower())
			{
				case "text":
					return new TextReportWriter();
				case "json":
					return new JsonReportWriter();
				default:throw new ArgumentException($"Unsupported report format: {format}");
			}
		}
	}
}
