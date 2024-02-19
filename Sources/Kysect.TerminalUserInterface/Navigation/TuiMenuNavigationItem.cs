using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Navigation;

public class TuiMenuNavigationItem
{
    public ITuiMenu Menu { get; }
    public IReadOnlyCollection<TuiMenuNavigationItem> NavigationLinks { get; }

    public TuiMenuNavigationItem(ITuiMenu menu, IReadOnlyCollection<TuiMenuNavigationItem> navigationLinks)
    {
        Menu = menu;
        NavigationLinks = navigationLinks;
    }
}