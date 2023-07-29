using Spectre.Console;
using Spectre.Console.Rendering;
using System;

namespace Kysect.TerminalUserInterface.ProgressTracking;

public sealed class ItemProcessedColumn : ProgressColumn
{
    /// <inheritdoc/>
    public override IRenderable Render(RenderOptions options, ProgressTask task, TimeSpan deltaTime)
    {
        return task.IsFinished
            ? new Markup($"[green]{task.MaxValue}[/]")
            : new Markup($"[grey]{task.Value}[/] / [grey]{task.MaxValue}[/] ");
    }
}