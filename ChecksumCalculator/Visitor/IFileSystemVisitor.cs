using ChecksumCalculator.Composite;

namespace ChecksumCalculator.Visitor
{
	public interface IFileSystemVisitor
	{
		void VisitFile(FileNode fileNode);

		void VisitDirectory(DirectoryNode directoryNode);
	}
}
