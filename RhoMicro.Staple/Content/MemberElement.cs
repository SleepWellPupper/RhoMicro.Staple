// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Collections.Immutable;
using System.Runtime.InteropServices;
using System.Xml;

/// <summary>
/// Represents a <c>member</c> element.
/// </summary>
public sealed class MemberElement(
    String id,
    ImmutableArray<DocumentationContentNode> children)
    : DocumentationContentContainerElement(children)
{
    /// <summary>
    /// Gets the documentation comment id.
    /// </summary>
    public String Id { get; } = ArgumentNullException.Validate(id);

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
        => ArgumentNullException.Validate(visitor).VisitMember(this);

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
        => ArgumentNullException.Validate(visitor).VisitMember(this);

    internal static MemberElement Create(XmlNode root)
    {
        var id = GetId(root);
        var children = CreateNodes(root.ChildNodes);
        var result = new MemberElement(id, children);

        return result;
    }

    internal static String GetId(XmlNode root) => root.Attributes?["name"]?.Value ?? String.Empty;

    /// <summary>
    /// Parses a <c>member</c> element from XML source.
    /// </summary>
    /// <param name="source">The XML source containing a single <c>member</c> element.</param>
    /// <returns>The parsed member element.</returns>
    public static MemberElement Create(String source)
    {
        var document = XmlDocument.Create(source, "Unable to load member element documentation XML.");
        if (document is not
            {
                DocumentElement:
                {
                    Name: "member"
                } root
            })
        {
            throw new InvalidOperationException(
                "Unable to parse `member` element from member element documentation XML.");
        }

        var result = Create(root);

        return result;
    }

    private static ImmutableArray<DocumentationContentNode> CreateNodes(XmlNodeList nodes)
    {
        if (nodes.Count == 0)
        {
            return [];
        }

        var result = new DocumentationContentNode[nodes.Count];

        for (var i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] is not { } node)
            {
                continue;
            }

            var child = CreateNode(node);
            result[i] = child;
        }

        return ImmutableCollectionsMarshal.AsImmutableArray(result);
    }

    private static DocumentationContentNode CreateNode(XmlNode node)
    {
        var result = node switch
        {
            XmlText text => new DocumentationContentTextNode(text.Value ?? String.Empty),
            XmlCDataSection cdata => new DocumentationContentTextNode(cdata.Value ?? String.Empty),
            XmlWhitespace or XmlSignificantWhitespace => new DocumentationContentTextNode(node.Value ?? String.Empty),
            XmlElement element => CreateElementNode(element),
            _ => new UnknownElement(node.Name, new Dictionary<String, String>(), [])
        };

        return result;
    }

    private static DocumentationContentNode CreateElementNode(XmlElement element)
    {
        var children = CreateNodes(element.ChildNodes);

        DocumentationContentNode result = element.Name switch
        {
            "c" => new InlineCodeElement(children),
            "code" => new CodeElement(children),
            "i" => new ItalicsElement(children),
            "em" => new EmphasisElement(children),
            "b" => new BoldElement(children),
            "br" => new LineBreakElement(),
            "para" => new ParagraphElement(children),
            "member" => new MemberElement(
                element.GetAttribute("name"),
                children),
            "summary" => new SummaryElement(children),
            "remarks" => new RemarksElement(children),
            "returns" => new ReturnsElement(children),
            "example" => new ExampleElement(children),
            "list" => new ListElement(
                element.GetAttribute("type"),
                children),
            "listheader" => new ListHeaderElement(children),
            "item" => new ListItemElement(children),
            "term" => new ListTermElement(children),
            "description" => new ListDescriptionElement(children),
            "typeparam" => new TypeParameterElement(
                element.GetAttribute("name"),
                children),
            "param" => new ParameterElement(
                element.GetAttribute("name"),
                children),
            "see" => new SeeElement(
                element.GetAttribute("cref"),
                element.GetAttribute("href"),
                element.GetAttribute("langword"),
                element.GetAttribute("name"),
                children),
            "seealso" => new SeeAlsoElement(
                element.GetAttribute("cref"),
                element.GetAttribute("href"),
                element.GetAttribute("langword"),
                element.GetAttribute("name"),
                children),
            "inheritdoc" => new InheritdocElement(
                element.GetAttribute("cref"),
                element.GetAttribute("path"),
                children),
            "paramref" => new ParameterReferenceElement(
                element.GetAttribute("name"),
                children),
            "typeparamref" => new TypeParameterReferenceElement(
                element.GetAttribute("name"),
                children),
            _ => new UnknownElement(
                element.Name,
                CreateAttributes(element), children)
        };

        return result;
    }

    private static Dictionary<String, String> CreateAttributes(XmlElement element)
    {
        if (element.Attributes.Count == 0)
        {
            return new Dictionary<String, String>(0, StringComparer.Ordinal);
        }

        var result = new Dictionary<String, String>(element.Attributes.Count, StringComparer.Ordinal);

        foreach (XmlAttribute attribute in element.Attributes)
        {
            result[attribute.Name] = attribute.Value;
        }

        return result;
    }
}
