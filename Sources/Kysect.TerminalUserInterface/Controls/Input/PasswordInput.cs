using Spectre.Console;

namespace Kysect.TerminalUserInterface.Controls.Input;

public class PasswordInput
{
    public static string Prompt()
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>("Enter [green]password[/]:")
                .PromptStyle("red")
                .Secret());
    }
}