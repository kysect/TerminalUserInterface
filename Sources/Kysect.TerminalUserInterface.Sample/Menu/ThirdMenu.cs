using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public interface IThirdMenu : ITuiMenu
{
    FirstCommand FirstCommand { get; }
    SecondCommand SecondCommand { get; }
}