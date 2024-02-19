using Kysect.CommonLib.Exceptions;
using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Controls.Selection;
using Kysect.TerminalUserInterface.Navigation.Commands;
using Kysect.TerminalUserInterface.Tools;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace Kysect.TerminalUserInterface.Navigation;

public class TuiMenuNavigator
{
    private readonly TuiMenuNavigationItem _rootItem;
    private readonly ILogger _logger;

    public TuiMenuNavigator(TuiMenuNavigationItem rootItem, ILogger logger)
    {
        _rootItem = rootItem;
        _logger = logger;
    }

    public void Run()
    {
        var menuNavigationStack = new Stack<TuiMenuNavigationItem>();
        menuNavigationStack.Push(_rootItem);

        while (menuNavigationStack.Any())
        {
            IReadOnlyCollection<ITuiCommand> commandList = GetCommandsForPeekItem(menuNavigationStack);
            SelectionPrompt<ITuiCommand> selector = DefaultSelectionPromptFactory.CreateWithSingleSelection(commandList, TuiCommandExtensions.ToNameConverter);
            ITuiCommand selectedCommand = AnsiConsole.Prompt(selector);

            if (selectedCommand is INavigationTuiCommand navigationCommand)
            {
                ExecuteNavigationCommand(navigationCommand, menuNavigationStack);
            }
            else
            {
                ExecuteCommand(selectedCommand);
            }
        }
    }

    private static IReadOnlyCollection<ITuiCommand> GetCommandsForPeekItem(Stack<TuiMenuNavigationItem> menuNavigationStack)
    {
        TuiMenuNavigationItem currentMenu = menuNavigationStack.Peek();
        bool isCurrentElementRoot = menuNavigationStack.Count == 1;

        var commandList = new List<ITuiCommand>();
        commandList.AddRange(currentMenu.Menu.GetMenuItems());
        commandList.AddRange(currentMenu.NavigationLinks.Select(n => new NavigateToSubmenuTuiCommand(n)));
        commandList.Add(isCurrentElementRoot ? new ExitTuiCommand() : new ReturnTuiCommand());
        return commandList;
    }

    private void ExecuteCommand(ITuiCommand selectedCommand)
    {
        _logger.LogInformation("Start command: " + selectedCommand.GetName());

        try
        {
            selectedCommand.Execute();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while execution command " + selectedCommand.GetName());
        }

        _logger.LogInformation("Stop command: " + selectedCommand.GetName());
    }

    private void ExecuteNavigationCommand(INavigationTuiCommand navigationTuiCommand, Stack<TuiMenuNavigationItem> menuNavigationStack)
    {
        _logger.LogDebug("Select navigation command " + navigationTuiCommand.GetName());

        switch (navigationTuiCommand)
        {
            case ReturnTuiCommand navigateReturnUserCommand:
                menuNavigationStack.Pop();
                break;

            case NavigateToSubmenuTuiCommand navigateToSubmenuUserCommand:
                menuNavigationStack.Push(navigateToSubmenuUserCommand.Submenu);
                break;

            default:
                throw SwitchDefaultExceptions.OnUnexpectedType(navigationTuiCommand);
        }
    }
}