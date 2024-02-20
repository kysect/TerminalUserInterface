using Kysect.TerminalUserInterface.Commands;

namespace Kysect.TerminalUserInterface.Menu;

public interface ITuiMenuProvider
{
    public ITuiCommand GetCommand(Type commandType);
}