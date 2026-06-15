// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;

/// <summary>
/// Represents an element without a dedicated AST node type.
/// </summary>
public sealed class UnknownElement(
    String name,
    IReadOnlyDictionary<String, String> attributes,
    ImmutableArray<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <summary>
    /// Gets the element name.
    /// </summary>
    public String Name { get; } = ArgumentNullException.ThrowIfNull(name);

    /// <summary>
    /// Gets the element attributes.
    /// </summary>
    public IReadOnlyDictionary<String, String> Attributes { get; } = ArgumentNullException.ThrowIfNull(attributes);

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitUnknown(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitUnknown(this);
}
