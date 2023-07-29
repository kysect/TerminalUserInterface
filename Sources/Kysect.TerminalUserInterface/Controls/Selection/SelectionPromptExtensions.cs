using Spectre.Console;
using System;

namespace Kysect.TerminalUserInterface.Controls.Selection;

public static class SelectionPromptExtensions
{
    public const string NullValueMessage = "Cancel";

    public static T? PromptWithCancellation<T>(this SelectionPrompt<T> selectionPrompt)
        where T : class
    {
        // KB: we use null value for cancellation
        selectionPrompt.AddChoice(default!);

        Func<T, string>? previousConverter = selectionPrompt.Converter;

        if (previousConverter is null)
            selectionPrompt.Converter = value => value is null ? NullValueMessage : value.ToString();
        else
            selectionPrompt.Converter = value => value is null ? NullValueMessage : previousConverter(value);

        return AnsiConsole.Prompt(selectionPrompt);
    }
}