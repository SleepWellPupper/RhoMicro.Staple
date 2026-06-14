namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;

/// <summary>
/// Represents a <c>param</c> element.
/// </summary>
public sealed class ParameterElement(
    String name,
    ImmutableArray<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <summary>
    /// Gets the parameter name.
    /// </summary>
    public String Name { get; } = ArgumentNullException.ThrowIfNull(name);

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitParameter(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitParameter(this);
}
