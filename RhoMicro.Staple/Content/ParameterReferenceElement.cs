namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;

/// <summary>
/// Represents a <c>paramref</c> element.
/// </summary>
public sealed class ParameterReferenceElement(
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
        => ArgumentNullException.ThrowIfNull(visitor).VisitParameterReference(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitParameterReference(this);
}
