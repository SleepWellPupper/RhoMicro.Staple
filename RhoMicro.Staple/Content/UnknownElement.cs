namespace RhoMicro.Staple.Content;

/// <summary>
/// Represents an element without a dedicated AST node type.
/// </summary>
public sealed class UnknownElement(
    String name,
    IReadOnlyDictionary<String, String> attributes,
    IReadOnlyList<DocumentationContentNode> children)
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
