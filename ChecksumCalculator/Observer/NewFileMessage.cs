namespace ChecksumCalculator.Observer
{
	public class NewFileMessage
	{
		public string Path { get; }

		public NewFileMessage(string path)
		{
			Path = path;
		}
	}
}
