// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;

/// <summary>
/// Represents a documentation content element with children.
/// </summary>
public abstract class DocumentationContentContainerElement(
    ImmutableArray<DocumentationContentNode> children)
    : DocumentationContentNode
{
    /// <summary>
    /// Gets the child nodes.
    /// </summary>
    public ImmutableArray<DocumentationContentNode> Children { get; } = children;
}
