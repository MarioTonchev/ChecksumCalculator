using ChecksumCalculator.Model;

namespace ChecksumCalculator.Reporting
{
	internal interface IReportWriter
	{
		void Write(ChecksumResult result);
	}
}
