# Terminal User Interface

Kysect.TUI - это библиотека для упрощения разработки консольных приложений с интерактивным консольным интерфейсом на библиотеки SpectreConsole.

## How to use - Navigation

Шаг 1. Описать команды, которые пользователь может выполнять. Команды должны реализовывать интерфейс ITuiCommand:

```csharp
public class FirstCommand : ITuiCommand
{
    public string Name => "First command";

    public void Execute()
    {
        AnsiConsole.WriteLine(Name);
    }
}
```

Шаг 2. Сгруппировать команды - создать меню и пункты под меню, которые будут содержать команды. Например, если есть много команд, то их можно сгруппировать:

```
Root menu (menu)
    Feature 1 menu (submenu)
        Command 1 (command)
        Command 2 (command)
        Return to previous  menu (navigation command)
    Feature 2 menu
        Command 3 (command)
        Command 4 (command)
        Feature 2 extra command submenu (menu)
            Command 5 (command)
            Return to previous  menu (navigation command)
        Return to previous  menu (navigation command)
    Exit (navigation command)
```

Для реализации такого меню нужно описать меню (навигационные команды добавляются автоматически):

```csharp
public class Feature1Menu : ITuiMenu
{
    public string Name => "First menu";

    public IReadOnlyCollection<ITuiCommand> GetMenuItems()
    {
        return new[] { new Command1(), new Command2() };
    }
}
```

И описать структуру меню:

```csharp
builder
    .WithSubMenu<Feature1Menu>()
    .WithSubMenu<Feature2Menu>(b => b
        .WithSubMenu<Feature2ExtraMenu>());
```

По итогам при запуске будет отображаться такое меню:

Такая инициализация создаст такое меню:

```
> Go to First menu
  Go to Second menu
  Exit
```

Взаимодействие с меню происходит по средствам перемещением курсора стрелками и выбором пробелом.