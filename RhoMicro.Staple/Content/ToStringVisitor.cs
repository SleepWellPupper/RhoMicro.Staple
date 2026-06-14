// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Security;
using System.Text;

/// <summary>
/// Visits documentation content and appends XML text to a builder.
/// </summary>
/// <param name="builder">The builder to append to.</param>
public sealed class ToStringVisitor(StringBuilder builder) : DepthFirstDocumentationContentVisitor
{
    private void SurroundAndVisit(DocumentationContentContainerElement element, String name)
    {
        builder.Append($"<{name}>");
        VisitChildren(element);
        builder.Append($"</{name}>");
    }

    private void AppendAttribute(String name, String value)
    {
        if (String.IsNullOrEmpty(value))
        {
            return;
        }

        builder.Append(' ');
        builder.Append(name);
        builder.Append("=\"");
        builder.Append(SecurityElement.Escape(value));
        builder.Append('"');
    }

    private void AppendSelfClosing(String name)
        => builder.Append($"<{name}/>");

    /// <inheritdoc />
    public override void VisitBold(BoldElement element) => SurroundAndVisit(element, "b");

    /// <inheritdoc />
    public override void VisitInlineCode(InlineCodeElement element) => SurroundAndVisit(element, "c");

    /// <inheritdoc />
    public override void VisitCode(CodeElement element) => SurroundAndVisit(element, "code");

    /// <inheritdoc />
    public override void VisitEmphasis(EmphasisElement element) => SurroundAndVisit(element, "em");

    /// <inheritdoc />
    public override void VisitExample(ExampleElement element) => SurroundAndVisit(element, "example");

    /// <inheritdoc />
    public override void VisitInheritdoc(InheritdocElement element)
    {
        builder.Append("<inheritdoc");
        AppendAttribute("cref", element.Cref);
        AppendAttribute("path", element.Path);

        if (element.Children.Count == 0)
        {
            AppendSelfClosing("inheritdoc");
            return;
        }

        builder.Append('>');
        VisitChildren(element);
        builder.Append("</inheritdoc>");
    }

    /// <inheritdoc />
    public override void VisitItalics(ItalicsElement element) => SurroundAndVisit(element, "i");

    /// <inheritdoc />
    public override void VisitLineBreak(LineBreakElement element) => AppendSelfClosing("br");

    /// <inheritdoc />
    public override void VisitList(ListElement element)
    {
        builder.Append("<list");
        AppendAttribute("type", element.Type);
        builder.Append('>');
        VisitChildren(element);
        builder.Append("</list>");
    }

    /// <inheritdoc />
    public override void VisitListDescription(ListDescriptionElement element) =>
        SurroundAndVisit(element, "description");

    /// <inheritdoc />
    public override void VisitListHeader(ListHeaderElement element) => SurroundAndVisit(element, "listheader");

    /// <inheritdoc />
    public override void VisitListItem(ListItemElement element) => SurroundAndVisit(element, "item");

    /// <inheritdoc />
    public override void VisitListTerm(ListTermElement element) => SurroundAndVisit(element, "term");

    /// <inheritdoc />
    public override void VisitMember(MemberElement element) => SurroundAndVisit(element, "member");

    /// <inheritdoc />
    public override void VisitParameter(ParameterElement element)
    {
        builder.Append("<param");
        AppendAttribute("name", element.Name);
        builder.Append('>');
        VisitChildren(element);
        builder.Append("</param>");
    }

    /// <inheritdoc />
    public override void VisitParameterReference(ParameterReferenceElement element)
    {
        builder.Append("<paramref");
        AppendAttribute("name", element.Name);

        if (element.Children.Count == 0)
        {
            AppendSelfClosing("paramref");
            return;
        }

        builder.Append('>');
        VisitChildren(element);
        builder.Append("</paramref>");
    }

    /// <inheritdoc />
    public override void VisitParagraph(ParagraphElement element) => SurroundAndVisit(element, "para");

    /// <inheritdoc />
    public override void VisitRemarks(RemarksElement element) => SurroundAndVisit(element, "remarks");

    /// <inheritdoc />
    public override void VisitReturns(ReturnsElement element) => SurroundAndVisit(element, "returns");

    /// <inheritdoc />
    public override void VisitSee(SeeElement element)
    {
        builder.Append("<see");
        AppendAttribute("cref", element.Cref);
        AppendAttribute("href", element.Href);
        AppendAttribute("langword", element.Langword);
        AppendAttribute("name", element.Name);

        if (element.Children.Count == 0)
        {
            AppendSelfClosing("see");
            return;
        }

        builder.Append('>');
        VisitChildren(element);
        builder.Append("</see>");
    }

    /// <inheritdoc />
    public override void VisitSeeAlso(SeeAlsoElement element)
    {
        builder.Append("<seealso");
        AppendAttribute("cref", element.Cref);
        AppendAttribute("href", element.Href);
        AppendAttribute("langword", element.Langword);
        AppendAttribute("name", element.Name);

        if (element.Children.Count == 0)
        {
            AppendSelfClosing("seealso");
            return;
        }

        builder.Append('>');
        VisitChildren(element);
        builder.Append("</seealso>");
    }

    /// <inheritdoc />
    public override void VisitSummary(SummaryElement element) => SurroundAndVisit(element, "summary");

    /// <inheritdoc />
    public override void VisitText(DocumentationContentTextNode node) => builder.Append(SecurityElement.Escape(node.Text));

    /// <inheritdoc />
    public override void VisitTypeParameter(TypeParameterElement element)
    {
        builder.Append("<typeparam");
        AppendAttribute("name", element.Name);
        builder.Append('>');
        VisitChildren(element);
        builder.Append("</typeparam>");
    }

    /// <inheritdoc />
    public override void VisitTypeParameterReference(TypeParameterReferenceElement element)
    {
        builder.Append("<typeparamref");
        AppendAttribute("name", element.Name);

        if (element.Children.Count == 0)
        {
            AppendSelfClosing("typeparamref");
            return;
        }

        builder.Append('>');
        VisitChildren(element);
        builder.Append("</typeparamref>");
    }

    /// <inheritdoc />
    public override void VisitUnknown(UnknownElement element)
    {
        builder.Append('<');
        builder.Append(element.Name);

        foreach (var attribute in element.Attributes)
        {
            AppendAttribute(attribute.Key, attribute.Value);
        }

        if (element.Children.Count == 0)
        {
            AppendSelfClosing(element.Name);
            return;
        }

        builder.Append('>');
        VisitChildren(element);
        builder.Append($"</{element.Name}>");
    }
}
