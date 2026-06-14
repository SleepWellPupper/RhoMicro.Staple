namespace RhoMicro.Staple.Content;

/// <summary>
/// Represents a <c>seealso</c> element.
/// </summary>
public sealed class SeeAlsoElement(
    String cref,
    String href,
    String langword,
    String name,
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <summary>
    /// Gets the <c>cref</c> attribute value.
    /// </summary>
    public String Cref { get; } = ArgumentNullException.ThrowIfNull(cref);

    /// <summary>
    /// Gets the <c>href</c> attribute value.
    /// </summary>
    public String Href { get; } = ArgumentNullException.ThrowIfNull(href);

    /// <summary>
    /// Gets the <c>langword</c> attribute value.
    /// </summary>
    public String Langword { get; } = ArgumentNullException.ThrowIfNull(langword);

    /// <summary>
    /// Gets the <c>name</c> attribute value.
    /// </summary>
    public String Name { get; } = ArgumentNullException.ThrowIfNull(name);

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitSeeAlso(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitSeeAlso(this);
}
