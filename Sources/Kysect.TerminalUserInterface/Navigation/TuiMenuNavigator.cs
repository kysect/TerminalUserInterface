using Kysect.CommonLib.Exceptions;
using Kysect.CommonLib.Reflection.TypeCache;
using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Controls.Selection;
using Kysect.TerminalUserInterface.Menu;
using Kysect.TerminalUserInterface.Navigation.Commands;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace Kysect.TerminalUserInterface.Navigation;

public class TuiMenuNavigator
{
    private readonly TuiMenuNavigationItem _rootItem;
    private readonly ITuiMenuProvider _menuProvider;
    private readonly ILogger _logger;

    public static TuiMenuNavigator Create<T>(ITuiMenuProvider menuProvider, ILogger logger) where T : ITuiMainMenu
    {
        return new TuiMenuNavigator(new TuiMenuNavigationItem("Main menu", TypeInstanceCache<T>.Instance), menuProvider, logger);
    }

    public TuiMenuNavigator(TuiMenuNavigationItem rootItem, ITuiMenuProvider menuProvider, ILogger logger)
    {
        _rootItem = rootItem;
        _menuProvider = menuProvider;
        _logger = logger;
    }

    public void Run()
    {
        var menuNavigationStack = new Stack<TuiMenuNavigationItem>();
        menuNavigationStack.Push(_rootItem);

        while (menuNavigationStack.Any())
        {
            IReadOnlyCollection<IMenuNavigationAction> commandList = GetCommandsForPeekItem(menuNavigationStack);
            SelectionPrompt<IMenuNavigationAction> selector = DefaultSelectionPromptFactory.CreateWithSingleSelection(commandList, c => c.Name);
            IMenuNavigationAction selectedCommand = AnsiConsole.Prompt(selector);

            if (selectedCommand is NavigateToSubmenuTuiCommand navigationCommand)
            {
                menuNavigationStack.Push(navigationCommand.Submenu);
                continue;
            }

            if (selectedCommand is ReturnTuiCommand)
            {
                menuNavigationStack.Pop();
                continue;
            }

            if (selectedCommand is ExecuteCommandNavigationAction executeCommand)
            {
                ExecuteCommand(executeCommand);
                continue;
            }

            throw SwitchDefaultExceptions.OnUnexpectedType(selectedCommand);
        }
    }

    private IReadOnlyCollection<IMenuNavigationAction> GetCommandsForPeekItem(Stack<TuiMenuNavigationItem> menuNavigationStack)
    {
        TuiMenuNavigationItem currentMenu = menuNavigationStack.Peek();
        bool isCurrentElementRoot = menuNavigationStack.Count == 1;

        var commandList = new List<IMenuNavigationAction>();
        commandList.AddRange(
            TuiMenuExtensions
                .GetMenuCommands(currentMenu.MenuType)
                .Select(i => new ExecuteCommandNavigationAction(i.Name, i.CommandType))
                .ToList());

        commandList.AddRange(
            TuiMenuExtensions
                .GetMenuSubmenu(currentMenu.MenuType)
                .Select(i => new NavigateToSubmenuTuiCommand(i))
                .ToList());

        commandList.Add(new ReturnTuiCommand(isCurrentElementRoot ? "Exit" : "Return"));
        return commandList;
    }

    private void ExecuteCommand(ExecuteCommandNavigationAction selectedCommand)
    {
        _logger.LogInformation("Start command: " + selectedCommand.Name);

        try
        {
            ITuiCommand command = _menuProvider.GetCommand(selectedCommand.CommandType);
            command.Execute();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while execution command " + selectedCommand.Name);
        }

        _logger.LogInformation("Stop command: " + selectedCommand.Name);
    }
}