using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Menu;
using Microsoft.Extensions.DependencyInjection;

namespace Kysect.TerminalUserInterface.DependencyInjection;

public class TuiMenuProvider : ITuiMenuProvider
{
    private readonly IServiceProvider _serviceProvider;

    public TuiMenuProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ITuiCommand GetCommand(Type commandType)
    {
        return _serviceProvider.GetRequiredService(commandType).To<ITuiCommand>();
    }
}