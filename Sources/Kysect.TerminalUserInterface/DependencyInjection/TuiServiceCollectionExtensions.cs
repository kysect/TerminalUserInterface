using Kysect.CommonLib.DependencyInjection;
using Kysect.TerminalUserInterface.Commands;
using Kysect.TerminalUserInterface.Menu;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kysect.TerminalUserInterface.DependencyInjection;

public static class TuiServiceCollectionExtensions
{
    public static IServiceCollection AddUserActionSelectionMenus(this IServiceCollection services, params Assembly[] assemblies)
    {
        return services
            .AddAllImplementationOf<ITuiCommand>(assemblies)
            .AddAllImplementationOf<ITuiMenu>(assemblies);
    }

}