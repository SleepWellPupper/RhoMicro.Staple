namespace RhoMicro.Staple.Content;

/// <summary>
/// Represents a <c>term</c> element within a list.
/// </summary>
public sealed class ListTermElement(
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitListTerm(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitListTerm(this);
}
