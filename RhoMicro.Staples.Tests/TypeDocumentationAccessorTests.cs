namespace RhoMicro.Staples.Tests;

using Content;

public class TypeDocumentationAccessorTests
{
    [Fact]
    public void RootExposesTypeParameterHelpers()
    {
        var documentation = new TypeDocumentationAccessor(DocumentationTestData.TypeDocumentationSource);
        var root = documentation.Root;

        var byType = documentation.GetTypeParameter(DocumentationTestData.TypeParameterType.Name);
        var byName = documentation.GetTypeParameter("TValue");

        Assert.NotNull(byType);
        Assert.NotNull(byName);
        Assert.Equal(2, byType!.Children.Count);
        DocumentationContentAssertions.AssertText(byType.Children[0], "Type parameter ");
        var byTypeBold = DocumentationContentAssertions.AssertNode<BoldElement>(byType.Children[1]);
        DocumentationContentAssertions.AssertText(Assert.Single(byTypeBold.Children), "docs");

        Assert.Equal(2, byName!.Children.Count);
        DocumentationContentAssertions.AssertText(byName.Children[0], "Type parameter ");
        var byNameBold = DocumentationContentAssertions.AssertNode<BoldElement>(byName.Children[1]);
        DocumentationContentAssertions.AssertText(Assert.Single(byNameBold.Children), "docs");
    }

    [Fact]
    public void RootReturnsNullForMissingTypeParameter()
    {
        var documentation = new TypeDocumentationAccessor(DocumentationTestData.TypeDocumentationSource);
        var root = documentation.Root;

        Assert.Null(documentation.GetTypeParameter(typeof(String).Name));
        Assert.Null(documentation.GetTypeParameter("TMissing"));
    }

    [Fact]
    public void RootIgnoresUnnamedTypeParameterInNameLookup()
    {
        var documentation = new TypeDocumentationAccessor(DocumentationTestData.DuplicateTypeDocumentationSource);

        var typeParameter = Assert.IsType<TypeParameterElement>(documentation.GetTypeParameter("TValue"));
        DocumentationContentAssertions.AssertText(Assert.Single(typeParameter.Children), "Named type parameter");
        Assert.Null(documentation.GetTypeParameter(String.Empty));
    }
}
