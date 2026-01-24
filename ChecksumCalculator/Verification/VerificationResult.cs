namespace ChecksumCalculator.Verification
{
    public enum VerificationStatus
    {
        OK,
        MODIFIED,
        NEW,
        REMOVED
    }

    public class VerificationResult
    {
        public string Path { get; init; } = string.Empty;
        public VerificationStatus Status { get; init; }
    }
}
