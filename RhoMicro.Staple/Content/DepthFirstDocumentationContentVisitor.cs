// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

/// <summary>
/// Visits documentation content in depth-first order.
/// </summary>
public abstract class DepthFirstDocumentationContentVisitor : IDocumentationContentVisitor
{
    /// <summary>
    /// Visits the children of a container element.
    /// </summary>
    /// <param name="element">The element.</param>
    protected void VisitChildren(DocumentationContentContainerElement element)
    {
        foreach (var child in element.Children)
        {
            child.Accept(this);
        }
    }

    /// <inheritdoc />
    public virtual void VisitMember(MemberElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitSummary(SummaryElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitRemarks(RemarksElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitReturns(ReturnsElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitExample(ExampleElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitTypeParameter(TypeParameterElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitParameter(ParameterElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitText(DocumentationContentTextNode node)
    {
    }

    /// <inheritdoc />
    public virtual void VisitInlineCode(InlineCodeElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitCode(CodeElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitItalics(ItalicsElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitEmphasis(EmphasisElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitBold(BoldElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitLineBreak(LineBreakElement element)
    {
    }

    /// <inheritdoc />
    public virtual void VisitParagraph(ParagraphElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitList(ListElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitListHeader(ListHeaderElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitListItem(ListItemElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitListTerm(ListTermElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitListDescription(ListDescriptionElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitSee(SeeElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitSeeAlso(SeeAlsoElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitInheritdoc(InheritdocElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitParameterReference(ParameterReferenceElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitTypeParameterReference(TypeParameterReferenceElement element) => VisitChildren(element);

    /// <inheritdoc />
    public virtual void VisitUnknown(UnknownElement element) => VisitChildren(element);
}
