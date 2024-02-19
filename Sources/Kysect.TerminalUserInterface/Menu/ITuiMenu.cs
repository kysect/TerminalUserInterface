using Kysect.TerminalUserInterface.Commands;

namespace Kysect.TerminalUserInterface.Menu;

public interface ITuiMenu
{
    string Name { get; }

    IReadOnlyCollection<ITuiCommand> GetMenuItems();
}