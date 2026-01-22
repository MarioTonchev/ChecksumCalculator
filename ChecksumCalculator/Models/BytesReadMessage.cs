namespace ChecksumCalculator.Models
{
	public class BytesReadMessage
	{
        public long BytesRead { get; }

        public BytesReadMessage(long bytesRead)
        {
			BytesRead = bytesRead;
        }
    }
}
