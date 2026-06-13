namespace RhoMicro.Staples.Content;

/// <summary>
/// Represents a <c>description</c> element within a list.
/// </summary>
public sealed class ListDescriptionElement(
    String source,
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(source, children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitListDescription(this);
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        var result = visitor.VisitListDescription(this);
        return result;
    }
}
