using ChecksumCalculator.Reporting;
using ChecksumCalculator.Visitor;

namespace ChecksumCalculator.Tests.Reporting
{
    [TestFixture]
    public class TextReportWriterTests
    {
        [Test]
        public void TextReportWriter_WritesCorrectFormat()
        {
            var results = new List<ChecksumResult>
            {
                new() { Path = "a.txt", Checksum = "123"}
            };

            var textReportWriter = new TextReportWriter();
            var stringWriter = new StringWriter();

            textReportWriter.Write(results, stringWriter);

            var output = stringWriter.ToString().Trim();

            Assert.That(output, Is.EqualTo("123 *a.txt"));
        }
    }
}
