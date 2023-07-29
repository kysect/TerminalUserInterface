using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.TerminalUserInterface.Menu;

namespace Kysect.TerminalUserInterface.Tests.Mocks;

public class TestTuiMenuProvider : ITuiMenuProvider
{
    public T GetMenu<T>() where T : ITuiMenu
    {
        var menu = new TestTuiMenu();
        return menu.To<T>();
    }
}