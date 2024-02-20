namespace Kysect.TerminalUserInterface.Navigation.Commands;

public class ExecuteCommandNavigationAction : IMenuNavigationAction
{
    public string Name { get; }
    public Type CommandType { get; }

    public ExecuteCommandNavigationAction(string name, Type commandType)
    {
        Name = name;
        CommandType = commandType;
    }
}