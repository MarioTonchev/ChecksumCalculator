using ChecksumCalculator.Composite;

namespace ChecksumCalculator.Builder
{
	public abstract class FileSystemBuilder
	{
		public abstract FileSystemNode Build(string path);
	}
}
