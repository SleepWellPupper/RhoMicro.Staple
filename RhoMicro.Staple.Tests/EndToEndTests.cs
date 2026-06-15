// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Tests;

using System.Reflection;
using Content;

public class EndToEndTests
{
    [Fact]
    public void TypeDocumentationIsLoadedFromSourceComments()
    {
        var documentation = Assert.IsType<Documentation>(typeof(SourceDocumentedContainer<>).Documentation);

        Assert.Equal("T:RhoMicro.Staple.Tests.EndToEndTests.SourceDocumentedContainer`1", documentation.Id);

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var typeSummaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("Source documented container type.", Assert.IsType<DocumentationContentTextNode>(typeSummaryTextNode).Text);

        var typeParameter = Assert.Single(documentation.TypeParameters.Values);
        Assert.IsType<TypeParameterElement>(typeParameter);
        Assert.Equal("TContainer", typeParameter.Name);
        var typeParameterTextNode = Assert.Single(typeParameter.Children);
        Assert.Equal("Type parameter docs.", Assert.IsType<DocumentationContentTextNode>(typeParameterTextNode).Text);
    }

    [Fact]
    public void ConstructorDocumentationIsLoadedFromSourceComments()
    {
        var type = typeof(SourceDocumentedContainer<>);
        var constructor = Assert.IsAssignableFrom<ConstructorInfo>(type.GetConstructor(Type.EmptyTypes));

        var documentation = Assert.IsType<Documentation>(constructor.Documentation);

        Assert.Equal("M:RhoMicro.Staple.Tests.EndToEndTests.SourceDocumentedContainer`1.#ctor", documentation.Id);

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var summaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("Creates the documented container.", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);
    }

    [Fact]
    public void PropertyDocumentationIsLoadedFromSourceComments()
    {
        var type = typeof(SourceDocumentedContainer<>);
        var property = Assert.IsAssignableFrom<PropertyInfo>(type.GetProperty(nameof(SourceDocumentedContainer<int>.Value), BindingFlags.Instance | BindingFlags.Public));

        var documentation = Assert.IsType<Documentation>(property.Documentation);

        Assert.Equal("P:RhoMicro.Staple.Tests.EndToEndTests.SourceDocumentedContainer`1.Value", documentation.Id);

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var summaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("A documented property.", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);
    }

    [Fact]
    public void FieldDocumentationIsLoadedFromSourceComments()
    {
        var type = typeof(SourceDocumentedContainer<>);
        var field = Assert.IsAssignableFrom<FieldInfo>(type.GetField(nameof(SourceDocumentedContainer<int>.Field), BindingFlags.Static | BindingFlags.Public));

        var documentation = Assert.IsType<Documentation>(field.Documentation);

        Assert.Equal("F:RhoMicro.Staple.Tests.EndToEndTests.SourceDocumentedContainer`1.Field", documentation.Id);

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var summaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("A documented field.", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);
    }

    [Fact]
    public void EventDocumentationIsLoadedFromSourceComments()
    {
        var type = typeof(SourceDocumentedContainer<>);
        var eventInfo = Assert.IsAssignableFrom<EventInfo>(type.GetEvent(nameof(SourceDocumentedContainer<int>.Changed), BindingFlags.Instance | BindingFlags.Public));

        var documentation = Assert.IsType<Documentation>(eventInfo.Documentation);

        Assert.Equal("E:RhoMicro.Staple.Tests.EndToEndTests.SourceDocumentedContainer`1.Changed", documentation.Id);

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var summaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("A documented event.", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);
    }

    [Fact]
    public void MethodDocumentationIsLoadedFromSourceComments()
    {
        var type = typeof(SourceDocumentedContainer<>);
        var method = Assert.IsAssignableFrom<MethodInfo>(type.GetMethod(nameof(SourceDocumentedContainer<int>.Echo)));

        var documentation = Assert.IsType<Documentation>(method.Documentation);

        Assert.Equal("M:RhoMicro.Staple.Tests.EndToEndTests.SourceDocumentedContainer`1.Echo``1(``0)", documentation.Id);

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var summaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("Runs a documented method.", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);

        var methodTypeParameter = Assert.Single(documentation.TypeParameters.Values);
        Assert.IsType<TypeParameterElement>(methodTypeParameter);
        var methodTypeParameterTextNode = Assert.Single(methodTypeParameter.Children);
        Assert.Equal("Method type parameter docs.", Assert.IsType<DocumentationContentTextNode>(methodTypeParameterTextNode).Text);

        var methodParameter = Assert.Single(documentation.Parameters.Values);
        Assert.IsType<ParameterElement>(methodParameter);
        var methodParameterTextNode = Assert.Single(methodParameter.Children);
        Assert.Equal("Method parameter docs.", Assert.IsType<DocumentationContentTextNode>(methodParameterTextNode).Text);
    }

    [Fact]
    public void InternalPropertyDocumentationIsLoadedFromSourceComments()
    {
        var type = typeof(SourceDocumentedContainer<>);
        var property = Assert.IsAssignableFrom<PropertyInfo>(type.GetProperty(nameof(SourceDocumentedContainer<int>.InternalValue), BindingFlags.Instance | BindingFlags.NonPublic));

        var documentation = Assert.IsType<Documentation>(property.Documentation);

        Assert.Equal("P:RhoMicro.Staple.Tests.EndToEndTests.SourceDocumentedContainer`1.InternalValue", documentation.Id);

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var summaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("A documented internal property.", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);
    }

    [Fact]
    public void InternalMethodDocumentationIsLoadedFromSourceComments()
    {
        var type = typeof(SourceDocumentedContainer<>);
        var method = Assert.IsAssignableFrom<MethodInfo>(type.GetMethod(nameof(SourceDocumentedContainer<int>.Describe), BindingFlags.Static | BindingFlags.NonPublic));

        var documentation = Assert.IsType<Documentation>(method.Documentation);

        Assert.Equal("M:RhoMicro.Staple.Tests.EndToEndTests.SourceDocumentedContainer`1.Describe``1(System.String)", documentation.Id);

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var summaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("Describes an internal value.", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);

        var methodTypeParameter = Assert.Single(documentation.TypeParameters.Values);
        Assert.IsType<TypeParameterElement>(methodTypeParameter);
        var methodTypeParameterTextNode = Assert.Single(methodTypeParameter.Children);
        Assert.Equal("The internal result type.", Assert.IsType<DocumentationContentTextNode>(methodTypeParameterTextNode).Text);

        var methodParameter = Assert.Single(documentation.Parameters.Values);
        Assert.IsType<ParameterElement>(methodParameter);
        var methodParameterTextNode = Assert.Single(methodParameter.Children);
        Assert.Equal("The value to describe.", Assert.IsType<DocumentationContentTextNode>(methodParameterTextNode).Text);
    }

    [Fact]
    public void NestedTypeDocumentationIsLoadedFromSourceComments()
    {
        var documentation = Assert.IsType<Documentation>(typeof(SourceDocumentedContainer<>.Nested).Documentation);

        Assert.Equal("T:RhoMicro.Staple.Tests.EndToEndTests.SourceDocumentedContainer`1.Nested", documentation.Id);

        var summary = Assert.IsType<SummaryElement>(documentation.Summary);
        var nestedTypeSummaryTextNode = Assert.Single(summary.Children);
        Assert.Equal("Nested source-documented type.", Assert.IsType<DocumentationContentTextNode>(nestedTypeSummaryTextNode).Text);
    }

    /// <summary>Source documented container type.</summary>
    /// <typeparam name="TContainer">Type parameter docs.</typeparam>
    public class SourceDocumentedContainer<TContainer>
    {
        /// <summary>Creates the documented container.</summary>
        public SourceDocumentedContainer()
        {
        }

        /// <summary>A documented property.</summary>
        public String Value { get; set; } = String.Empty;

        /// <summary>A documented internal property.</summary>
        internal String InternalValue { get; set; } = String.Empty;

        /// <summary>A documented field.</summary>
        public const Int32 Field = 1;

        /// <summary>A documented event.</summary>
        public event EventHandler? Changed;

        /// <summary>Runs a documented method.</summary>
        /// <param name="value">Method parameter docs.</param>
        /// <typeparam name="TMethod">Method type parameter docs.</typeparam>
        public TMethod Echo<TMethod>(TMethod value)
        {
            var result = value;

            return result;
        }

        /// <summary>Describes an internal value.</summary>
        /// <typeparam name="TResult">The internal result type.</typeparam>
        /// <param name="value">The value to describe.</param>
        internal static TResult Describe<TResult>(String value)
        {
            var result = default(TResult);

            return result!;
        }

        /// <summary>Nested source-documented type.</summary>
        public sealed class Nested
        {
        }
    }
}
