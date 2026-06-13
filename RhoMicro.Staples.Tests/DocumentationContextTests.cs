namespace RhoMicro.Staples.Tests;

using Content;

public class DocumentationContextTests
{
    [Fact]
    public void CreateBuildsContextFromAssemblyAttribute()
    {
        var context = DocumentationContext.Create(typeof(DocumentationContextTests).Assembly);

        Assert.IsType<TypeDocumentationAccessor>(context.GetContent("T:Example.GenericType`1"));
        Assert.IsType<MethodDocumentationAccessor>(context.GetContent("M:Example.Container.SampleMethod``1(System.String,System.Int32)"));
    }

    [Fact]
    public void CreateIsSafeForConcurrentAccessorLookups()
    {
        var context = DocumentationContext.Create(typeof(DocumentationContextTests).Assembly);
        DocumentationAccessor? expected = null;
        Object gate = new();

        Parallel.For(0, 64, _ =>
        {
            var accessor = context.GetContent("T:Example.GenericType`1");
            Assert.NotNull(accessor);

            lock(gate)
            {
                expected ??= accessor;
                Assert.Same(expected, accessor);
            }
        });
    }

    [Fact]
    public void CreateReturnsEmptyContextWhenAssemblyLacksAttribute()
    {
        var context = DocumentationContext.Create(typeof(String).Assembly);

        var result = context.GetContent("T:Missing");
        Assert.Null(result);
    }

    [Fact]
    public void CreateThrowsForNullAssembly()
    {
        var result = Assert.Throws<ArgumentNullException>(() => DocumentationContext.Create((System.Reflection.Assembly)null!));
        Assert.Equal("assembly", result.ParamName);
    }

    [Fact]
    public void GetAccessorReturnsExpectedAccessorTypes()
    {
        var context = new DocumentationContext(DocumentationTestData.ContextSource);

        Assert.IsType<TypeDocumentationAccessor>(context.GetContent("T:Example.GenericType`1"));
        Assert.IsType<MethodDocumentationAccessor>(context.GetContent("M:Example.Container.SampleMethod``1(System.String,System.Int32)"));
        Assert.IsType<MemberDocumentationAccessor>(context.GetContent("P:Example.Container.Value"));
        Assert.IsType<MemberDocumentationAccessor>(context.GetContent("F:Example.Container.Field"));
        Assert.IsType<DocumentationAccessor>(context.GetContent("Example.Custom"));
    }

    [Fact]
    public void GetAccessorReturnsNullForUnknownIdsOrMissingMembersSection()
    {
        var context = new DocumentationContext(DocumentationTestData.ContextSource);
        var emptyContext = new DocumentationContext("<doc></doc>");

        Assert.Null(context.GetContent("T:Missing"));
        Assert.Null(emptyContext.GetContent("T:Example.GenericType`1"));
    }

    [Fact]
    public void GetAccessorThrowsForNullId()
    {
        var context = new DocumentationContext(DocumentationTestData.ContextSource);

        var result = Assert.Throws<ArgumentNullException>(() => context.GetContent((String)null!));
        Assert.Equal("id", result.ParamName);
    }

    [Fact]
    public void InvalidXmlThrowsInvalidOperationExceptionWhenLookupOccurs()
    {
        var context = new DocumentationContext("<doc");

        var result = Assert.Throws<InvalidOperationException>(() => context.GetContent("T:Example.GenericType`1"));

        Assert.Equal("The XML documentation source is not valid XML.", result.Message);
        Assert.IsType<System.Xml.XmlException>(result.InnerException);
    }

    [Fact]
    public void IgnoresUnnamedAndNonMemberNodes()
    {
        var context = new DocumentationContext(DocumentationTestData.DuplicateContextSource);

        var accessor = Assert.IsType<TypeDocumentationAccessor>(context.GetContent("T:Example.GenericType`1"));
        DocumentationContentAssertions.AssertText(Assert.Single(Assert.IsType<SummaryElement>(accessor.Root.Summary).Children), "Known");
        Assert.Null(context.GetContent("T:Ignored"));
    }

    [Fact]
    public void NullSourceThrowsArgumentNullExceptionAtConstruction()
    {
        var result = Assert.Throws<ArgumentNullException>(() => new DocumentationContext(null!));
        Assert.Equal("source", result.ParamName);
    }
}
