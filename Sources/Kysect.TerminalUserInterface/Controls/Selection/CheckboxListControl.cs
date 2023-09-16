using Kysect.CommonLib.Reflection;
using Kysect.CommonLib.Reflection.TypeCache;
using Kysect.TerminalUserInterface.Tools;
using Spectre.Console;
using System.Collections.Generic;
using System.Reflection;

namespace Kysect.TerminalUserInterface.Controls.Selection;

public static class CheckboxListControl
{
    public static T Ask<T>(string title) where T : notnull
    {
        MultiSelectionPrompt<string> multiSelectionPrompt = DefaultSelectionPromptFactory.CreateWithMultipleSelection<string>(title);

        foreach (PropertyInfo publicProperty in TypeInstanceCache<T>.GetPublicProperties())
        {
            if (publicProperty.PropertyType != typeof(bool))
                throw new TerminalInterfaceFrameworkException($"Cannot create CheckBox list from type {typeof(T)}. Type has non bool attribute {publicProperty.Name}");

            multiSelectionPrompt.AddChoice(publicProperty.Name);
        }

        List<string> selectedCheckbox = AnsiConsole.Prompt(multiSelectionPrompt);

        var reflectionInstanceInitializer = new ReflectionInstanceInitializer<T>();

        foreach (string checkboxName in selectedCheckbox)
            reflectionInstanceInitializer.Set(checkboxName, true);

        return reflectionInstanceInitializer.GetValue();
    }
}