using Kysect.CommonLib.BaseTypes.Extensions;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Kysect.TerminalUserInterface.ProgressTracking;

public sealed class ItemProcessedColumn : ProgressColumn
{
    /// <inheritdoc/>
    public override IRenderable Render(RenderOptions options, ProgressTask task, TimeSpan deltaTime)
    {
        task.ThrowIfNull();

        return task.IsFinished
            ? new Markup($"[green]{task.MaxValue}[/]")
            : new Markup($"[grey]{task.Value}[/] / [grey]{task.MaxValue}[/] ");
    }
}