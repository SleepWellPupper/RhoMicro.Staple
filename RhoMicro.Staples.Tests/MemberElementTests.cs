namespace RhoMicro.Staples.Tests;

public class MemberElementTests
{
    [Fact]
    public void NameBasedHelpersThrowForNullArguments()
    {
        var accessor = new MethodDocumentationAccessor(DocumentationTestData.MethodDocumentationSource);

        var typeNameResult = Assert.Throws<ArgumentNullException>(() => accessor.GetTypeParameter((String)null!));
        var parameterNameResult = Assert.Throws<ArgumentNullException>(() => accessor.GetParameter((String)null!));

        Assert.Equal("typeParameterName", typeNameResult.ParamName);
        Assert.Equal("parameterName", parameterNameResult.ParamName);
    }

    [Fact]
    public void ConcurrentCacheAccessDoesNotThrow()
    {
        var accessor = new MethodDocumentationAccessor(DocumentationTestData.MethodDocumentationSource);

        Parallel.For(0, 64, _ =>
        {
            Assert.NotNull(accessor.Root.Summary);
            Assert.NotNull(accessor.Root.Remarks);
            Assert.NotNull(accessor.Root.Example);
            Assert.Single(accessor.TypeParameters);
            Assert.Single(accessor.Parameters);
            Assert.NotNull(accessor.GetTypeParameter("TMethod"));
            Assert.NotNull(accessor.GetParameter("value"));
        });
    }
}
