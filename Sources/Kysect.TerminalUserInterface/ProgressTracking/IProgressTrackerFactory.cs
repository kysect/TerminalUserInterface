namespace Kysect.TerminalUserInterface.ProgressTracking;

public interface IProgressTrackerFactory
{
    IProgressTracker Create(string operationName, int maxValue);
}