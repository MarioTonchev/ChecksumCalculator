using ChecksumCalculator.Visitor;

namespace ChecksumCalculator.Composite
{
	public abstract class FileSystemNode
	{
        public string Path { get; }
        public long Size { get; protected set; }

        protected FileSystemNode(string path)
        {
            Path = path;
        }

        public abstract void Accept(IFileSystemVisitor visitor);
    }
}
