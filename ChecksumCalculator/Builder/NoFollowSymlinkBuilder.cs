using ChecksumCalculator.Composite;

namespace ChecksumCalculator.Builder
{
	public class NoFollowSymlinkBuilder : FileSystemBuilder
	{
		public override FileSystemNode Build(string path)
		{
			if (File.Exists(path))
			{
				var info = new FileInfo(path);
				return new FileNode(path, info.Length);
			}

			if (Directory.Exists(path))
			{
				var dirNode = new DirectoryNode(path);

				foreach (var entry in Directory.EnumerateFileSystemEntries(path))
				{
					dirNode.AddChild(Build(entry));
				}

				return dirNode;
			}

			throw new FileNotFoundException(path);
		}
	}
}
