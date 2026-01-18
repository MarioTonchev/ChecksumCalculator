using ChecksumCalculator.Model;

namespace ChecksumCalculator.Hashing
{
	public class FileChecksumService
	{
		public static ChecksumResult Compute(string path, IChecksumCalculator calculator)
		{
			using FileStream fs = File.OpenRead(path);
			string checksum = calculator.Calculate(fs);

			return new ChecksumResult
			{
				Path = path,
				Checksum = checksum,
				Size = fs.Length
			};
		}
	}
}
