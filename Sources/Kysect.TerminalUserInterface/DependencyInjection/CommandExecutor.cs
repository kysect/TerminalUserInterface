using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.TerminalUserInterface.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Kysect.TerminalUserInterface.DependencyInjection;

public class CommandExecutor : ICommandExecutor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CommandExecutor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void Execute(Type commandType)
    {
        using IServiceScope serviceScope = _serviceScopeFactory.CreateScope();
        ITuiCommand command = serviceScope.ServiceProvider.GetRequiredService(commandType).To<ITuiCommand>();
        command.Execute();
    }
}