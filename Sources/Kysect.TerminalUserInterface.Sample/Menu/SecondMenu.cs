using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public interface ISecondMenu : ITuiMenu
{
    IThirdMenu ThirdMenu { get; }
    SecondCommand SecondCommand { get; }
}