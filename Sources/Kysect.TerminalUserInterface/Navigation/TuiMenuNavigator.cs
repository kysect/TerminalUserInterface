using Kysect.CommonLib.Exceptions;
using Kysect.CommonLib.Reflection.TypeCache;
using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Menu;
using Kysect.TerminalUserInterface.Navigation.Commands;
using Microsoft.Extensions.Logging;

namespace Kysect.TerminalUserInterface.Navigation;

public class TuiMenuNavigator
{
    private readonly ICommandExecutor _commandExecutor;
    private readonly ITuiNavigationActionSelector _actionSelector;
    private readonly ILogger _logger;
    private readonly Stack<TuiMenuNavigationItem> _menuNavigationStack;

    public static TuiMenuNavigator Create<T>(ICommandExecutor commandExecutor, ILogger logger) where T : ITuiMainMenu
    {
        return new TuiMenuNavigator(new TuiMenuNavigationItem("Main menu", TypeInstanceCache<T>.Instance), commandExecutor, logger, new AnsiConsoleTuiNavigationActionSelector());
    }

    public TuiMenuNavigator(TuiMenuNavigationItem rootItem, ICommandExecutor commandExecutor, ILogger logger, ITuiNavigationActionSelector actionSelector)
    {
        _commandExecutor = commandExecutor;
        _actionSelector = actionSelector;
        _logger = logger;

        _menuNavigationStack = new Stack<TuiMenuNavigationItem>();
        _menuNavigationStack.Push(rootItem);
    }

    public void Run()
    {
        while (_menuNavigationStack.Any())
        {
            ProcessOneAction();
        }
    }

    public void ProcessOneAction()
    {
        IReadOnlyCollection<IMenuNavigationAction> commandList = GetCommandsForPeekItem(_menuNavigationStack);
        IMenuNavigationAction selectedCommand = _actionSelector.Select(commandList);

        if (selectedCommand is NavigateToSubmenuTuiCommand navigationCommand)
        {
            _menuNavigationStack.Push(navigationCommand.Submenu);
            return;
        }

        if (selectedCommand is ReturnTuiCommand)
        {
            _menuNavigationStack.Pop();
            return;
        }

        if (selectedCommand is ExecuteCommandNavigationAction executeCommand)
        {
            ExecuteCommand(executeCommand);
            return;
        }

        throw SwitchDefaultExceptions.OnUnexpectedType(selectedCommand);
    }

    private IReadOnlyCollection<IMenuNavigationAction> GetCommandsForPeekItem(Stack<TuiMenuNavigationItem> menuNavigationStack)
    {
        TuiMenuNavigationItem currentMenu = menuNavigationStack.Peek();
        bool isCurrentElementRoot = menuNavigationStack.Count == 1;

        var commandList = new List<IMenuNavigationAction>();
        commandList.AddRange(
            TuiMenuNavigationExtensions
                .GetMenuCommands(currentMenu.MenuType)
                .Select(i => new ExecuteCommandNavigationAction(i.Name, i.CommandType))
                .ToList());

        commandList.AddRange(
            TuiMenuNavigationExtensions
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
            _commandExecutor.Execute(selectedCommand.CommandType);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while execution command " + selectedCommand.Name);
        }

        _logger.LogInformation("Stop command: " + selectedCommand.Name);
    }
}