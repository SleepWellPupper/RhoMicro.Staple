namespace RhoMicro.Staples.Tests;

using Content;

public class MemberDocumentationAccessorTests
{
    [Fact]
    public void InheritedDocumentationPropertiesRemainAvailable()
    {
        var accessor = new MemberDocumentationAccessor(DocumentationTestData.MemberDocumentationSource);
        var root = accessor.Root;

        Assert.Equal("P:Example.Container.Value", accessor.Id);
        DocumentationContentAssertions.AssertText(Assert.Single(Assert.IsType<SummaryElement>(root.Summary).Children), "Property summary");
        DocumentationContentAssertions.AssertText(Assert.Single(Assert.IsType<RemarksElement>(root.Remarks).Children), "Property remarks");
        DocumentationContentAssertions.AssertText(Assert.Single(Assert.IsType<ExampleElement>(root.Example).Children), "Property example");
    }
}
