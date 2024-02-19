using Kysect.TerminalUserInterface.Commands;

namespace Kysect.TerminalUserInterface.Menu;

public interface ITuiMenu
{
    IReadOnlyCollection<ITuiCommand> GetMenuItems();
}