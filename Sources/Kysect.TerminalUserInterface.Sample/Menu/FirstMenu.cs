using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Sample.Menu;

public interface IFirstMenu : ITuiMenu
{
    FirstCommand FirstCommand { get; }
}