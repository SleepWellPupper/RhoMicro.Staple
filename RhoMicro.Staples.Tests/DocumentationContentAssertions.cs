namespace RhoMicro.Staples.Tests;

using Content;

internal static class DocumentationContentAssertions
{
    public static DocumentationContentTextNode AssertText(DocumentationContentNode node, String expectedText)
    {
        var result = Assert.IsType<DocumentationContentTextNode>(node);
        Assert.Equal(expectedText, result.Text);
        return result;
    }

    public static TElement AssertNode<TElement>(DocumentationContentNode node)
        where TElement : DocumentationContentNode
    {
        var result = Assert.IsType<TElement>(node);
        return result;
    }
}
