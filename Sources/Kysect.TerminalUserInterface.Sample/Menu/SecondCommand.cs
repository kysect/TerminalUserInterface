using Kysect.TerminalUserInterface.Commands;
using Spectre.Console;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public class SecondCommand : ITuiCommand
{
    public string Name => "Second command";
    public void Execute()
    {
        AnsiConsole.WriteLine(Name);
    }
}