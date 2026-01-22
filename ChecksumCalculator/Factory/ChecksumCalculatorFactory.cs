using ChecksumCalculator.Hashing;
using ChecksumCalculator.PauseResume;

namespace ChecksumCalculator.Factory
{
	public static class ChecksumCalculatorFactory
	{
		public static BaseChecksumCalculator Create(string algorithm, PauseController pauseController)
		{
			switch (algorithm.ToLower())
			{
				case "sha256":
					return new Sha256ChecksumCalculator(pauseController);
				default:
					throw new ArgumentException($"Unsupported algorithm: {algorithm}");
			};
		}
	}
}
