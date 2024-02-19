using Kysect.TerminalUserInterface.Commands;
using Spectre.Console;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public class FirstCommand : ITuiCommand
{
    public void Execute()
    {
        AnsiConsole.WriteLine("First");
    }
}