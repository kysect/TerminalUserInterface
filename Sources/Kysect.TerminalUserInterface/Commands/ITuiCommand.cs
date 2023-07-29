namespace Kysect.TerminalUserInterface.Commands;

public interface ITuiCommand
{
    string Name { get; }

    void Execute();
}