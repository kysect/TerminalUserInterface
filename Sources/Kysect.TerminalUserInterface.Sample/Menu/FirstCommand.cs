using Kysect.TerminalUserInterface.Commands;
using Spectre.Console;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public class FirstCommand : ITuiCommand
{
    public string Name => "First command";
    public void Execute()
    {
        AnsiConsole.WriteLine(Name);
    }
}