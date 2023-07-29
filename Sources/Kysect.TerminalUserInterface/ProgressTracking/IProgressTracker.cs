using System;

namespace Kysect.TerminalUserInterface.ProgressTracking;

public interface IProgressTracker : IDisposable
{
    void OnUpdate();
}