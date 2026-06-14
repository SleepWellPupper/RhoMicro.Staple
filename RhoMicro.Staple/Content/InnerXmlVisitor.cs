// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Text;

/// <summary>
/// Visits documentation content and appends inner XML text to a builder.
/// </summary>
/// <param name="builder">The builder to append to.</param>
public sealed class InnerXmlVisitor(StringBuilder builder) : IDocumentationContentVisitor
{
    private readonly OuterXmlVisitor _visitor = new(builder);

    /// <inheritdoc/>
    public void VisitMember(MemberElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitSummary(SummaryElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitRemarks(RemarksElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitReturns(ReturnsElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitExample(ExampleElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitTypeParameter(TypeParameterElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitParameter(ParameterElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitText(DocumentationContentTextNode node) => _visitor.VisitText(node);

    /// <inheritdoc/>
    public void VisitInlineCode(InlineCodeElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitCode(CodeElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitItalics(ItalicsElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitEmphasis(EmphasisElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitBold(BoldElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitLineBreak(LineBreakElement element) => _visitor.VisitLineBreak(element);

    /// <inheritdoc/>
    public void VisitParagraph(ParagraphElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitList(ListElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitListHeader(ListHeaderElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitListItem(ListItemElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitListTerm(ListTermElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitListDescription(ListDescriptionElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitSee(SeeElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitSeeAlso(SeeAlsoElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitInheritdoc(InheritdocElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitParameterReference(ParameterReferenceElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitTypeParameterReference(TypeParameterReferenceElement element) => _visitor.VisitChildren(element);

    /// <inheritdoc/>
    public void VisitUnknown(UnknownElement element) => _visitor.VisitChildren(element);
}
