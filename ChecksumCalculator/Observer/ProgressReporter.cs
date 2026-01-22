using ChecksumCalculator.Models;
using ChecksumCalculator.PauseResume;
using System.Diagnostics;

namespace ChecksumCalculator.Observer
{
	public class ProgressReporter : IObserver
	{
		private readonly long totalBytes;
		private long totalBytesRead = 0;

		private string currentPath = "(none)";
		private long currentFileBytesRead = 0;

		private readonly Stopwatch stopwatch;
		private bool isFirstFile = true;

		public ProgressReporter(long totalBytes, PauseController pauseController)
		{
			stopwatch = Stopwatch.StartNew();
			this.totalBytes = totalBytes;

			pauseController.Resumed += () => stopwatch.Start();
			pauseController.Paused += () => stopwatch.Stop();
		}

		public void Update(object sender, object message)
		{
			if (message is NewFileMessage fileMessage)
			{
				currentPath = fileMessage.Path;
				currentFileBytesRead = 0;

				if (!isFirstFile)
				{
					Console.WriteLine();
				}
				else
				{
					isFirstFile = false;
				}
			}
			else if (message is BytesReadMessage bytesMessage)
			{
				long delta = bytesMessage.BytesRead - currentFileBytesRead;

				currentFileBytesRead = bytesMessage.BytesRead;
				totalBytesRead += delta;
			}
			else
			{
				return;
			}

			Refresh();
		}

		private void Refresh()
		{
			double percent = totalBytes == 0 ? 100 : (double)totalBytesRead / totalBytes * 100;
			TimeSpan elapsed = stopwatch.Elapsed;

			TimeSpan eta = totalBytesRead == 0 ? TimeSpan.Zero : TimeSpan.FromSeconds(elapsed.TotalSeconds * (totalBytes - totalBytesRead) / totalBytesRead);

			Console.Write($"\rProcessing {currentPath}... {totalBytesRead}/{totalBytes} bytes ({percent:F1}%) ETA: {eta:mm\\:ss}");
		}
	}
}
