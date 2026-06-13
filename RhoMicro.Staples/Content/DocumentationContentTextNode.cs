namespace RhoMicro.Staples.Content;

/// <summary>
/// Represents a text node in documentation content.
/// </summary>
public sealed class DocumentationContentTextNode(String text) : DocumentationContentNode(text)
{
    /// <summary>
    /// Gets the text value.
    /// </summary>
    public String Text => Source;

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitText(this);
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);

        var result = visitor.VisitText(this);
        return result;
    }

}
