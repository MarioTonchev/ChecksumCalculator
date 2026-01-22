using ChecksumCalculator.Visitor;

namespace ChecksumCalculator.Composite
{
	public class DirectoryNode : FileSystemNode
	{
		private readonly List<FileSystemNode> children;

		public IReadOnlyCollection<FileSystemNode> Children => children;

        public DirectoryNode(string path) : base(path)
		{
			children = new();
			Size = 0;
		}

		public void AddChild(FileSystemNode child)
		{
			children.Add(child);
			Size += child.Size;
		}

		public override void Accept(IFileSystemVisitor visitor)
		{
			visitor.VisitDirectory(this);

			foreach (FileSystemNode child in Children)
			{
				child.Accept(visitor);
			}
		}
	}
}
