// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Tests
{
    using System.Reflection;
    using Foo;

    public class ReadmeTests
    {
        [Fact]
        public void GeneratedAccess()
        {
            var barSummary = typeof(Bar).Documentation?.Summary?.InnerXml;
            var baz = typeof(Bar).GetMethod("Baz");
            var bazSummary = baz?.Documentation?.Summary?.InnerXml;
            var bazReturns = baz?.Documentation?.Returns?.InnerXml;

            Assert.Equal("This is the <c>Bar</c> class.", barSummary);
            Assert.Equal("This is the <see cref=\"M:Foo.Bar.Baz\"/> method.", bazSummary);
            Assert.Equal("This method always returns <c>0</c> (<see langword=\"default\"/>).", bazReturns);
        }

        [Fact]
        public void ManualAccess()
        {
            var context = DocumentationContext.Create(
                """
                <members>
                <member name="T:Foo.Bar">
                <summary>This is an artificial summary.</summary>
                </member>
                </members>
                """);
            var barSummary = context.GetContent(typeof(Bar).DocumentationId)?.Summary?.InnerXml;

            Assert.Equal("This is an artificial summary.", barSummary);
        }
    }
}

namespace Foo
{
    /// <summary>This is the <c>Bar</c> class.</summary>
    public static class Bar
    {
        /// <summary>This is the <see cref="Baz"/> method.</summary>
        /// <returns>This method always returns <c>0</c> (<see langword="default"/>).</returns>
        public static int Baz() => 0;
    }
}
