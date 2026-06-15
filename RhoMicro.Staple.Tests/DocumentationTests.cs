// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Tests;

using Content;

public class DocumentationTests
{
    [Fact]
    public void RootHasExpectedIdAndChildCount()
    {
        var content = new Documentation(MemberElement.Create(DocumentationTestData.DocumentationAccessorSource));

        Assert.Equal("X:Example.Item", content.Id);
        Assert.Equal("X:Example.Item", content.Root.Id);
        Assert.Equal(3, content.Root.Children.Length);
    }

    [Fact]
    public void RootSummaryContainsMixedContent()
    {
        var content = new Documentation(MemberElement.Create(DocumentationTestData.DocumentationAccessorSource));

        var summary = Assert.IsType<SummaryElement>(content.Root.Children[0]);
        var summaryTextNode = summary.Children[0];
        Assert.Equal("Summary ", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);
        var code = Assert.IsType<InlineCodeElement>(summary.Children[1]);
        var codeTextNode = Assert.Single(code.Children);
        Assert.Equal("body", Assert.IsType<DocumentationContentTextNode>(codeTextNode).Text);
    }

    [Fact]
    public void RootRemarksContainMixedContent()
    {
        var content = new Documentation(MemberElement.Create(DocumentationTestData.DocumentationAccessorSource));

        var remarks = Assert.IsType<RemarksElement>(content.Root.Children[1]);
        var remarksTextNode = remarks.Children[0];
        Assert.Equal("Remarks ", Assert.IsType<DocumentationContentTextNode>(remarksTextNode).Text);
        var remarksBold = Assert.IsType<BoldElement>(remarks.Children[1]);
        var remarksBoldTextNode = Assert.Single(remarksBold.Children);
        Assert.Equal("body", Assert.IsType<DocumentationContentTextNode>(remarksBoldTextNode).Text);
    }

    [Fact]
    public void RootExampleContainsReferenceContent()
    {
        var content = new Documentation(MemberElement.Create(DocumentationTestData.DocumentationAccessorSource));

        var example = Assert.IsType<ExampleElement>(content.Root.Children[2]);
        var exampleTextNode = example.Children[0];
        Assert.Equal("Example ", Assert.IsType<DocumentationContentTextNode>(exampleTextNode).Text);
        var exampleSee = Assert.IsType<SeeElement>(example.Children[1]);
        Assert.Equal("T:System.String", exampleSee.Cref);
    }

    [Fact]
    public void SummaryContainsMixedContent()
    {
        var content = new Documentation(MemberElement.Create(DocumentationTestData.DocumentationAccessorSource));

        var summary = Assert.IsType<SummaryElement>(content.Summary);
        var summaryTextNode = summary.Children[0];
        Assert.Equal("Summary ", Assert.IsType<DocumentationContentTextNode>(summaryTextNode).Text);
        var summaryCode = Assert.IsType<InlineCodeElement>(summary.Children[1]);
        var summaryCodeTextNode = Assert.Single(summaryCode.Children);
        Assert.Equal("body", Assert.IsType<DocumentationContentTextNode>(summaryCodeTextNode).Text);
    }

    [Fact]
    public void RemarksContainMixedContent()
    {
        var content = new Documentation(MemberElement.Create(DocumentationTestData.DocumentationAccessorSource));

        var remarks = Assert.IsType<RemarksElement>(content.Remarks);
        var remarksTextNode = remarks.Children[0];
        Assert.Equal("Remarks ", Assert.IsType<DocumentationContentTextNode>(remarksTextNode).Text);
        var remarksBold = Assert.IsType<BoldElement>(remarks.Children[1]);
        var remarksBoldTextNode = Assert.Single(remarksBold.Children);
        Assert.Equal("body", Assert.IsType<DocumentationContentTextNode>(remarksBoldTextNode).Text);
    }

    [Fact]
    public void ExampleContainsReferenceContentAtSummaryLevel()
    {
        var content = new Documentation(MemberElement.Create(DocumentationTestData.DocumentationAccessorSource));

        var example = Assert.IsType<ExampleElement>(content.Example);
        var exampleTextNode = example.Children[0];
        Assert.Equal("Example ", Assert.IsType<DocumentationContentTextNode>(exampleTextNode).Text);
        var exampleSee = Assert.IsType<SeeElement>(example.Children[1]);
        Assert.Equal("T:System.String", exampleSee.Cref);
    }

    [Fact]
    public void MissingOptionalNodesReturnNull()
    {
        var content = new Documentation(MemberElement.Create(DocumentationTestData.MissingDocumentationAccessorSource));
        var summary = Assert.IsType<SummaryElement>(content.Summary);

        Assert.Empty(summary.Children);
        Assert.Null(content.Remarks);
        Assert.Null(content.Example);
    }

    [Fact]
    public void InlineAndBlockCodeAreParsedSeparately()
    {
        var member = MemberElement.Create("<member name=\"X:Example.Item\"><summary>Summary <c>inline</c> and <code>block</code></summary></member>");

        var summary = Assert.IsType<SummaryElement>(Assert.Single(member.Children));
        Assert.Equal("Summary ", Assert.IsType<DocumentationContentTextNode>(summary.Children[0]).Text);

        var inlineCode = Assert.IsType<InlineCodeElement>(summary.Children[1]);
        Assert.Equal("inline", Assert.IsType<DocumentationContentTextNode>(Assert.Single(inlineCode.Children)).Text);

        Assert.Equal(" and ", Assert.IsType<DocumentationContentTextNode>(summary.Children[2]).Text);

        var blockCode = Assert.IsType<CodeElement>(summary.Children[3]);
        Assert.Equal("block", Assert.IsType<DocumentationContentTextNode>(Assert.Single(blockCode.Children)).Text);
    }

    [Fact]
    public void TypeParametersAreIndexedByName()
    {
        var content = new Documentation(MemberElement.Create(DocumentationTestData.TypeDocumentationSource));

        var typeParameter = Assert.Single(content.TypeParameters.Values);
        Assert.IsType<TypeParameterElement>(typeParameter);
        Assert.Equal("TValue", typeParameter.Name);
        var typeParameterTextNode = typeParameter.Children[0];
        Assert.Equal("Type parameter ", Assert.IsType<DocumentationContentTextNode>(typeParameterTextNode).Text);
        var bold = Assert.IsType<BoldElement>(typeParameter.Children[1]);
        var boldTextNode = Assert.Single(bold.Children);
        Assert.Equal("docs", Assert.IsType<DocumentationContentTextNode>(boldTextNode).Text);
    }

    [Fact]
    public void ParametersAreIndexedByName()
    {
        var content = new Documentation(MemberElement.Create(DocumentationTestData.MethodDocumentationSource));

        var parameter = Assert.Single(content.Parameters.Values);
        Assert.IsType<ParameterElement>(parameter);
        Assert.Equal("value", parameter.Name);
        var parameterTextNode = parameter.Children[0];
        Assert.Equal("Parameter ", Assert.IsType<DocumentationContentTextNode>(parameterTextNode).Text);
        var italic = Assert.IsType<ItalicsElement>(parameter.Children[1]);
        var italicTextNode = Assert.Single(italic.Children);
        Assert.Equal("docs", Assert.IsType<DocumentationContentTextNode>(italicTextNode).Text);
    }

    [Fact]
    public void InvalidXmlThrowsInvalidOperationExceptionWithXmlExceptionInner()
    {
        var result = Assert.Throws<InvalidOperationException>(() => MemberElement.Create("<member"));

        Assert.Equal("Unable to load member element documentation XML.", result.Message);
        Assert.IsType<System.Xml.XmlException>(result.InnerException);
    }

    [Fact]
    public void NullRootThrowsArgumentNullExceptionAtConstruction()
    {
        var result = Assert.Throws<ArgumentNullException>(() => new Documentation(GetNullMemberElement()));

        Assert.Equal("root", result.ParamName);
    }

#pragma warning disable CS8603
    private static MemberElement GetNullMemberElement() => default;
#pragma warning restore CS8603
}
