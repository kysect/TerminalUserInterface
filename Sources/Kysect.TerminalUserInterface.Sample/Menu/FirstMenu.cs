﻿using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public class FirstMenu : ITuiMenu
{
    public string Name => "First menu";

    public IReadOnlyCollection<ITuiCommand> GetMenuItems()
    {
        return new[] { new FirstCommand(), };
    }
}