namespace RhoMicro.Staples.Tests;

using Content;
using System.Reflection;

public class MethodDocumentationAccessorTests
{
    [Fact]
    public void RootExposesMethodParameterHelpers()
    {
        var accessor = new MethodDocumentationAccessor(DocumentationTestData.MethodDocumentationSource);
        var root = accessor.Root;

        var typeByType = accessor.GetTypeParameter(DocumentationTestData.MethodTypeParameterType.Name);
        var typeByName = accessor.GetTypeParameter("TMethod");
        var parameterByInfo = accessor.GetParameter(DocumentationTestData.DocumentedParameterInfo.Name ?? String.Empty);
        var parameterByName = accessor.GetParameter("value");

        Assert.NotNull(typeByType);
        Assert.NotNull(typeByName);
        Assert.NotNull(parameterByInfo);
        Assert.NotNull(parameterByName);

        Assert.Equal(2, typeByType!.Children.Count);
        DocumentationContentAssertions.AssertText(typeByType.Children[0], "Method type parameter ");
        var typeByTypeBold = DocumentationContentAssertions.AssertNode<BoldElement>(typeByType.Children[1]);
        DocumentationContentAssertions.AssertText(Assert.Single(typeByTypeBold.Children), "docs");

        Assert.Equal(2, typeByName!.Children.Count);
        DocumentationContentAssertions.AssertText(typeByName.Children[0], "Method type parameter ");
        var typeByNameBold = DocumentationContentAssertions.AssertNode<BoldElement>(typeByName.Children[1]);
        DocumentationContentAssertions.AssertText(Assert.Single(typeByNameBold.Children), "docs");

        Assert.Equal(2, parameterByInfo!.Children.Count);
        DocumentationContentAssertions.AssertText(parameterByInfo.Children[0], "Parameter ");
        var parameterByInfoItalic = DocumentationContentAssertions.AssertNode<ItalicsElement>(parameterByInfo.Children[1]);
        DocumentationContentAssertions.AssertText(Assert.Single(parameterByInfoItalic.Children), "docs");

        Assert.Equal(2, parameterByName!.Children.Count);
        DocumentationContentAssertions.AssertText(parameterByName.Children[0], "Parameter ");
        var parameterByNameItalic = DocumentationContentAssertions.AssertNode<ItalicsElement>(parameterByName.Children[1]);
        DocumentationContentAssertions.AssertText(Assert.Single(parameterByNameItalic.Children), "docs");
    }

    [Fact]
    public void RootReturnsNullForMissingMethodDocumentation()
    {
        var accessor = new MethodDocumentationAccessor(DocumentationTestData.MethodDocumentationSource);
        var root = accessor.Root;

        Assert.Null(accessor.GetTypeParameter(typeof(String).Name));
        Assert.Null(accessor.GetTypeParameter("TMissing"));
        Assert.Null(accessor.GetParameter(DocumentationTestData.UndocumentedParameterInfo.Name ?? String.Empty));
        Assert.Null(accessor.GetParameter("missing"));
    }

    [Fact]
    public void RootIgnoresUnnamedMethodElementsInNameLookup()
    {
        var accessor = new MethodDocumentationAccessor(DocumentationTestData.DuplicateMethodDocumentationSource);

        var typeParameter = Assert.IsType<TypeParameterElement>(accessor.GetTypeParameter("TMethod"));
        DocumentationContentAssertions.AssertText(Assert.Single(typeParameter.Children), "Named type parameter");

        var parameter = Assert.IsType<ParameterElement>(accessor.GetParameter("value"));
        DocumentationContentAssertions.AssertText(Assert.Single(parameter.Children), "Named parameter");

        Assert.Null(accessor.GetTypeParameter(String.Empty));
        Assert.Null(accessor.GetParameter(String.Empty));
    }
}
