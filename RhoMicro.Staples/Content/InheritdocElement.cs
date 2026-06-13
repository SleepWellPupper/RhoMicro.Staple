namespace RhoMicro.Staples.Content;

/// <summary>
/// Represents an <c>inheritdoc</c> element.
/// </summary>
public sealed class InheritdocElement(
    String cref,
    String path,
    String source,
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(source, children)
{
    /// <summary>
    /// Gets the <c>cref</c> attribute value.
    /// </summary>
    public String Cref { get; } = ArgumentNullException.Validate(cref);

    /// <summary>
    /// Gets the <c>path</c> attribute value.
    /// </summary>
    public String Path { get; } = ArgumentNullException.Validate(path);

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitInheritdoc(this);
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        var result = visitor.VisitInheritdoc(this);
        return result;
    }
}
