using IWshRuntimeLibrary;

public static class WindowsShortcutResolver
{
	public static bool IsShortcut(string path)
	{
		return Path.GetExtension(path).Equals(".lnk", StringComparison.OrdinalIgnoreCase);
	}

	public static string? ResolveTarget(string lnkPath)
	{
		try
		{
			var shell = new WshShell();

			IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(lnkPath);

			return string.IsNullOrWhiteSpace(shortcut.TargetPath) ? null : shortcut.TargetPath;
		}
		catch
		{
			return null; 
		}
	}
}
