namespace Kysect.TerminalUserInterface.ProgressTracking;

public class SpectreConsoleProgressTrackerFactory : IProgressTrackerFactory
{
    public static SpectreConsoleProgressTrackerFactory Instance { get; } = new SpectreConsoleProgressTrackerFactory();

    public IProgressTracker Create(string operationName, int maxValue)
    {
        return new SpectreConsoleProgressTracker(operationName, maxValue);
    }
}