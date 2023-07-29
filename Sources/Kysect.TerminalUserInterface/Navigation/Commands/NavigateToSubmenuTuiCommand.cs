namespace Kysect.TerminalUserInterface.Navigation.Commands;

public class NavigateToSubmenuTuiCommand : INavigationTuiCommand
{
    public TuiMenuNavigationItem Submenu { get; }
    public string Name => "Go to " + Submenu.Menu.Name;

    public NavigateToSubmenuTuiCommand(TuiMenuNavigationItem submenu)
    {
        Submenu = submenu;
    }

    public void Execute()
    {
    }
}