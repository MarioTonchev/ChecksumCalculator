using ChecksumCalculator.Observer;
using ChecksumCalculator.PauseResume;

namespace ChecksumCalculator.Hashing
{
	public abstract class BaseChecksumCalculator : Observable
	{
        protected PauseController pauseController;

        public BaseChecksumCalculator(PauseController pauseController)
        {
            this.pauseController = pauseController;
        }

        public abstract string Calculate(Stream stream);
	}
}
