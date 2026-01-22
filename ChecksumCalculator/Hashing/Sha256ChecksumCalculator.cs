using ChecksumCalculator.Models;
using ChecksumCalculator.PauseResume;
using System.Security.Cryptography;

namespace ChecksumCalculator.Hashing
{
	public class Sha256ChecksumCalculator : BaseChecksumCalculator
	{
		private const int BufferSize = 16;

		public Sha256ChecksumCalculator(PauseController pauseController) : base(pauseController)
		{
		}

		public override string Calculate(Stream stream)
		{
			using SHA256 sha256 = SHA256.Create();
			byte[] buffer = new byte[BufferSize];

			int bytesRead;
			long totalRead = 0;

			while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
			{
				pauseController.WaitIfPaused();

				sha256.TransformBlock(buffer, 0, bytesRead, null, 0);

				totalRead += bytesRead;

				Notify(this, new BytesReadMessage(totalRead));
			}

			sha256.TransformFinalBlock(Array.Empty<byte>(), 0, 0);

			return Convert.ToHexString(sha256.Hash!).ToLowerInvariant();
		}
	}
}
