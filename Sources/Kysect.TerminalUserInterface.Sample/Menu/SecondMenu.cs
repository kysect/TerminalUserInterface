using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public class SecondMenu : ITuiMenu
{
    public string Name => "Second menu";

    public IReadOnlyCollection<ITuiCommand> GetMenuItems()
    {
        return new[] { new SecondCommand(), };
    }
}