namespace RhoMicro.Staples.Content;

/// <summary>
/// Represents a <c>br</c> element.
/// </summary>
public sealed class LineBreakElement(
    String source)
    : DocumentationContentNode(source)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitLineBreak(this);
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        var result = visitor.VisitLineBreak(this);
        return result;
    }
}
