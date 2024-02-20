using Kysect.CommonLib.BaseTypes.Extensions;

namespace Kysect.TerminalUserInterface.Tools;

[AttributeUsage(AttributeTargets.Property)]
public class TuiNameAttribute : Attribute
{
    public string Name { get; }

    public TuiNameAttribute(string name)
    {
        Name = name.ThrowIfNull();
    }
}