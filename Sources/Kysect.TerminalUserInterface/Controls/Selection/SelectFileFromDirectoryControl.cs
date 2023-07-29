using System.IO;
using System.Linq;

namespace Kysect.TerminalUserInterface.Controls.Selection;

public class SelectFileFromDirectoryControl
{
    public static bool TrySelectFile(string directory, out string? file)
    {
        var filesInDirectory = Directory.EnumerateFiles(directory).ToList();

        file = DefaultSelectionPromptFactory
            .CreateWithSingleSelection(filesInDirectory)
            .PromptWithCancellation();

        if (file is null)
            return false;

        file = Path.Combine(directory, file);
        return true;
    }
}