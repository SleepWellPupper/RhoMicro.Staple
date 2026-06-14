// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;

/// <summary>
/// Represents a <c>returns</c> element.
/// </summary>
public sealed class ReturnsElement(
    ImmutableArray<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitReturns(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitReturns(this);
}
