using System;

namespace Kysect.TerminalUserInterface.Tools;

public class TerminalInterfaceFrameworkException : Exception
{
    public TerminalInterfaceFrameworkException(string message) : base(message)
    {
    }
}