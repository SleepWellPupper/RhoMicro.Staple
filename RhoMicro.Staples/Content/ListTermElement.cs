namespace RhoMicro.Staples.Content;

/// <summary>
/// Represents a <c>term</c> element within a list.
/// </summary>
public sealed class ListTermElement(
    String source,
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(source, children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitListTerm(this);
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        var result = visitor.VisitListTerm(this);
        return result;
    }
}
