namespace RhoMicro.Staples.Content;

/// <summary>
/// Represents an element without a dedicated AST node type.
/// </summary>
public sealed class UnknownElement(
    String name,
    IReadOnlyDictionary<String, String> attributes,
    String source,
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(source, children)
{
    /// <summary>
    /// Gets the element name.
    /// </summary>
    public String Name { get; } = ArgumentNullException.Validate(name);

    /// <summary>
    /// Gets the element attributes.
    /// </summary>
    public IReadOnlyDictionary<String, String> Attributes { get; } = ArgumentNullException.Validate(attributes);

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitUnknown(this);
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        var result = visitor.VisitUnknown(this);
        return result;
    }
}
