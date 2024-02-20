# Terminal User Interface

Kysect.TUI - это библиотека для упрощения разработки консольных приложений с интерактивным консольным интерфейсом на основе библиотеки [SpectreConsole](https://spectreconsole.net/).

## How to use - Navigation

Шаг 1. Описать команды, которые пользователь может выполнять. Команды должны реализовывать интерфейс ITuiCommand:

```csharp
public class FirstCommand : ITuiCommand
{
    public void Execute()
    {
        AnsiConsole.WriteLine("First");
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

```csharp
public interface ISampleMainMenu : ITuiMainMenu
{
    IFirstMenu FirstMenu { get; }
    ISecondMenu SecondMenu { get; }
}

public interface IFirstMenu : ITuiMenu
{
    [TuiName("First command")]
    FirstCommand FirstCommand { get; }
}
```

По итогам при запуске будет отображаться такое меню:

Такая инициализация создаст такое меню:

```
> First menu
  Second menu
  Exit
```

Взаимодействие с меню происходит по средствам перемещением курсора стрелками и выбором пробелом.
