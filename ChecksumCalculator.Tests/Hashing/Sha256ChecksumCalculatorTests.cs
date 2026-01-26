using ChecksumCalculator.Hashing;
using ChecksumCalculator.PauseResume;
using System.Text;

namespace ChecksumCalculator.Tests.Hashing
{
    [TestFixture]
    public class Sha256ChecksumCalculatorTests
    {
        [Test]
        public void Calculate_ReturnsCorrectHash()
        {
            var pauseController = new PauseController();
            var calculator = new Sha256ChecksumCalculator(pauseController);

            var input = "abc";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(input));

            var hash = calculator.Calculate(stream);

            Assert.That(hash, Is.EqualTo("ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad"));
        }
    }
}
