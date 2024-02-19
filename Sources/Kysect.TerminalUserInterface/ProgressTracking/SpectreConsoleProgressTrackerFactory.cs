using Kysect.CommonLib.ProgressTracking;

namespace Kysect.TerminalUserInterface.ProgressTracking;

public class SpectreConsoleProgressTrackerFactory : IProgressTrackerFactory, IDisposable
{
    public static SpectreConsoleProgressTrackerFactory Instance { get; } = new SpectreConsoleProgressTrackerFactory();

    private bool _disposedValue;
    private readonly List<IDisposable> _resources = new List<IDisposable>();

    public IProgressTracker Create(string operationName, int maxValue)
    {
        var progressTracker = new SpectreConsoleProgressTracker(operationName, maxValue);
        _resources.Add(progressTracker);
        return progressTracker;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _resources.ForEach(r => r.Dispose());
                _resources.Clear();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}