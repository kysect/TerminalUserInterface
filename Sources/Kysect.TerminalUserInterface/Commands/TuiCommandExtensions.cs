using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.TerminalUserInterface.Tools;

namespace Kysect.TerminalUserInterface.Commands;

public static class TuiCommandExtensions
{
    public static string ToNameConverter(ITuiCommand command)
    {
        command.ThrowIfNull();

        return command.GetName();
    }
}