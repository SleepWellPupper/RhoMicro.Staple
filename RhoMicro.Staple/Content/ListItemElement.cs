namespace RhoMicro.Staple.Content;

/// <summary>
/// Represents an <c>item</c> element within a list.
/// </summary>
public sealed class ListItemElement(
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitListItem(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitListItem(this);
}
