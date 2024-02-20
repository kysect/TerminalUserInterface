using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Tests.Mocks;

public interface ITestTuiMenu : ITuiMenu
{
    TestTuiCommand TestTuiCommand { get; }
}