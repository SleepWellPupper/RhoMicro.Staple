namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;

/// <summary>
/// Represents a <c>listheader</c> element.
/// </summary>
public sealed class ListHeaderElement(
    ImmutableArray<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitListHeader(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitListHeader(this);
}
