using ChecksumCalculator.Composite;
using ChecksumCalculator.Hashing;
using ChecksumCalculator.Observer;
using ChecksumCalculator.PauseResume;

namespace ChecksumCalculator.Visitor
{
	public class HashStreamWriter : Observable, IFileSystemVisitor, IObserver
	{
		private readonly BaseChecksumCalculator calculator;
		private readonly List<ChecksumResult> results;
		private readonly PauseController pauseController;

		public HashStreamWriter(BaseChecksumCalculator calculator, List<ChecksumResult> results, PauseController pauseController)
		{
			this.calculator = calculator;
			this.results = results;
			this.pauseController = pauseController;
			calculator.RegisterObserver(this);
		}

		public void VisitFile(FileNode file)
		{
			pauseController.WaitIfPaused();

			Notify(this, new NewFileMessage(file.Path));

			using FileStream fs = File.OpenRead(file.Path);
			string checksum = calculator.Calculate(fs);

			ChecksumResult result = new ChecksumResult()
			{
				Path = file.Path,
				Checksum = checksum,
				Size = file.Size
			};

			results.Add(result);

			pauseController.WaitIfPaused();
		}

		public void VisitDirectory(DirectoryNode directory)
		{
		}

		public void Update(object sender, object message)
		{
			Notify(sender, message);
		}
	}
}
