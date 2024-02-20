using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Reflection.TypeCache;
using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Menu;
using System.Reflection;

namespace Kysect.TerminalUserInterface.Navigation;

public static class TuiMenuNavigationExtensions
{
    public static IReadOnlyCollection<TuiMenuNavigationItem> GetMenuSubmenu(Type menuType)
    {
        menuType.ThrowIfNull();

        var result = new List<TuiMenuNavigationItem>();
        foreach (var property in menuType.GetProperties())
        {
            if (TypeInstanceCache<ITuiMenu>.Instance.IsAssignableFrom(property.PropertyType))
            {
                string propertyName = property.Name;
                TuiNameAttribute? nameAttribute = property.GetCustomAttribute<TuiNameAttribute>();
                if (nameAttribute is not null)
                    propertyName = nameAttribute.Name;

                result.Add(new TuiMenuNavigationItem(propertyName, property.PropertyType));
            }
        }

        return result;
    }

    public static IReadOnlyCollection<TuiMenuCommandElement> GetMenuCommands(Type menuType)
    {
        menuType.ThrowIfNull();

        var result = new List<TuiMenuCommandElement>();
        foreach (var property in menuType.GetProperties())
        {
            if (TypeInstanceCache<ITuiCommand>.Instance.IsAssignableFrom(property.PropertyType))
            {
                string propertyName = property.Name;
                TuiNameAttribute? nameAttribute = property.GetCustomAttribute<TuiNameAttribute>();
                if (nameAttribute is not null)
                    propertyName = nameAttribute.Name;

                result.Add(new TuiMenuCommandElement(propertyName, property.PropertyType));
            }
        }

        return result;
    }
}
