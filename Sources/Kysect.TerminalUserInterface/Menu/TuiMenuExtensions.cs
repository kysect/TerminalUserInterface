using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.CommonLib.Reflection.TypeCache;
using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Navigation;

namespace Kysect.TerminalUserInterface.Menu;

public static class TuiMenuExtensions
{
    public static IReadOnlyCollection<TuiMenuNavigationItem> GetMenuSubmenu(Type menuType)
    {
        menuType.ThrowIfNull();

        return menuType
            .GetProperties()
            .Where(p => TypeInstanceCache<ITuiMenu>.Instance.IsAssignableFrom(p.PropertyType))
            .Select(property => new TuiMenuNavigationItem(property.Name, property.PropertyType))
            .ToList();
    }

    public static IReadOnlyCollection<TuiMenuCommandElement> GetMenuCommands(Type menuType)
    {
        menuType.ThrowIfNull();

        return menuType
            .GetProperties()
            .Where(p => TypeInstanceCache<ITuiCommand>.Instance.IsAssignableFrom(p.PropertyType))
            .Select(property => new TuiMenuCommandElement(property.Name, property.PropertyType))
            .ToList();
    }
}