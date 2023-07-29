using System.Collections.Generic;

namespace Kysect.TerminalUserInterface.Controls.Grids;

public interface IObjectGridViewDecomposer<T>
{
    IReadOnlyCollection<string> GetHeader();
    IReadOnlyCollection<string> GetValues(T instance);
}