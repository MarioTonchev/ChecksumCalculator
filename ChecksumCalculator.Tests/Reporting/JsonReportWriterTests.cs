using ChecksumCalculator.Reporting;
using ChecksumCalculator.Visitor;

namespace ChecksumCalculator.Tests.Reporting
{
    [TestFixture]
    public class JsonReportWriterTests
    {
        [Test]
        public void JsonReportWriter_WritesValidJson()
        {
            var results = new List<ChecksumResult>
            {
                new() { Path = "a.txt", Checksum = "123" }
            };

            var jsonReportWriter = new JsonReportWriter();
            var stringWriter = new StringWriter();

            jsonReportWriter.Write(results, stringWriter);

            var json = stringWriter.ToString();

            Assert.That(json, Does.Contain("\"Path\": \"a.txt\""));
            Assert.That(json, Does.Contain("\"Checksum\": \"123\""));
        }

    }
}
