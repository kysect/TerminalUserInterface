using FluentAssertions;
using Kysect.TerminalUserInterface.Navigation;
using Kysect.TerminalUserInterface.Tests.Mocks;

namespace Kysect.TerminalUserInterface.Tests;

public class TuiMenuNavigationBuilderTests
{
    private readonly TuiMenuNavigationBuilder _builder;

    public TuiMenuNavigationBuilderTests()
    {
        _builder = new TuiMenuNavigationBuilder(new TestTuiMenuProvider());

    }

    [Fact]
    public void Build_WithMenuAndSubMenu_ReturnMenuWithCorrectElements()
    {
        TuiMenuNavigationItem menu = _builder
            .WithMainMenu<TestTuiMenu>()
            .WithSubMenu<TestTuiMenu>()
            .WithSubMenu<TestTuiMenu>(b => b
                .WithSubMenu<TestTuiMenu>())
            .Build();

        menu.Menu
            .Should().BeOfType<TestTuiMenu>();

        menu.NavigationLinks
            .Should().HaveCount(2);

        menu.NavigationLinks.ElementAt(0).Menu
            .Should().BeOfType<TestTuiMenu>();

        menu.NavigationLinks.ElementAt(1).Menu
            .Should().BeOfType<TestTuiMenu>();

        menu.NavigationLinks.ElementAt(1).NavigationLinks
            .Should().HaveCount(1)
            .And.Subject.ElementAt(0).Menu.Should().BeOfType<TestTuiMenu>();
    }
}