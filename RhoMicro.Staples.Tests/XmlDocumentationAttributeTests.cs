namespace RhoMicro.Staples.Tests;

public class XmlDocumentationAttributeTests
{
    [Fact]
    public void ConstructorStoresIdAndSource()
    {
        var attribute = new XmlDocumentationAttribute("T:Example.Type", "<summary>Hello</summary>");

        Assert.Equal("T:Example.Type", attribute.Id);
        Assert.Equal("<summary>Hello</summary>", attribute.Source);
    }

    [Fact]
    public void NullIdOrSourceThrowsArgumentNullExceptionAtConstruction()
    {
        var idResult = Assert.Throws<ArgumentNullException>(() => new XmlDocumentationAttribute(null!, "<summary />"));
        var sourceResult = Assert.Throws<ArgumentNullException>(() => new XmlDocumentationAttribute("T:Example.Type", null!));

        Assert.Equal("id", idResult.ParamName);
        Assert.Equal("source", sourceResult.ParamName);
    }

    [Fact]
    public void AttributeUsageIsAssemblyOnlyAndRepeatable()
    {
        var usage = Assert.Single(typeof(XmlDocumentationAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), inherit: false));
        var result = Assert.IsType<AttributeUsageAttribute>(usage);

        Assert.Equal(AttributeTargets.Assembly, result.ValidOn);
        Assert.True(result.AllowMultiple);
        Assert.False(result.Inherited);
    }
}
