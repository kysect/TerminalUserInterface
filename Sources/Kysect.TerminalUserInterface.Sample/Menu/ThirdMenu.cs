using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public class ThirdMenu : ITuiMenu
{
    public IReadOnlyCollection<ITuiCommand> GetMenuItems()
    {
        return new ITuiCommand[] { new FirstCommand(), new SecondCommand(), };
    }
}