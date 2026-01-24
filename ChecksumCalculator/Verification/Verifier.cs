using ChecksumCalculator.Models;

namespace ChecksumCalculator.Verification
{
    public static class Verifier
    {
        public static List<VerificationResult> Verify(IEnumerable<ChecksumResult> currentChecksums,
            Dictionary<string,string> givenChecksums)
        {
            var verificationResults = new List<VerificationResult>();
            var seen = new HashSet<string>();

            foreach (var checksum in currentChecksums)
            {
                if (givenChecksums.TryGetValue(checksum.Path, out var givenHash))
                {
                    verificationResults.Add(new VerificationResult 
                    {
                        Path = checksum.Path, 
                        Status = checksum.Checksum == givenHash ? VerificationStatus.OK : VerificationStatus.MODIFIED 
                    });
                }
                else
                {
                    verificationResults.Add(new VerificationResult
                    {
                        Path = checksum.Path,
                        Status = VerificationStatus.NEW
                    });
                }

                seen.Add(checksum.Path);
            }

            foreach (var givenChecksumPath in givenChecksums.Keys)
            {
                if (!seen.Contains(givenChecksumPath))
                {
                    verificationResults.Add(new VerificationResult
                    {
                        Path = givenChecksumPath,
                        Status = VerificationStatus.REMOVED
                    });
                }
            }

            return verificationResults;
        }
    }
}
