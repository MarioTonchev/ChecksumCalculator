using ChecksumCalculator.Verification;

namespace ChecksumCalculator.Tests.Verification
{
    [TestFixture]
    public class ChecksumFileParserTests
    {
        [Test]
        public void Parse_ValidFile_WorksCorrectly()
        {
            var temp = Path.GetTempFileName();

            File.WriteAllText(temp, "abc *a.txt\ndef *b.txt");

            var result = ChecksumFileParser.Parse(temp);

            Assert.That(result["a.txt"], Is.EqualTo("abc"));
            Assert.That(result["b.txt"], Is.EqualTo("def"));
        }

        [Test]
        public void Parse_NonExistentFile_Throws()
        {
            Assert.Throws<FileNotFoundException>(() => ChecksumFileParser.Parse("idk"));
        }
    }
}
