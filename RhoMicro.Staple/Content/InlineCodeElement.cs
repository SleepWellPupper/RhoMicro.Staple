namespace RhoMicro.Staple.Content;

/// <summary>
/// Represents a <c>c</c> element.
/// </summary>
public sealed class InlineCodeElement(
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitInlineCode(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitInlineCode(this);
}
