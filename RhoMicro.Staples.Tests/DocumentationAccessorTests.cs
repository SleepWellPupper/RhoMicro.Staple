namespace RhoMicro.Staples.Tests;

using Content;

public class DocumentationAccessorTests
{
    [Fact]
    public void PropertiesReturnExpectedValues()
    {
        var accessor = new DocumentationAccessor(DocumentationTestData.DocumentationAccessorSource);
        var root = accessor.Root;

        Assert.Equal("X:Example.Item", accessor.Id);
        Assert.Equal("X:Example.Item", root.Id);

        var summary = Assert.IsType<SummaryElement>(root.Summary);
        Assert.Equal(2, summary.Children.Count);
        DocumentationContentAssertions.AssertText(summary.Children[0], "Summary ");
        var summaryCode = DocumentationContentAssertions.AssertNode<CodeElement>(summary.Children[1]);
        DocumentationContentAssertions.AssertText(Assert.Single(summaryCode.Children), "body");

        var remarks = Assert.IsType<RemarksElement>(root.Remarks);
        Assert.Equal(2, remarks.Children.Count);
        DocumentationContentAssertions.AssertText(remarks.Children[0], "Remarks ");
        var remarksBold = DocumentationContentAssertions.AssertNode<BoldElement>(remarks.Children[1]);
        DocumentationContentAssertions.AssertText(Assert.Single(remarksBold.Children), "body");

        var example = Assert.IsType<ExampleElement>(root.Example);
        Assert.Equal(2, example.Children.Count);
        DocumentationContentAssertions.AssertText(example.Children[0], "Example ");
        var exampleSee = DocumentationContentAssertions.AssertNode<SeeElement>(example.Children[1]);
        Assert.Equal("T:System.String", exampleSee.Cref);
    }

    [Fact]
    public void MissingElementsReturnEmptyStrings()
    {
        var accessor = new DocumentationAccessor(DocumentationTestData.MissingDocumentationAccessorSource);
        var root = accessor.Root;

        Assert.Equal(String.Empty, accessor.Id);
        var summary = Assert.IsType<SummaryElement>(root.Summary);
        Assert.Empty(summary.Children);
        Assert.Null(root.Remarks);
        Assert.Null(root.Example);
    }

    [Fact]
    public void InvalidXmlThrowsInvalidOperationExceptionWhenAccessed()
    {
        var accessor = new DocumentationAccessor("<member");

        var result = Assert.Throws<InvalidOperationException>(() => _ = accessor.Root);

        Assert.Equal("The XML documentation source is not valid XML.", result.Message);
        Assert.IsType<System.Xml.XmlException>(result.InnerException);
    }

    [Fact]
    public void NullSourceThrowsArgumentNullExceptionAtConstruction()
    {
        var result = Assert.Throws<ArgumentNullException>(() => new DocumentationAccessor(null!));
        Assert.Equal("source", result.ParamName);
    }
}
