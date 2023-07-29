using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public class ThirdMenu : ITuiMenu
{
    public string Name => "Third menu";

    public IReadOnlyCollection<ITuiCommand> GetMenuItems()
    {
        return new ITuiCommand[] {new FirstCommand(), new SecondCommand(), };
    }
}