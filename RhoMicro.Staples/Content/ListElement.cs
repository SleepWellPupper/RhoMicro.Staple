namespace RhoMicro.Staples.Content;

/// <summary>
/// Represents a <c>list</c> element.
/// </summary>
public sealed class ListElement(
    String type,
    String source,
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(source, children)
{
    /// <summary>
    /// Gets the list type.
    /// </summary>
    public String Type { get; } = ArgumentNullException.Validate(type);

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitList(this);
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        var result = visitor.VisitList(this);
        return result;
    }
}
