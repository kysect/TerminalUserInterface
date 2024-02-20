using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public interface IFirstMenu : ITuiMenu
{
    [TuiName("First command")]
    FirstCommand FirstCommand { get; }
}