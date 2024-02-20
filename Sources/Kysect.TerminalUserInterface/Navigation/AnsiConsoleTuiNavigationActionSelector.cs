using Kysect.TerminalUserInterface.Controls.Selection;
using Kysect.TerminalUserInterface.Navigation.Commands;
using Spectre.Console;

namespace Kysect.TerminalUserInterface.Navigation;

public class AnsiConsoleTuiNavigationActionSelector : ITuiNavigationActionSelector
{
    public IMenuNavigationAction Select(IReadOnlyCollection<IMenuNavigationAction> actions)
    {
        SelectionPrompt<IMenuNavigationAction> selector = DefaultSelectionPromptFactory.CreateWithSingleSelection(actions, c => c.Name);
        return AnsiConsole.Prompt(selector);
    }
}