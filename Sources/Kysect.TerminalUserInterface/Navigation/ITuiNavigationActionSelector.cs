using Kysect.TerminalUserInterface.Navigation.Commands;

namespace Kysect.TerminalUserInterface.Navigation;

public interface ITuiNavigationActionSelector
{
    IMenuNavigationAction Select(IReadOnlyCollection<IMenuNavigationAction> actions);
}