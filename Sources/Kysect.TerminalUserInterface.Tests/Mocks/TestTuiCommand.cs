using Kysect.TerminalUserInterface.Commands;

namespace Kysect.TerminalUserInterface.Tests.Mocks;

public class TestTuiCommand : ITuiCommand
{
    public string Name => nameof(TestTuiCommand);
    public void Execute()
    {
    }
}