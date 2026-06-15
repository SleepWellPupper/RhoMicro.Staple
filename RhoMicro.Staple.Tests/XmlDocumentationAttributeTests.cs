// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Tests;

using Staple;

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
    public void AttributeUsageIsAssemblyOnlyAndRepeatable()
    {
        var usage = Assert.Single(typeof(XmlDocumentationAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), inherit: false));
        var result = Assert.IsType<AttributeUsageAttribute>(usage);

        Assert.Equal(AttributeTargets.Assembly, result.ValidOn);
        Assert.True(result.AllowMultiple);
        Assert.False(result.Inherited);
    }
}
