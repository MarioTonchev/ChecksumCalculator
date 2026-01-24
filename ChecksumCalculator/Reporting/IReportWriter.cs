using ChecksumCalculator.Visitor;

namespace ChecksumCalculator.Reporting
{
	public interface IReportWriter
	{
		void Write(IEnumerable<ChecksumResult> results, TextWriter output);
	}
}
