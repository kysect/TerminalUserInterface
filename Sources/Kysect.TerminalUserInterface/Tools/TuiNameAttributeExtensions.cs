using Kysect.CommonLib.Reflection;
using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Tools;

public static class TuiNameAttributeExtensions
{
    private static readonly ReflectionAttributeFinder ReflectionAttributeFinder = new ReflectionAttributeFinder();

    public static string GetName(this ITuiMenu menu)
    {
        TuiNameAttribute attributeFromInstance = ReflectionAttributeFinder.GetAttributeFromInstance<TuiNameAttribute>(menu);
        return attributeFromInstance.Name;
    }

    public static string GetName(this ITuiCommand command)
    {
        TuiNameAttribute attributeFromInstance = ReflectionAttributeFinder.GetAttributeFromInstance<TuiNameAttribute>(command);
        return attributeFromInstance.Name;
    }
}