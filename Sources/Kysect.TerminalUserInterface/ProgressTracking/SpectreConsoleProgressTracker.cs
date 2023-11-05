using Kysect.CommonLib.ProgressTracking;
using Spectre.Console;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kysect.TerminalUserInterface.ProgressTracking;

public class SpectreConsoleProgressTracker : IProgressTracker, IDisposable
{
    private readonly AutoResetEvent _updateEvent;
    private readonly Task _runningTask;

    private bool _isFinished;
    private int _progress;
    private bool _disposedValue;

    public SpectreConsoleProgressTracker(string operationName, int maxValue)
    {
        _updateEvent = new AutoResetEvent(false);

        _progress = 0;
        _isFinished = false;

        _runningTask = AnsiConsole.Progress()
            .Columns(
                new TaskDescriptionColumn(),
                new ProgressBarColumn(),
                new PercentageColumn(),
                new ItemProcessedColumn(),
                new ElapsedTimeColumn(),
                new SpinnerColumn())
            .StartAsync(async ctx =>
            {
                int lastUpdate = 0;

                ProgressTask task = ctx.AddTask($"[green]{operationName}[/]", true, maxValue);

                await Task.Yield();

                do
                {
                    _updateEvent.WaitOne(TimeSpan.FromSeconds(1));
                    int currentValue = _progress;
                    task.Increment(currentValue - lastUpdate);
                    lastUpdate = currentValue;
                } while (_progress != maxValue && !_isFinished);

                task.Increment(maxValue - lastUpdate);
                task.StopTask();

                return Task.CompletedTask;
            });
    }

    public void OnUpdate()
    {
        Interlocked.Increment(ref _progress);
        _updateEvent.Set();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue)
            return;

        if (disposing)
        {
            _isFinished = true;

            _updateEvent.Set();
            _runningTask.Wait();

            _runningTask.Dispose();
            _updateEvent?.Dispose();
        }

        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
    }
}