using System.Text;

namespace Kysect.TerminalUserInterface.Tools;

public static class TerminalGlobalConfigurator
{
    public static void PrepareTerminal()
    {
        Console.OutputEncoding = Encoding.UTF8;
    }
}