
using Kysect.TerminalUserInterface.DependencyInjection;
using Kysect.TerminalUserInterface.Navigation;
using Kysect.TerminalUserInterface.Sample;
using Lunet.Extensions.Logging.SpectreConsole;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

IServiceProvider serviceProvider = CreateDependencies();

TuiMenuNavigator menuNavigator = CreateMenuNavigator(serviceProvider);

menuNavigator.Run();


IServiceProvider CreateDependencies()
{
    var serviceCollection = new ServiceCollection();

    serviceCollection.AddUserActionSelectionMenus(typeof(Program).Assembly);

    serviceCollection.AddLogging(b => b.AddSpectreConsole(new SpectreConsoleLoggerOptions()
    {
        IncludeNewLineBeforeMessage = false,
        IncludeTimestamp = true,
    }));

    return serviceCollection.BuildServiceProvider();
}

TuiMenuNavigator CreateMenuNavigator(IServiceProvider serviceProvider)
{
    ILogger logger = serviceProvider.GetRequiredService<ILogger<Program>>();

    var menuProvider = new TuiMenuProvider(serviceProvider);
    var sampleMenuInitializer = new SampleMenuInitializer(menuProvider);
    TuiMenuNavigationItem menuNavigationItem = sampleMenuInitializer.Create();
    var tuiMenuNavigator = new TuiMenuNavigator(menuNavigationItem, logger);
    return tuiMenuNavigator;
}