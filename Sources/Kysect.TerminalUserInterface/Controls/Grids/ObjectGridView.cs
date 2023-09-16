using Kysect.CommonLib.BaseTypes.Extensions;
using Kysect.TerminalUserInterface.Tools;
using Spectre.Console;
using Spectre.Console.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Kysect.TerminalUserInterface.Controls.Grids;

public class ObjectGridView<T>
{
    private readonly Style _style;
    private readonly IObjectGridViewDecomposer<T> _deconstructor;

    public ObjectGridView(IObjectGridViewDecomposer<T> deconstructor)
    {
        _deconstructor = deconstructor;

        // TODO: pass as parameter
        _style = new Style(Color.Green, Color.Black);
    }

    public void Show(IReadOnlyCollection<T> elements)
    {
        elements.ThrowIfNull();

        var grid = new Grid();

        IReadOnlyCollection<string> header = _deconstructor.GetHeader();

        IRenderable[] headerValues = header
            .Select(FormatHeader)
            .ToArray();

        grid.AddColumns(header.Count);
        grid.AddRow(headerValues);

        foreach (T element in elements)
            grid.AddRow(FormatValues(element, header));

        AnsiConsole.Write(grid);
    }

    private IRenderable FormatHeader(string header)
    {
        return new Text(header, _style);
    }

    private string[] FormatValues(T element, IReadOnlyCollection<string> header)
    {
        IReadOnlyCollection<string> values = _deconstructor
            .GetValues(element)
            .Select(Markup.Escape)
            .ToList();

        if (values.Count != header.Count)
            throw new TerminalInterfaceFrameworkException($"Cannot decompose {typeof(T)} for Object grid view. Header size does not match with element.");

        return values.ToArray();
    }
}