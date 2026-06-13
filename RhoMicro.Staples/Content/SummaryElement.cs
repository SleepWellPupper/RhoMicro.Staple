namespace RhoMicro.Staples.Content;

/// <summary>
/// Represents a <c>summary</c> element.
/// </summary>
public sealed class SummaryElement(
    String source,
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(source, children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitSummary(this);
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        var result = visitor.VisitSummary(this);
        return result;
    }
}
