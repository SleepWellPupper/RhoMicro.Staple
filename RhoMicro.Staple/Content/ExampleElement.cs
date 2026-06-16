// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;

/// <summary>
/// Represents an <c>example</c> element.
/// </summary>
public sealed class ExampleElement(
    ImmutableArray<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor) 
        => ArgumentNullException.Validate(visitor).VisitExample(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor) 
        => ArgumentNullException.Validate(visitor) .VisitExample(this);
}
