namespace ChecksumCalculator.Hashing
{
	public interface IChecksumCalculator
	{
		public string Calculate(Stream stream);
	}
}
