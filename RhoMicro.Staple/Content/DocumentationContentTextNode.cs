// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

/// <summary>
/// Represents a text node in documentation content.
/// </summary>
public sealed class DocumentationContentTextNode(String text) : DocumentationContentNode
{
    /// <summary>
    /// Gets the text value.
    /// </summary>
    public String Text => text;

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitText(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.ThrowIfNull(visitor).VisitText(this);
}
