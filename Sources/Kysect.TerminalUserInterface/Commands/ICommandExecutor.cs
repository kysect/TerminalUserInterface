namespace Kysect.TerminalUserInterface.Commands;

public interface ICommandExecutor
{
    public void Execute(Type commandType);
}