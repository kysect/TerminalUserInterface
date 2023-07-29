using Spectre.Console;
using System;
using System.Collections.Generic;

namespace Kysect.TerminalUserInterface.Controls.Selection;

public static class DefaultSelectionPromptFactory
{
    public static SelectionPrompt<T> CreateWithSingleSelection<T>(IReadOnlyCollection<T> elements, Func<T, string>? displaySelector = null) where T : notnull
    {
        SelectionPrompt<T> selectionPrompt = new SelectionPrompt<T>()
            .PageSize(10)
            .WrapAround()
            .AddChoices(elements);

        if (displaySelector != null)
            selectionPrompt.Converter = displaySelector;

        return selectionPrompt;
    }

    public static MultiSelectionPrompt<T> CreateWithMultipleSelection<T>(string title) where T : notnull
    {
        return new MultiSelectionPrompt<T>()
            .Title(title)
            .NotRequired() // Not required to have a favorite fruit
            .PageSize(10)
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to select, " +
                "[green]<enter>[/] to accept)[/]");
    }

}