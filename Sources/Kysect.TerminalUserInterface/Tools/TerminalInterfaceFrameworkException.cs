using System;

namespace Kysect.TerminalUserInterface.Tools;

public class TerminalInterfaceFrameworkException : Exception
{
    public TerminalInterfaceFrameworkException()
    {
    }

    public TerminalInterfaceFrameworkException(string message) : base(message)
    {
    }

    public TerminalInterfaceFrameworkException(string message, Exception innerException) : base(message, innerException)
    {
    }
}