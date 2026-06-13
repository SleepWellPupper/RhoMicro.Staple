namespace RhoMicro.Staples.Tests;

using Content;

public class DocumentationContentTests
{
    [Fact]
    public void NodesRepresentMixedContent()
    {
        var content = new DocumentationContent(DocumentationTestData.XmlDocumentationContentSource);

        Assert.Equal(4, content.Nodes.Count);
        DocumentationContentAssertions.AssertText(content.Nodes[0], "Summary ");

        var code = DocumentationContentAssertions.AssertNode<CodeElement>(content.Nodes[1]);
        DocumentationContentAssertions.AssertText(Assert.Single(code.Children), "body");

        DocumentationContentAssertions.AssertText(content.Nodes[2], " and ");

        var parameterReference = DocumentationContentAssertions.AssertNode<ParameterReferenceElement>(content.Nodes[3]);
        Assert.Equal("value", parameterReference.Name);
    }

    [Fact]
    public void EmptyContentReturnsNoNodes()
    {
        var content = new DocumentationContent(String.Empty);

        Assert.Empty(content.Nodes);
    }

    [Fact]
    public void NodesPreserveFormattingElements()
    {
        var content = new DocumentationContent(DocumentationTestData.XmlDocumentationFormattingContentSource);

        Assert.Equal(5, content.Nodes.Count);

        var italic = DocumentationContentAssertions.AssertNode<ItalicsElement>(content.Nodes[0]);
        DocumentationContentAssertions.AssertText(Assert.Single(italic.Children), "Italic");
        DocumentationContentAssertions.AssertText(content.Nodes[1], " and ");

        var emphasis = DocumentationContentAssertions.AssertNode<EmphasisElement>(content.Nodes[2]);
        DocumentationContentAssertions.AssertText(Assert.Single(emphasis.Children), "emphasized");
        DocumentationContentAssertions.AssertText(content.Nodes[3], " and ");

        var bold = DocumentationContentAssertions.AssertNode<BoldElement>(content.Nodes[4]);
        DocumentationContentAssertions.AssertText(Assert.Single(bold.Children), "bold");
    }

    [Fact]
    public void NodesPreserveParagraphsAndLineBreaks()
    {
        var content = new DocumentationContent(DocumentationTestData.XmlDocumentationParagraphContentSource);

        Assert.Equal(5, content.Nodes.Count);
        DocumentationContentAssertions.AssertText(content.Nodes[0], "Before");
        DocumentationContentAssertions.AssertNode<LineBreakElement>(content.Nodes[1]);
        DocumentationContentAssertions.AssertText(content.Nodes[2], "Middle");

        var paragraph = DocumentationContentAssertions.AssertNode<ParagraphElement>(content.Nodes[3]);
        Assert.Equal(4, paragraph.Children.Count);
        DocumentationContentAssertions.AssertText(paragraph.Children[0], "Paragraph ");

        var see = DocumentationContentAssertions.AssertNode<SeeElement>(paragraph.Children[1]);
        Assert.Equal("T:System.String", see.Cref);
        DocumentationContentAssertions.AssertText(paragraph.Children[2], " ");

        var parameterReference = DocumentationContentAssertions.AssertNode<ParameterReferenceElement>(paragraph.Children[3]);
        Assert.Equal("value", parameterReference.Name);
        DocumentationContentAssertions.AssertText(content.Nodes[4], "After");
    }

    [Fact]
    public void NodesPreserveReferenceElements()
    {
        var content = new DocumentationContent(DocumentationTestData.XmlDocumentationReferenceContentSource);

        Assert.Equal(5, content.Nodes.Count);

        var see = DocumentationContentAssertions.AssertNode<SeeElement>(content.Nodes[0]);
        Assert.Equal("T:System.String", see.Cref);
        DocumentationContentAssertions.AssertText(content.Nodes[1], " ");

        var langword = DocumentationContentAssertions.AssertNode<SeeElement>(content.Nodes[2]);
        Assert.Equal("null", langword.Langword);
        DocumentationContentAssertions.AssertText(content.Nodes[3], " ");

        var inheritdoc = DocumentationContentAssertions.AssertNode<InheritdocElement>(content.Nodes[4]);
        Assert.Equal("M:Example.Method", inheritdoc.Cref);
    }

    [Fact]
    public void NodesPreserveListElements()
    {
        var content = new DocumentationContent(DocumentationTestData.XmlDocumentationListContentSource);

        var list = DocumentationContentAssertions.AssertNode<ListElement>(content.Nodes[0]);
        Assert.Equal("table", list.Type);
        Assert.Equal(2, list.Children.Count);

        var header = DocumentationContentAssertions.AssertNode<ListHeaderElement>(list.Children[0]);
        Assert.Equal(2, header.Children.Count);
        var headerTerm = DocumentationContentAssertions.AssertNode<ListTermElement>(header.Children[0]);
        DocumentationContentAssertions.AssertText(Assert.Single(headerTerm.Children), "Header term");
        var headerDescription = DocumentationContentAssertions.AssertNode<ListDescriptionElement>(header.Children[1]);
        DocumentationContentAssertions.AssertText(Assert.Single(headerDescription.Children), "Header description");

        var item = DocumentationContentAssertions.AssertNode<ListItemElement>(list.Children[1]);
        Assert.Equal(2, item.Children.Count);
        var itemTerm = DocumentationContentAssertions.AssertNode<ListTermElement>(item.Children[0]);
        DocumentationContentAssertions.AssertText(Assert.Single(itemTerm.Children), "Row term");
        var itemDescription = DocumentationContentAssertions.AssertNode<ListDescriptionElement>(item.Children[1]);
        DocumentationContentAssertions.AssertText(Assert.Single(itemDescription.Children), "Row description");
    }

    [Fact]
    public void VisitorReceivesConcreteNodeTypes()
    {
        var content = new DocumentationContent(DocumentationTestData.XmlDocumentationContentSource);
        var visitor = new RecordingVisitor();

        foreach(var node in content.Nodes)
        {
            node.Accept(visitor);
        }

        Assert.Equal(
            ["Text", "Code", "Text", "ParameterReference"],
            visitor.VisitedKinds);
    }

    [Fact]
    public void VisitorReceivesListNodeTypes()
    {
        var content = new DocumentationContent(DocumentationTestData.XmlDocumentationListContentSource);
        var visitor = new RecordingVisitor();

        VisitAll(content.Nodes[0], visitor);

        Assert.Equal(
            ["List", "ListHeader", "ListTerm", "Text", "ListDescription", "Text", "ListItem", "ListTerm", "Text", "ListDescription", "Text"],
            visitor.VisitedKinds);
    }

    [Fact]
    public void InvalidXmlThrowsInvalidOperationExceptionWithXmlExceptionInner()
    {
        var content = new DocumentationContent("<c>");

        var result = Assert.Throws<InvalidOperationException>(() => _ = content.Nodes);
        Assert.Equal("The XML documentation content is not valid XML.", result.Message);
        Assert.IsType<System.Xml.XmlException>(result.InnerException);
    }

    [Fact]
    public void NullSourceThrowsArgumentNullExceptionAtConstruction()
    {
        var result = Assert.Throws<ArgumentNullException>(() => new DocumentationContent(null!));
        Assert.Equal("source", result.ParamName);
    }

    private sealed class RecordingVisitor : IDocumentationContentVisitor
    {
        public List<String> VisitedKinds { get; } = [];

        public void VisitMember(MemberElement element)
        {
            VisitedKinds.Add("Member");
        }

        public void VisitSummary(SummaryElement element)
        {
            VisitedKinds.Add("Summary");
        }

        public void VisitRemarks(RemarksElement element)
        {
            VisitedKinds.Add("Remarks");
        }

        public void VisitExample(ExampleElement element)
        {
            VisitedKinds.Add("Example");
        }

        public void VisitTypeParameter(TypeParameterElement element)
        {
            VisitedKinds.Add("TypeParameter");
        }

        public void VisitParameter(ParameterElement element)
        {
            VisitedKinds.Add("Parameter");
        }

        public void VisitText(DocumentationContentTextNode node)
        {
            VisitedKinds.Add("Text");
        }

        public void VisitCode(CodeElement element)
        {
            VisitedKinds.Add("Code");
        }

        public void VisitItalics(ItalicsElement element)
        {
            VisitedKinds.Add("Italics");
        }

        public void VisitEmphasis(EmphasisElement element)
        {
            VisitedKinds.Add("Emphasis");
        }

        public void VisitBold(BoldElement element)
        {
            VisitedKinds.Add("Bold");
        }

        public void VisitLineBreak(LineBreakElement element)
        {
            VisitedKinds.Add("LineBreak");
        }

        public void VisitParagraph(ParagraphElement element)
        {
            VisitedKinds.Add("Paragraph");
        }

        public void VisitList(ListElement element)
        {
            VisitedKinds.Add("List");
        }

        public void VisitListHeader(ListHeaderElement element)
        {
            VisitedKinds.Add("ListHeader");
        }

        public void VisitListItem(ListItemElement element)
        {
            VisitedKinds.Add("ListItem");
        }

        public void VisitListTerm(ListTermElement element)
        {
            VisitedKinds.Add("ListTerm");
        }

        public void VisitListDescription(ListDescriptionElement element)
        {
            VisitedKinds.Add("ListDescription");
        }

        public void VisitSee(SeeElement element)
        {
            VisitedKinds.Add("See");
        }

        public void VisitSeeAlso(SeeAlsoElement element)
        {
            VisitedKinds.Add("SeeAlso");
        }

        public void VisitInheritdoc(InheritdocElement element)
        {
            VisitedKinds.Add("Inheritdoc");
        }

        public void VisitParameterReference(ParameterReferenceElement element)
        {
            VisitedKinds.Add("ParameterReference");
        }

        public void VisitTypeParameterReference(TypeParameterReferenceElement element)
        {
            VisitedKinds.Add("TypeParameterReference");
        }

        public void VisitUnknown(UnknownElement element)
        {
            VisitedKinds.Add("Unknown");
        }
    }

    private static void VisitAll(DocumentationContentNode node, RecordingVisitor visitor)
    {
        node.Accept(visitor);

        if(node is not DocumentationContentContainerElement container)
        {
            return;
        }

        foreach(var child in container.Children)
        {
            VisitAll(child, visitor);
        }
    }
}
