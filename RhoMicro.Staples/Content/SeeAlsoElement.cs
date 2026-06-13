namespace RhoMicro.Staples.Content;

/// <summary>
/// Represents a <c>seealso</c> element.
/// </summary>
public sealed class SeeAlsoElement(
    String cref,
    String href,
    String langword,
    String name,
    String source,
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(source, children)
{
    /// <summary>
    /// Gets the <c>cref</c> attribute value.
    /// </summary>
    public String Cref { get; } = ArgumentNullException.Validate(cref);

    /// <summary>
    /// Gets the <c>href</c> attribute value.
    /// </summary>
    public String Href { get; } = ArgumentNullException.Validate(href);

    /// <summary>
    /// Gets the <c>langword</c> attribute value.
    /// </summary>
    public String Langword { get; } = ArgumentNullException.Validate(langword);

    /// <summary>
    /// Gets the <c>name</c> attribute value.
    /// </summary>
    public String Name { get; } = ArgumentNullException.Validate(name);

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitSeeAlso(this);
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        var result = visitor.VisitSeeAlso(this);
        return result;
    }
}
