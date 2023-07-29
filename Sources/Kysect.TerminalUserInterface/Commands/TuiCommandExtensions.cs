namespace Kysect.TerminalUserInterface.Commands;

public class TuiCommandExtensions
{
    public static string ToNameConverter(ITuiCommand command)
    {
        return command.Name;
    }
}