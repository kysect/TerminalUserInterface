using Kysect.CommonLib.BaseTypes.Extensions;

namespace Kysect.TerminalUserInterface.Tools;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public class TuiNameAttribute : Attribute
{
    public string Name { get; }

    public TuiNameAttribute(string name)
    {
        Name = name.ThrowIfNull();
    }
}