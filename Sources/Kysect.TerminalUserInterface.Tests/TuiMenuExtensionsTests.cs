using FluentAssertions;
using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Menu;
using Kysect.TerminalUserInterface.Tools;
using Spectre.Console;

namespace Kysect.TerminalUserInterface.Tests;

public class TuiMenuExtensionsTests
{
    [TuiName("First command")]
    public class FirstCommand : ITuiCommand
    {
        public void Execute()
        {
            AnsiConsole.WriteLine("First");
        }
    }

    public interface ITestMenu : ITuiMenu
    {
        FirstCommand First { get; }
    }

    [Fact]
    public void GetMenuItems_MenuWithCommand_ReturnCommand()
    {
        Type menuType = typeof(ITestMenu);

        IReadOnlyCollection<TuiMenuCommandElement> items = TuiMenuExtensions.GetMenuCommands(menuType);

        items.Should().BeEquivalentTo([new TuiMenuCommandElement("First", typeof(FirstCommand))]);
    }
}