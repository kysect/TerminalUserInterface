namespace Kysect.TerminalUserInterface.Navigation.Commands;

public class ReturnTuiCommand : INavigationTuiCommand
{
    private readonly bool _isCurrentElementRoot;

    public string Name => _isCurrentElementRoot ? "Exit" : "Return to previous menu";

    public ReturnTuiCommand(bool isCurrentElementRoot)
    {
        _isCurrentElementRoot = isCurrentElementRoot;
    }

    public void Execute()
    {
    }
}