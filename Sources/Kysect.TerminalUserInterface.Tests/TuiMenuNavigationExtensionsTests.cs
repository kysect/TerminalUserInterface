﻿using FluentAssertions;
using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Menu;
using Kysect.TerminalUserInterface.Navigation;
using Spectre.Console;

namespace Kysect.TerminalUserInterface.Tests;

public class TuiMenuNavigationExtensionsTests
{
    public class FirstCommand : ITuiCommand
    {
        public void Execute()
        {
            AnsiConsole.WriteLine("First");
        }
    }

    public interface ITestMenu : ITuiMenu
    {
        [TuiName("First command")]
        FirstCommand First { get; }
    }

    [Fact]
    public void GetMenuItems_MenuWithCommand_ReturnCommand()
    {
        Type menuType = typeof(ITestMenu);

        IReadOnlyCollection<TuiMenuCommandElement> items = TuiMenuNavigationExtensions.GetMenuCommands(menuType);

        items.Should().BeEquivalentTo([new TuiMenuCommandElement("First command", typeof(FirstCommand))]);
    }
}