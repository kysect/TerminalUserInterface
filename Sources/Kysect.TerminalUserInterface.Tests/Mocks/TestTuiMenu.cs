using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Tests.Mocks;

public class TestTuiMenu : ITuiMenu
{
    public string Name => nameof(TestTuiMenu);
    public IReadOnlyCollection<ITuiCommand> GetMenuItems()
    {
        return new[] { new TestTuiCommand() };
    }
}