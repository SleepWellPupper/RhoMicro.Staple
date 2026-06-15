// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;
using System.Diagnostics;

/// <summary>
/// Represents a <c>c</c> element.
/// </summary>
public sealed class InlineCodeElement(
    ImmutableArray<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor) 
        => ArgumentNullException.ThrowIfNull(visitor).VisitInlineCode(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitInlineCode(this);
}
