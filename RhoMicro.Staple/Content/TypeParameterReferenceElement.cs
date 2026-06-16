// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;

/// <summary>
/// Represents a <c>typeparamref</c> element.
/// </summary>
public sealed class TypeParameterReferenceElement(
    String name,
    ImmutableArray<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <summary>
    /// Gets the type parameter name.
    /// </summary>
    public String Name { get; } = ArgumentNullException.Validate(name);

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.Validate(visitor);
        visitor.VisitTypeParameterReference(this);
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.Validate(visitor);

        var result = visitor.VisitTypeParameterReference(this);
        return result;
    }
}
