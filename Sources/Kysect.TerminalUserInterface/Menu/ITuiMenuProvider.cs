namespace Kysect.TerminalUserInterface.Menu;

public interface ITuiMenuProvider
{
    public T GetMenu<T>() where T : ITuiMenu;
}