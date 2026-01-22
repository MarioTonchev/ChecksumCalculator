using ChecksumCalculator.Visitor;

namespace ChecksumCalculator.Composite
{
	public class FileNode : FileSystemNode
	{
        public FileNode(string path, long size) : base(path)
        {
			Size = size;
        }

        public override void Accept(IFileSystemVisitor visitor)
		{
			visitor.VisitFile(this);
		}
	}
}
