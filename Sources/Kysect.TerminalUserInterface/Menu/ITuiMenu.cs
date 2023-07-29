using Kysect.TerminalUserInterface.Commands;
using System.Collections.Generic;

namespace Kysect.TerminalUserInterface.Menu;

public interface ITuiMenu
{
    string Name { get; }

    IReadOnlyCollection<ITuiCommand> GetMenuItems();
}