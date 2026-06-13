namespace RhoMicro.Staples.Content;

/// <summary>
/// Visits documentation content nodes.
/// </summary>
public interface IDocumentationContentVisitor
{
    /// <summary>
    /// Visits a member element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitMember(MemberElement element);

    /// <summary>
    /// Visits a summary element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitSummary(SummaryElement element);

    /// <summary>
    /// Visits a remarks element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitRemarks(RemarksElement element);

    /// <summary>
    /// Visits an example element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitExample(ExampleElement element);

    /// <summary>
    /// Visits a type parameter element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitTypeParameter(TypeParameterElement element);

    /// <summary>
    /// Visits a parameter element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitParameter(ParameterElement element);

    /// <summary>
    /// Visits a text node.
    /// </summary>
    /// <param name="node">The node.</param>
    void VisitText(DocumentationContentTextNode node);

    /// <summary>
    /// Visits a code element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitCode(CodeElement element);

    /// <summary>
    /// Visits an italics element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitItalics(ItalicsElement element);

    /// <summary>
    /// Visits an emphasis element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitEmphasis(EmphasisElement element);

    /// <summary>
    /// Visits a bold element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitBold(BoldElement element);

    /// <summary>
    /// Visits a line break element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitLineBreak(LineBreakElement element);

    /// <summary>
    /// Visits a paragraph element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitParagraph(ParagraphElement element);

    /// <summary>
    /// Visits a list element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitList(ListElement element);

    /// <summary>
    /// Visits a list header element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitListHeader(ListHeaderElement element);

    /// <summary>
    /// Visits a list item element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitListItem(ListItemElement element);

    /// <summary>
    /// Visits a list term element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitListTerm(ListTermElement element);

    /// <summary>
    /// Visits a list description element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitListDescription(ListDescriptionElement element);

    /// <summary>
    /// Visits a see element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitSee(SeeElement element);

    /// <summary>
    /// Visits a see also element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitSeeAlso(SeeAlsoElement element);

    /// <summary>
    /// Visits an inheritdoc element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitInheritdoc(InheritdocElement element);

    /// <summary>
    /// Visits a parameter reference element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitParameterReference(ParameterReferenceElement element);

    /// <summary>
    /// Visits a type parameter reference element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitTypeParameterReference(TypeParameterReferenceElement element);

    /// <summary>
    /// Visits an unknown element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitUnknown(UnknownElement element);
}
/// <summary>
/// Visits documentation content nodes and returns a result.
/// </summary>
/// <typeparam name="TResult">The visit result type.</typeparam>
public interface IDocumentationContentVisitor<out TResult>
{
    /// <summary>
    /// Visits a member element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitMember(MemberElement element);

    /// <summary>
    /// Visits a summary element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitSummary(SummaryElement element);

    /// <summary>
    /// Visits a remarks element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitRemarks(RemarksElement element);

    /// <summary>
    /// Visits an example element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitExample(ExampleElement element);

    /// <summary>
    /// Visits a type parameter element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitTypeParameter(TypeParameterElement element);

    /// <summary>
    /// Visits a parameter element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitParameter(ParameterElement element);

    /// <summary>
    /// Visits a text node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns>The visit result.</returns>
    TResult VisitText(DocumentationContentTextNode node);

    /// <summary>
    /// Visits a code element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitCode(CodeElement element);

    /// <summary>
    /// Visits an italics element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitItalics(ItalicsElement element);

    /// <summary>
    /// Visits an emphasis element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitEmphasis(EmphasisElement element);

    /// <summary>
    /// Visits a bold element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitBold(BoldElement element);

    /// <summary>
    /// Visits a line break element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitLineBreak(LineBreakElement element);

    /// <summary>
    /// Visits a paragraph element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitParagraph(ParagraphElement element);

    /// <summary>
    /// Visits a list element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitList(ListElement element);

    /// <summary>
    /// Visits a list header element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitListHeader(ListHeaderElement element);

    /// <summary>
    /// Visits a list item element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitListItem(ListItemElement element);

    /// <summary>
    /// Visits a list term element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitListTerm(ListTermElement element);

    /// <summary>
    /// Visits a list description element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitListDescription(ListDescriptionElement element);

    /// <summary>
    /// Visits a see element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitSee(SeeElement element);

    /// <summary>
    /// Visits a see also element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitSeeAlso(SeeAlsoElement element);

    /// <summary>
    /// Visits an inheritdoc element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitInheritdoc(InheritdocElement element);

    /// <summary>
    /// Visits a parameter reference element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitParameterReference(ParameterReferenceElement element);

    /// <summary>
    /// Visits a type parameter reference element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitTypeParameterReference(TypeParameterReferenceElement element);

    /// <summary>
    /// Visits an unknown element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitUnknown(UnknownElement element);
}
