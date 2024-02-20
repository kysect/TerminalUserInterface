
using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.DependencyInjection;
using Kysect.TerminalUserInterface.Navigation;
using Kysect.TerminalUserInterface.Sample.Menu;
using Lunet.Extensions.Logging.SpectreConsole;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

IServiceProvider serviceProvider = new ServiceCollection()
    .AddUserActionSelectionMenus(typeof(Program).Assembly)
    .AddLogging(b => b.AddSpectreConsole(new SpectreConsoleLoggerOptions() { IncludeNewLineBeforeMessage = false, IncludeTimestamp = true, }))
    .BuildServiceProvider();

ILogger logger = serviceProvider.GetRequiredService<ILogger<Program>>();
ICommandExecutor commandExecutor = serviceProvider.GetRequiredService<ICommandExecutor>();
TuiMenuNavigator menuNavigator = TuiMenuNavigator.Create<ISampleMainMenu>(commandExecutor, logger);

menuNavigator.Run();
