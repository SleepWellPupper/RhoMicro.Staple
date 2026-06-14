namespace RhoMicro.Staple.Content;

/// <summary>
/// Represents a documentation content element with children.
/// </summary>
public abstract class DocumentationContentContainerElement(
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentNode
{
    /// <summary>
    /// Gets the child nodes.
    /// </summary>
    public IReadOnlyList<DocumentationContentNode> Children { get; } = ArgumentNullException.ThrowIfNull(children);
}
