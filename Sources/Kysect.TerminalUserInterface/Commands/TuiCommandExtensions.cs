using Kysect.CommonLib.BaseTypes.Extensions;

namespace Kysect.TerminalUserInterface.Commands;

public static class TuiCommandExtensions
{
    public static string ToNameConverter(ITuiCommand command)
    {
        command.ThrowIfNull();

        return command.Name;
    }
}