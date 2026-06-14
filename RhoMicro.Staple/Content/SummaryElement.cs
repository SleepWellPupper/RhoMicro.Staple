namespace RhoMicro.Staple.Content;

/// <summary>
/// Represents a <c>summary</c> element.
/// </summary>
public sealed class SummaryElement(
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitSummary(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitSummary(this);
}
