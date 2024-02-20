using Kysect.TerminalUserInterface.Menu;
using Kysect.TerminalUserInterface.Tools;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public interface IFirstMenu : ITuiMenu
{
    [TuiName("First command")]
    FirstCommand FirstCommand { get; }
}