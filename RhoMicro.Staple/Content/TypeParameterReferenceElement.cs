namespace RhoMicro.Staple.Content;

/// <summary>
/// Represents a <c>typeparamref</c> element.
/// </summary>
public sealed class TypeParameterReferenceElement(
    String name,
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <summary>
    /// Gets the type parameter name.
    /// </summary>
    public String Name { get; } = ArgumentNullException.ThrowIfNull(name);

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitTypeParameterReference(this);
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        var result = visitor.VisitTypeParameterReference(this);
        return result;
    }
}
