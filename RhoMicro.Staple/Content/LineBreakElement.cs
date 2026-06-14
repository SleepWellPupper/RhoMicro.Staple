namespace RhoMicro.Staple.Content;

/// <summary>
/// Represents a <c>br</c> element.
/// </summary>
public sealed class LineBreakElement : DocumentationContentNode
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitLineBreak(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitLineBreak(this);
}
