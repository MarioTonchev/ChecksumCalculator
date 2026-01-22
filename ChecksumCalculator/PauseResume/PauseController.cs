namespace ChecksumCalculator.PauseResume
{
	public class PauseController
	{
		private volatile bool paused;

		public bool IsPaused => paused;

		public event Action? Paused;
		public event Action? Resumed;

		public void Pause()
		{
			paused = true;
			Paused?.Invoke();
		}

		public void Resume()
		{
			paused = false;
			Resumed?.Invoke();
		}

		public void WaitIfPaused()
		{
			while (paused)
			{
				Thread.Sleep(1000);
			}
		}
	}
}
