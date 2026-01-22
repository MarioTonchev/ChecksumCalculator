using ChecksumCalculator.Composite;

namespace ChecksumCalculator.Builder
{
	public class FollowSymlinkBuilder : FileSystemBuilder
	{
		private readonly HashSet<string> visited = new();

		public override FileSystemNode Build(string path)
		{
			string fullPath = Path.GetFullPath(path);

			if (OperatingSystem.IsWindows() && WindowsShortcutResolver.IsShortcut(fullPath))
			{
				string? target = WindowsShortcutResolver.ResolveTarget(fullPath);

				if (target == null)
				{
					var info = new FileInfo(fullPath);
					return new FileNode(fullPath, info.Length);
				}

				fullPath = Path.GetFullPath(target);
			}

			if (visited.Contains(fullPath))
			{
				return new DirectoryNode(fullPath);
			}

			visited.Add(fullPath);

			if (File.Exists(fullPath))
			{
				var info = new FileInfo(fullPath);
				return new FileNode(fullPath, info.Length);
			}

			if (Directory.Exists(fullPath))
			{
				var dirNode = new DirectoryNode(fullPath);

				foreach (var entry in Directory.EnumerateFileSystemEntries(fullPath))
				{
					string childPath = Path.GetFullPath(entry);

					if (visited.Contains(childPath))
					{
						continue;
					}

					dirNode.AddChild(Build(entry));
				}

				return dirNode;
			}

			throw new FileNotFoundException(fullPath);
		}
	}
}
