using ChecksumCalculator.Verification;
using ChecksumCalculator.Visitor;

namespace ChecksumCalculator.Tests.Verification
{
    [TestFixture]
    public class VerifierTests
    {
        [Test]
        public void Verifier_DetectsAllFourCases()
        {
            var currentChecksums = new List<ChecksumResult>
            {
                new() { Path = "a.txt", Checksum = "123" },
                new() { Path = "b.txt", Checksum = "999" },
                new() { Path = "c.txt", Checksum = "555" }
            };

            var givenChecksums = new Dictionary<string, string>
            {
                { "a.txt", "123" },   
                { "b.txt", "111" },   
                { "d.txt", "000" }    
            };

            var results = Verifier.Verify(currentChecksums, givenChecksums);

            Assert.That(results.Any(r => r.Path == "a.txt" && r.Status == VerificationStatus.OK));
            Assert.That(results.Any(r => r.Path == "b.txt" && r.Status == VerificationStatus.MODIFIED));
            Assert.That(results.Any(r => r.Path == "c.txt" && r.Status == VerificationStatus.NEW));
            Assert.That(results.Any(r => r.Path == "d.txt" && r.Status == VerificationStatus.REMOVED));
        }
    }
}
