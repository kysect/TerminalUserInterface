namespace Kysect.TerminalUserInterface.Navigation.Commands;

public class ReturnTuiCommand : IMenuNavigationAction
{
    public string Name { get; }

    public ReturnTuiCommand(string name)
    {
        Name = name;
    }
}