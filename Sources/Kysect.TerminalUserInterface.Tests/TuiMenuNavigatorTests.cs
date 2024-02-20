using FluentAssertions;
using Kysect.CommonLib.DependencyInjection.Logging;
using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.DependencyInjection;
using Kysect.TerminalUserInterface.Menu;
using Kysect.TerminalUserInterface.Navigation;
using Kysect.TerminalUserInterface.Navigation.Commands;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace Kysect.TerminalUserInterface.Tests;

public class TuiMenuNavigatorTests
{
    public interface ITestMenu : ITuiMainMenu
    {
        SecondCommand SecondCommand { get; }
        ThirdCommand ThirdCommand { get; }
        IFirstMenu FirstMenu { get; }
    }

    public interface IFirstMenu : ITuiMenu
    {
        FirstCommand FirstCommand { get; }
    }

    public class FirstCommand : ITuiCommand
    {
        public void Execute()
        {
            AnsiConsole.WriteLine("First");
        }
    }

    public class SecondCommand : ITuiCommand
    {
        public void Execute()
        {
            AnsiConsole.WriteLine("First");
        }
    }

    public class ThirdCommand : ITuiCommand
    {
        public void Execute()
        {
            AnsiConsole.WriteLine("First");
        }
    }

    private readonly TuiMenuNavigator _menuNavigator;
    private readonly TestTuiNavigationActionSelector _navigationActionSelector;


    public TuiMenuNavigatorTests()
    {
        IServiceProvider serviceProvider = new ServiceCollection()
            .AddUserActionSelectionMenus(typeof(TuiMenuNavigatorTests).Assembly)
            .BuildServiceProvider();

        ICommandExecutor commandExecutor = serviceProvider.GetRequiredService<ICommandExecutor>();

        _navigationActionSelector = new TestTuiNavigationActionSelector();
        _menuNavigator = new TuiMenuNavigator(
            new TuiMenuNavigationItem("Main menu", typeof(ITestMenu)),
            commandExecutor,
            DefaultLoggerConfiguration.CreateConsoleLogger(),
            _navigationActionSelector);
    }

    [Fact]
    public void ProcessOneAction_SelectExit_NavigatorReturnExpectedListOfCommands()
    {
        _navigationActionSelector.SelectedIndex = 3;
        _menuNavigator.Run();

        _navigationActionSelector.LastActions.ElementAt(0).Should().BeOfType<ExecuteCommandNavigationAction>()
            .And.Subject.As<ExecuteCommandNavigationAction>().CommandType.Should().Be(typeof(SecondCommand));

        _navigationActionSelector.LastActions.ElementAt(1).Should().BeOfType<ExecuteCommandNavigationAction>()
            .And.Subject.As<ExecuteCommandNavigationAction>().CommandType.Should().Be(typeof(ThirdCommand));

        _navigationActionSelector.LastActions.ElementAt(2).Should().BeOfType<NavigateToSubmenuTuiCommand>()
            .And.Subject.As<NavigateToSubmenuTuiCommand>().Submenu.MenuType.Should().Be(typeof(IFirstMenu));

        _navigationActionSelector.LastActions.ElementAt(3).Should().BeOfType<ReturnTuiCommand>();
    }

    [Fact]
    public void ProcessOneAction_SelectCommandExecution_NoThrow()
    {
        _navigationActionSelector.SelectedIndex = 0;

        _menuNavigator.Run();


    }
}

public class TestTuiNavigationActionSelector : ITuiNavigationActionSelector
{
    public int SelectedIndex { get; set; }
    public IReadOnlyCollection<IMenuNavigationAction> LastActions { get; set; } = [];

    public IMenuNavigationAction Select(IReadOnlyCollection<IMenuNavigationAction> actions)
    {
        LastActions = actions;
        actions.Should().HaveCountGreaterThan(SelectedIndex);
        return actions.ElementAt(SelectedIndex);
    }
}