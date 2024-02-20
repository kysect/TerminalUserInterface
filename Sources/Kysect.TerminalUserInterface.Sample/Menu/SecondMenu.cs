using Kysect.TerminalUserInterface.Menu;
using Kysect.TerminalUserInterface.Tools;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public interface ISecondMenu : ITuiMenu
{
    [TuiName("Third menu")]
    IThirdMenu ThirdMenu { get; }
    [TuiName("Second command")]
    SecondCommand SecondCommand { get; }
}