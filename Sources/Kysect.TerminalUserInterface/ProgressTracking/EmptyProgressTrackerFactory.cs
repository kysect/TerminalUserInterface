namespace Kysect.TerminalUserInterface.ProgressTracking;

public class EmptyProgressTrackerFactory : IProgressTrackerFactory
{
    public class EmptyProgressTracker : IProgressTracker
    {
        public void Dispose()
        {
        }

        public void OnUpdate()
        {
        }
    }

    public IProgressTracker Create(string operationName, int maxValue)
    {
        return new EmptyProgressTracker();
    }
}