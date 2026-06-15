// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Text;

/// <summary>
/// Visits documentation content nodes.
/// </summary>
public interface IDocumentationContentVisitor
{
    /// <summary>
    /// Visits a <c>member</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitMember(MemberElement element);

    /// <summary>
    /// Visits a <c>summary</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitSummary(SummaryElement element);

    /// <summary>
    /// Visits a <c>remarks</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitRemarks(RemarksElement element);

    /// <summary>
    /// Visits a <c>returns</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitReturns(ReturnsElement element);

    /// <summary>
    /// Visits an <c>example</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitExample(ExampleElement element);

    /// <summary>
    /// Visits a <c>typeparam</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitTypeParameter(TypeParameterElement element);

    /// <summary>
    /// Visits a <c>param</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitParameter(ParameterElement element);

    /// <summary>
    /// Visits a text node.
    /// </summary>
    /// <param name="node">The node.</param>
    void VisitText(DocumentationContentTextNode node);

    /// <summary>
    /// Visits an inline <c>c</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitInlineCode(InlineCodeElement element);

    /// <summary>
    /// Visits a <c>code</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitCode(CodeElement element);

    /// <summary>
    /// Visits an <c>i</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitItalics(ItalicsElement element);

    /// <summary>
    /// Visits an <c>em</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitEmphasis(EmphasisElement element);

    /// <summary>
    /// Visits a <c>b</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitBold(BoldElement element);

    /// <summary>
    /// Visits a <c>br</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitLineBreak(LineBreakElement element);

    /// <summary>
    /// Visits a <c>para</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitParagraph(ParagraphElement element);

    /// <summary>
    /// Visits a <c>list</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitList(ListElement element);

    /// <summary>
    /// Visits a <c>listheader</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitListHeader(ListHeaderElement element);

    /// <summary>
    /// Visits a <c>item</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitListItem(ListItemElement element);

    /// <summary>
    /// Visits a <c>term</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitListTerm(ListTermElement element);

    /// <summary>
    /// Visits a <c>description</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitListDescription(ListDescriptionElement element);

    /// <summary>
    /// Visits a <c>see</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitSee(SeeElement element);

    /// <summary>
    /// Visits a <c>seealso</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitSeeAlso(SeeAlsoElement element);

    /// <summary>
    /// Visits an <c>inheritdoc</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitInheritdoc(InheritdocElement element);

    /// <summary>
    /// Visits a <c>paramref</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    void VisitParameterReference(ParameterReferenceElement element);

    /// <summary>
    /// Visits a <c>typeparamref</c> element.
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
    /// Visits a <c>member</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitMember(MemberElement element);

    /// <summary>
    /// Visits a <c>summary</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitSummary(SummaryElement element);

    /// <summary>
    /// Visits a <c>remarks</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitRemarks(RemarksElement element);

    /// <summary>
    /// Visits a <c>returns</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitReturns(ReturnsElement element);

    /// <summary>
    /// Visits an <c>example</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitExample(ExampleElement element);

    /// <summary>
    /// Visits a <c>typeparam</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitTypeParameter(TypeParameterElement element);

    /// <summary>
    /// Visits a <c>param</c> element.
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
    /// Visits an inline <c>c</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitInlineCode(InlineCodeElement element);

    /// <summary>
    /// Visits a <c>code</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitCode(CodeElement element);

    /// <summary>
    /// Visits an <c>i</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitItalics(ItalicsElement element);

    /// <summary>
    /// Visits an <c>em</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitEmphasis(EmphasisElement element);

    /// <summary>
    /// Visits a <c>b</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitBold(BoldElement element);

    /// <summary>
    /// Visits a <c>br</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitLineBreak(LineBreakElement element);

    /// <summary>
    /// Visits a <c>para</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitParagraph(ParagraphElement element);

    /// <summary>
    /// Visits a <c>list</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitList(ListElement element);

    /// <summary>
    /// Visits a <c>listheader</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitListHeader(ListHeaderElement element);

    /// <summary>
    /// Visits a <c>item</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitListItem(ListItemElement element);

    /// <summary>
    /// Visits a <c>term</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitListTerm(ListTermElement element);

    /// <summary>
    /// Visits a <c>description</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitListDescription(ListDescriptionElement element);

    /// <summary>
    /// Visits a <c>see</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitSee(SeeElement element);

    /// <summary>
    /// Visits a <c>seealso</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitSeeAlso(SeeAlsoElement element);

    /// <summary>
    /// Visits an <c>inheritdoc</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitInheritdoc(InheritdocElement element);

    /// <summary>
    /// Visits a <c>paramref</c> element.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <returns>The visit result.</returns>
    TResult VisitParameterReference(ParameterReferenceElement element);

    /// <summary>
    /// Visits a <c>typeparamref</c> element.
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
