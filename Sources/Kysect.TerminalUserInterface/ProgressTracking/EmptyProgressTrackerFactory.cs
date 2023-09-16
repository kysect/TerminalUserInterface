namespace Kysect.TerminalUserInterface.ProgressTracking;

public class EmptyProgressTrackerFactory : IProgressTrackerFactory
{
    public class EmptyProgressTracker : IProgressTracker
    {
        public void OnUpdate()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }
    }

    public IProgressTracker Create(string operationName, int maxValue)
    {
        return new EmptyProgressTracker();
    }
}