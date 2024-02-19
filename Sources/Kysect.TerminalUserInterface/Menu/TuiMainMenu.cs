using Kysect.TerminalUserInterface.Commands;

namespace Kysect.TerminalUserInterface.Menu;

public class TuiMainMenu : ITuiMenu
{
    public string Name => "Main menu";

    public IReadOnlyCollection<ITuiCommand> GetMenuItems()
    {
        return Array.Empty<ITuiCommand>();
    }
}