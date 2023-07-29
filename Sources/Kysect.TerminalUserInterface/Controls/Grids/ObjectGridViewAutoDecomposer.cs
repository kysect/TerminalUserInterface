using Kysect.CommonLib.Reflection.TypeCache;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kysect.TerminalUserInterface.Controls.Grids;

public class ObjectGridViewAutoDecomposer<T> : IObjectGridViewDecomposer<T>
{
    public const int MaxPropertyCount = 10;

    private readonly IReadOnlyCollection<PropertyInfo> _properties;

    public ObjectGridViewAutoDecomposer()
    {
        _properties = TypeInstanceCache<T>.GetPublicProperties().Take(MaxPropertyCount).ToList();
    }

    public IReadOnlyCollection<string> GetHeader()
    {
        return _properties.Select(p => p.Name).ToList();
    }

    public IReadOnlyCollection<string> GetValues(T instance)
    {
        return _properties.Select(p => p.GetValue(instance)?.ToString() ?? "<null>").ToList();
    }
}