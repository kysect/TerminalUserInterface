using Kysect.CommonLib.Collections.Extensions;
using Kysect.CommonLib.Reflection;
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
            .AddSingleton<ICommandExecutor, CommandExecutor>()
            .AddAllImplementationOfAsScoped<ITuiCommand>(assemblies)
            .AddAllImplementationOfAsScoped<ITuiMenu>(assemblies);
    }

    // TODO: add to CommonLib?
    private static IServiceCollection AddAllImplementationOfAsScoped<T>(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies.IsEmpty())
            throw new ArgumentException("No assemblies was specified.");

        IReadOnlyCollection<Type> allImplementationOf = AssemblyReflectionTraverser.GetAllImplementationOf<T>(assemblies);
        foreach (Type type in allImplementationOf)
            services.AddScoped(type);

        return services;
    }
}