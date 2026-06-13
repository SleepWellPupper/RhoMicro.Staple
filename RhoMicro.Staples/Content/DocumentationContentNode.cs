namespace RhoMicro.Staples.Content;

/// <summary>
/// Represents a node in documentation content.
/// </summary>
public abstract class DocumentationContentNode(string source)
{
    public String Source { get; } = source;

    /// <summary>
    /// Accepts a visitor.
    /// </summary>
    /// <param name="visitor">The visitor.</param>
    public abstract void Accept(IDocumentationContentVisitor visitor);

    /// <summary>
    /// Accepts a visitor.
    /// </summary>
    /// <typeparam name="TResult">The visit result type.</typeparam>
    /// <param name="visitor">The visitor.</param>
    /// <returns>The visit result.</returns>
    public abstract TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor);
}
