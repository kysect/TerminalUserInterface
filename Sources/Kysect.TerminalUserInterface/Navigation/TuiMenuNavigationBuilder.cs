using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.TerminalUserInterface.Menu;
using System;
using System.Collections.Generic;

namespace Kysect.TerminalUserInterface.Navigation;

public class TuiMenuNavigationBuilder
{
    private readonly ITuiMenuProvider _menuProvider;
    private readonly List<TuiMenuNavigationItem> _childNavigationItems;

    private ITuiMenu _rootMenu;

    public TuiMenuNavigationBuilder(ITuiMenuProvider menuProvider)
    {
        _menuProvider = menuProvider;

        _rootMenu = new TuiMainMenu();
        _childNavigationItems = new List<TuiMenuNavigationItem>();
    }

    public TuiMenuNavigationBuilder WithMainMenu<T>() where T : ITuiMenu
    {
        _rootMenu = _menuProvider.GetMenu<T>();
        return this;
    }

    public TuiMenuNavigationBuilder WithSubMenu<T>() where T : ITuiMenu
    {
        _childNavigationItems.Add(new TuiMenuNavigationItem(_menuProvider.GetMenu<T>(), Array.Empty<TuiMenuNavigationItem>()));
        return this;
    }

    public TuiMenuNavigationBuilder WithSubMenu<T>(Action<TuiMenuNavigationBuilder> innerBuilderModification) where T : ITuiMenu
    {
        innerBuilderModification.ThrowIfNull();

        var innerBuilder = new TuiMenuNavigationBuilder(_menuProvider);
        innerBuilder = innerBuilder.WithMainMenu<T>();
        innerBuilderModification(innerBuilder);

        _childNavigationItems.Add(innerBuilder.Build());

        return this;
    }

    public TuiMenuNavigationItem Build()
    {
        var navigatorItems = new List<TuiMenuNavigationItem>();
        foreach (TuiMenuNavigationItem navigationItem in _childNavigationItems)
        {
            navigatorItems.Add(navigationItem);
        }

        return new TuiMenuNavigationItem(_rootMenu, navigatorItems);
    }
}