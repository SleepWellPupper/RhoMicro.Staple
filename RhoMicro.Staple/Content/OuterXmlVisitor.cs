// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Security;
using System.Text;

/// <summary>
/// Visits documentation content and appends outer XML text to a builder.
/// </summary>
/// <param name="builder">The builder to append to.</param>
public sealed class OuterXmlVisitor(StringBuilder builder) : DepthFirstDocumentationContentVisitor
{
    private void AppendSelfClosingElement(
        String name,
        params ReadOnlySpan<(String key, String value)> attributes)
    {
        OpenElement(name, attributes);

        builder.Append("/>");
    }

    private void OpenElement(string name, ReadOnlySpan<(String key, String value)> attributes)
    {
        builder.Append($"<{name}");

        AppendAttributes(attributes);
    }

    private void AppendAttributes(ReadOnlySpan<(String key, String value)> attributes)
    {
        foreach (var (key, value) in attributes)
        {
            if (value is [])
            {
                continue;
            }

            builder.Append($" {key}=\"{value}\"");
        }
    }

    private void AppendElement(
        DocumentationContentContainerElement element,
        String name,
        params ReadOnlySpan<(String key, String value)> attributes)
    {
        if (attributes is not [] && element.Children is [])
        {
            AppendSelfClosingElement(name, attributes);
            return;
        }

        OpenElement(name, attributes);

        builder.Append('>');

        VisitChildren(element);

        builder.Append($"</{name}>");
    }

    /// <inheritdoc />
    public override void VisitBold(BoldElement element)
        => AppendElement(element, "b");

    /// <inheritdoc />
    public override void VisitInlineCode(InlineCodeElement element)
        => AppendElement(element, "c");

    /// <inheritdoc />
    public override void VisitCode(CodeElement element)
        => AppendElement(element, "code");

    /// <inheritdoc />
    public override void VisitEmphasis(EmphasisElement element)
        => AppendElement(element, "em");

    /// <inheritdoc />
    public override void VisitExample(ExampleElement element)
        => AppendElement(element, "example");

    /// <inheritdoc />
    public override void VisitInheritdoc(InheritdocElement element)
        => AppendElement(
            element,
            "inheritdoc",
            ("cref", element.Cref),
            ("path", element.Path));

    /// <inheritdoc />
    public override void VisitItalics(ItalicsElement element)
        => AppendElement(element, "i");

    /// <inheritdoc />
    public override void VisitLineBreak(LineBreakElement element) => AppendSelfClosingElement("br");

    /// <inheritdoc />
    public override void VisitList(ListElement element)
        => AppendElement(
            element,
            "list",
            ("type", element.Type));

    /// <inheritdoc />
    public override void VisitListDescription(ListDescriptionElement element)
        => AppendElement(element, "description");

    /// <inheritdoc />
    public override void VisitListHeader(ListHeaderElement element)
        => AppendElement(element, "listheader");

    /// <inheritdoc />
    public override void VisitListItem(ListItemElement element)
        => AppendElement(element, "item");

    /// <inheritdoc />
    public override void VisitListTerm(ListTermElement element)
        => AppendElement(element, "term");

    /// <inheritdoc />
    public override void VisitMember(MemberElement element)
        => AppendElement(element, "member");

    /// <inheritdoc />
    public override void VisitParameter(ParameterElement element)
        => AppendElement(
            element,
            "param",
            ("name", element.Name));

    /// <inheritdoc />
    public override void VisitParameterReference(ParameterReferenceElement element)
        => AppendElement(
            element,
            "paramref",
            ("name", element.Name));

    /// <inheritdoc />
    public override void VisitParagraph(ParagraphElement element)
        => AppendElement(element, "para");

    /// <inheritdoc />
    public override void VisitRemarks(RemarksElement element)
        => AppendElement(element, "remarks");

    /// <inheritdoc />
    public override void VisitReturns(ReturnsElement element)
        => AppendElement(element, "returns");

    /// <inheritdoc />
    public override void VisitSee(SeeElement element)
        => AppendElement(
            element,
            "see",
            ("name", element.Name),
            ("href", element.Href),
            ("cref", element.Cref),
            ("langword", element.Langword));

    /// <inheritdoc />
    public override void VisitSeeAlso(SeeAlsoElement element)
        => AppendElement(
            element,
            "seealso",
            ("name", element.Name),
            ("href", element.Href),
            ("cref", element.Cref),
            ("langword", element.Langword));

    /// <inheritdoc />
    public override void VisitSummary(SummaryElement element)
        => AppendElement(element, "summary");

    /// <inheritdoc />
    public override void VisitText(DocumentationContentTextNode node) =>
        builder.Append(SecurityElement.Escape(node.Text));

    /// <inheritdoc />
    public override void VisitTypeParameter(TypeParameterElement element)
        => AppendElement(
            element,
            "typeparam",
            ("name", element.Name));

    /// <inheritdoc />
    public override void VisitTypeParameterReference(TypeParameterReferenceElement element)
        => AppendElement(
            element,
            "typeparamref",
            ("name", element.Name));

    /// <inheritdoc />
    public override void VisitUnknown(UnknownElement element)
        => AppendElement(
            element,
            element.Name,
            element.Attributes.Select(kvp => (kvp.Key, kvp.Value)).ToArray());
}
