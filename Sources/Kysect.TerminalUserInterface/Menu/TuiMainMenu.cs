using Kysect.TerminalUserInterface.Commands;
using System;
using System.Collections.Generic;

namespace Kysect.TerminalUserInterface.Menu;

public class TuiMainMenu : ITuiMenu
{
    public string Name => "Main menu";

    public IReadOnlyCollection<ITuiCommand> GetMenuItems()
    {
        return Array.Empty<ITuiCommand>();
    }
}