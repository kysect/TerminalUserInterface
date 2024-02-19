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

    public T GetMenu<T>() where T : ITuiMenu
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}