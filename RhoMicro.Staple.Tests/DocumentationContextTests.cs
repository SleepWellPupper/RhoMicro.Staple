namespace RhoMicro.Staple.Tests;

using Content;
using Staple;

public class DocumentationContextTests
{
    [Fact]
    public void CreateBuildsTypeDocumentationFromXmlSource()
    {
        var context = DocumentationContext.Create(DocumentationTestData.MembersContextSource);

        var documentation = Assert.IsType<Documentation>(context.GetContent("T:Example.GenericType`1"));
        Assert.Equal("T:Example.GenericType`1", documentation.Id);

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var summaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("Type summary", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);
    }

    [Fact]
    public void CreateBuildsMethodDocumentationFromXmlSource()
    {
        var context = DocumentationContext.Create(DocumentationTestData.MembersContextSource);

        var documentation = Assert.IsType<Documentation>(context.GetContent("M:Example.Container.SampleMethod``1(System.String,System.Int32)"));
        Assert.Single(documentation.TypeParameters);
        Assert.Single(documentation.Parameters);
    }

    [Fact]
    public void CreateBuildsPropertyDocumentationFromXmlSource()
    {
        var context = DocumentationContext.Create(DocumentationTestData.MembersContextSource);

        var documentation = Assert.IsType<Documentation>(context.GetContent("P:Example.Container.Value"));

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var summaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("Property summary", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);
    }

    [Fact]
    public void CreateBuildsFieldDocumentationFromXmlSource()
    {
        var context = DocumentationContext.Create(DocumentationTestData.MembersContextSource);

        var documentation = Assert.IsType<Documentation>(context.GetContent("F:Example.Container.Field"));

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var summaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("Field summary", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);
    }

    [Fact]
    public void CreateBuildsFallbackDocumentationFromXmlSource()
    {
        var context = DocumentationContext.Create(DocumentationTestData.MembersContextSource);

        var documentation = Assert.IsType<Documentation>(context.GetContent("Example.Custom"));

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var summaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("Fallback summary", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);
    }

    [Fact]
    public void CreateReturnsNullForAssemblyWithoutDocumentationMetadata()
    {
        var context = DocumentationContext.Create(typeof(DocumentationContextTests).Assembly);

        Assert.Null(context.GetContent(DocumentationTestData.DocumentedTypeId));
        Assert.Null(context.GetContent(DocumentationTestData.DocumentedMethodId));
        Assert.Null(context.GetContent(DocumentationTestData.DocumentedConstructorId));
        Assert.Null(context.GetContent(DocumentationTestData.DocumentedPropertyId));
        Assert.Null(context.GetContent(DocumentationTestData.DocumentedFieldId));
        Assert.Null(context.GetContent(DocumentationTestData.DocumentedNestedTypeId));
    }

    [Fact]
    public void CreateBuildsContextFromDocumentationObjects()
    {
        var typeDocumentation = new Documentation(MemberElement.Create(DocumentationTestData.DocumentedTypeSource));
        var methodDocumentation = new Documentation(MemberElement.Create(DocumentationTestData.DocumentedMethodSource));

        var context = DocumentationContext.Create(typeDocumentation, methodDocumentation);

        Assert.Same(typeDocumentation, context.GetContent(DocumentationTestData.DocumentedTypeId));
        Assert.Same(methodDocumentation, context.GetContent(DocumentationTestData.DocumentedMethodId));
    }

    [Fact]
    public void CreateIgnoresUnexpectedNodes()
    {
        var context = DocumentationContext.Create(DocumentationTestData.DuplicateContextSource);

        var documentation = Assert.IsType<Documentation>(context.GetContent("T:Example.GenericType`1"));
        var knownSummary = Assert.IsType<SummaryElement>(documentation.Summary);
        var knownSummaryTextNode = Assert.Single(knownSummary.Children);
        Assert.Equal("Known", Assert.IsType<DocumentationContentTextNode>(knownSummaryTextNode).Text);
        Assert.Null(context.GetContent("T:Ignored"));
    }

    [Fact]
    public void CreateThrowsForInvalidXml()
    {
        var result = Assert.Throws<InvalidOperationException>(() => DocumentationContext.Create("<doc"));

        Assert.Equal("Unable to parse documentation XML.", result.Message);
        Assert.IsType<System.Xml.XmlException>(result.InnerException);
    }

    [Fact]
    public void GetContentReturnsNullForMissingId()
    {
        var context = DocumentationContext.Create(DocumentationTestData.ContextSource);

        Assert.Null(context.GetContent("T:Missing"));
    }

    [Fact]
    public void GetContentThrowsForNullId()
    {
        var context = DocumentationContext.Create(DocumentationTestData.ContextSource);

        var result = Assert.Throws<ArgumentNullException>(() => context.GetContent(GetNullString()));
        Assert.Equal("id", result.ParamName);
    }

#pragma warning disable CS8603
    private static String GetNullString() => default;
#pragma warning restore CS8603
}
