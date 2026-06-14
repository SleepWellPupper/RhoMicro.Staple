namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;

/// <summary>
/// Represents a <c>b</c> element.
/// </summary>
public sealed class BoldElement(
    ImmutableArray<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitBold(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitBold(this);
}
