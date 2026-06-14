namespace RhoMicro.Staple.Tests;

using Content;
using Staple;

public class DocumentationAccessorTests
{
    [Fact]
    public void AssemblyDocumentationContextIsCached()
    {
        var assembly = typeof(DocumentationAccessorTests).Assembly;

        Assert.Same(assembly.DocumentationContext, assembly.DocumentationContext);
    }

    [Fact]
    public void TypeDocumentationIdUsesMemberInfoExtension()
    {
        Assert.Equal(DocumentationTestData.DocumentedTypeId, DocumentationTestData.DocumentedType.DocumentationId);
    }

    [Fact]
    public void MemberDocumentationIdUsesMemberInfoExtension()
    {
        Assert.Equal(DocumentationTestData.DocumentedMethodId, DocumentationTestData.DocumentedMethodInfo.DocumentationId);
    }

    [Fact]
    public void TypeDocumentationIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedType.Documentation);
    }

    [Fact]
    public void TypeDocumentationRemarksIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedType.Documentation);
    }

    [Fact]
    public void TypeDocumentationExampleIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedType.Documentation);
    }

    [Fact]
    public void TypeDocumentationTypeParameterIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedType.Documentation);
    }

    [Fact]
    public void MethodDocumentationIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedMethodInfo.Documentation);
    }

    [Fact]
    public void MethodDocumentationTypeParameterIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedMethodInfo.Documentation);
    }

    [Fact]
    public void MethodDocumentationParameterIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedMethodInfo.Documentation);
    }

    [Fact]
    public void ConstructorDocumentationIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedConstructorInfo.Documentation);
    }

    [Fact]
    public void PropertyDocumentationIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedPropertyInfo.Documentation);
    }

    [Fact]
    public void PropertyDocumentationRemarksIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedPropertyInfo.Documentation);
    }

    [Fact]
    public void PropertyDocumentationExampleIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedPropertyInfo.Documentation);
    }

    [Fact]
    public void FieldDocumentationIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedFieldInfo.Documentation);
    }

    [Fact]
    public void EventDocumentationIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedEventInfo.Documentation);
    }

    [Fact]
    public void NestedTypeDocumentationIsNullWhenNoAssemblyMetadataExists()
    {
        Assert.Null(DocumentationTestData.DocumentedNestedType.Documentation);
    }
}
