using Kysect.TerminalUserInterface.Commands;
using Spectre.Console;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public class SecondCommand : ITuiCommand
{
    public void Execute()
    {
        AnsiConsole.WriteLine("Second");
    }
}