using Kysect.CommonLib.BaseTypes.Extensions;

namespace Kysect.TerminalUserInterface.Navigation.Commands;

public class NavigateToSubmenuTuiCommand : IMenuNavigationAction
{
    public string Name { get; }
    public TuiMenuNavigationItem Submenu { get; }

    public NavigateToSubmenuTuiCommand(TuiMenuNavigationItem submenu)
    {
        submenu.ThrowIfNull();

        Name = $"Navigate to {submenu.Name}";
        Submenu = submenu;
    }
}