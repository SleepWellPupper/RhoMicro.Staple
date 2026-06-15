// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;

/// <summary>
/// Represents an <c>inheritdoc</c> element.
/// </summary>
public sealed class InheritdocElement(
    String cref,
    String path,
    ImmutableArray<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <summary>
    /// Gets the <c>cref</c> attribute value.
    /// </summary>
    public String Cref { get; } = ArgumentNullException.ThrowIfNull(cref);

    /// <summary>
    /// Gets the <c>path</c> attribute value.
    /// </summary>
    public String Path { get; } = ArgumentNullException.ThrowIfNull(path);

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitInheritdoc(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitInheritdoc(this);
}
