namespace ChecksumCalculator.Observer
{
	public abstract class Observable
	{
		private readonly List<IObserver> observers = new();

		public void RegisterObserver(IObserver observer)
		{
			observers.Add(observer);
		}

		protected void Notify(object sender, object message)
		{
			foreach (var observer in observers)
			{
				observer.Update(sender, message);
			}
		}
	}
}
