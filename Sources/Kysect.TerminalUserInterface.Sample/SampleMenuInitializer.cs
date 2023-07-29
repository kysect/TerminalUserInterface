using Kysect.TerminalUserInterface.DependencyInjection;
using Kysect.TerminalUserInterface.Navigation;
using Kysect.TerminalUserInterface.Sample.Menu;

namespace Kysect.TerminalUserInterface.Sample;

public class SampleMenuInitializer
{
    private readonly TuiMenuProvider _menuProvider;

    public SampleMenuInitializer(TuiMenuProvider menuProvider)
    {
        _menuProvider = menuProvider;
    }

    public TuiMenuNavigationItem Create()
    {
        var builder = new TuiMenuNavigationBuilder(_menuProvider);

        builder
            .WithSubMenu<FirstMenu>()
            .WithSubMenu<SecondMenu>(b => b
                .WithSubMenu<ThirdMenu>());

        return builder.Build();
    }
}