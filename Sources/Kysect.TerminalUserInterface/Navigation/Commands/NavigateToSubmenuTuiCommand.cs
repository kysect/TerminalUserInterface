namespace Kysect.TerminalUserInterface.Navigation.Commands;

public class NavigateToSubmenuTuiCommand : INavigationTuiCommand
{
    public TuiMenuNavigationItem Submenu { get; }

    public NavigateToSubmenuTuiCommand(TuiMenuNavigationItem submenu)
    {
        Submenu = submenu;
    }

    public void Execute()
    {
    }
}