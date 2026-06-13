namespace RhoMicro.Staples.Content;

/// <summary>
/// Represents a documentation content element with children.
/// </summary>
public abstract class DocumentationContentContainerElement(
    string source,
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentNode(source)
{
    /// <summary>
    /// Gets the child nodes.
    /// </summary>
    public IReadOnlyList<DocumentationContentNode> Children { get; } = children;
}
