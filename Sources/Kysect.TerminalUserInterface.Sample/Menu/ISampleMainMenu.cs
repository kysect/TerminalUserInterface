using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public interface ISampleMainMenu : ITuiMainMenu
{
    IFirstMenu FirstMenu { get; }
    ISecondMenu SecondMenu { get; }
}