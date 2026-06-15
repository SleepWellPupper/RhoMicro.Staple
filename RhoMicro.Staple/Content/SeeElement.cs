// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;

/// <summary>
/// Represents a <c>see</c> element.
/// </summary>
public sealed class SeeElement(
    String cref,
    String href,
    String langword,
    String name,
    ImmutableArray<DocumentationContentNode> children)
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
        => ArgumentNullException.ThrowIfNull(visitor).VisitSee(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitSee(this);
}
