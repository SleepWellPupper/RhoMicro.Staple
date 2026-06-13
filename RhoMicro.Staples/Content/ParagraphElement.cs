namespace RhoMicro.Staples.Content;

/// <summary>
/// Represents a <c>para</c> element.
/// </summary>
public sealed class ParagraphElement(
    String source,
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(source, children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitParagraph(this);
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        var result = visitor.VisitParagraph(this);
        return result;
    }
}
