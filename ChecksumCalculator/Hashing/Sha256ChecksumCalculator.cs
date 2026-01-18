using System.Security.Cryptography;

namespace ChecksumCalculator.Hashing
{
	public class Sha256ChecksumCalculator : IChecksumCalculator
	{
		public string Calculate(Stream stream)
		{
			using SHA256 sha256 = SHA256.Create();
			byte[] hash = sha256.ComputeHash(stream);
			return Convert.ToHexString(hash).ToLowerInvariant();
		}
	}
}
