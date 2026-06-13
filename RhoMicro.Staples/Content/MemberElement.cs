namespace RhoMicro.Staples.Content;

using System.Xml;

/// <summary>
/// Represents a <c>member</c> element.
/// </summary>
public sealed class MemberElement(
    String id,
    String source,
    IReadOnlyList<DocumentationContentNode> children)
    : DocumentationContentContainerElement(source, children)
{
    /// <summary>
    /// Gets the documentation comment id.
    /// </summary>
    public String Id { get; } = ArgumentNullException.Validate(id);

    /// <inheritdoc />
    public override void Accept(IDocumentationContentVisitor visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        visitor.VisitMember(this);
    }

    internal static MemberElement Create(String source, DocumentationContext context)
    {
        var document = DocumentationXmlParser.LoadDocument(source);
        var result = Create(document, context);

        return result;
    }

    internal static MemberElement Create(XmlDocument document, DocumentationContext context)
    {
        var result = document.DocumentElement is { } root
            ? Create(root, context)
            : new MemberElement(String.Empty, document.OuterXml, []);

        return result;
    }

    internal static MemberElement Create(XmlNode source, DocumentationContext context)
    {
        ArgumentNullException.ThrowIfNull(source);

        var id = source.Attributes?["name"]?.Value ?? String.Empty;
        var children = CreateNodes(source.ChildNodes, context);
        var result = new MemberElement(id, source.OuterXml, children);

        return result;
    }

    /// <inheritdoc />
    public override TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor)
    {
        ArgumentNullException.ThrowIfNull(visitor);
        var result = visitor.VisitMember(this);
        return result;
    }

    private static IReadOnlyList<DocumentationContentNode> CreateNodes(XmlNodeList nodes, DocumentationContext context)
    {
        if (nodes.Count == 0)
        {
            return [];
        }

        var result = new List<DocumentationContentNode>(nodes.Count);

        foreach (XmlNode node in nodes)
        {
            var child = CreateNode(node, context);
            if (child is not null)
            {
                result.Add(child);
            }
        }

        return result;
    }

    private static DocumentationContentNode? CreateNode(XmlNode node, DocumentationContext context)
    {
        DocumentationContentNode? result;

        switch (node)
        {
            case XmlText text:
                result = new DocumentationContentTextNode(text.Value ?? String.Empty);
                break;
            case XmlCDataSection cdata:
                result = new DocumentationContentTextNode(cdata.Value ?? String.Empty);
                break;
            case XmlWhitespace:
            case XmlSignificantWhitespace:
                result = new DocumentationContentTextNode(node.Value ?? String.Empty);
                break;
            case XmlElement element:
                result = CreateElementNode(element, context);
                break;
            default:
                result = null;
                break;
        }

        return result;
    }

    private static DocumentationContentNode CreateElementNode(
        XmlElement element,
        DocumentationContext context)
    {
        var children = CreateNodes(element.ChildNodes, context);

        DocumentationContentNode result = element.Name switch
        {
            "c" => new CodeElement(
                element.OuterXml,
                children),
            "i" => new ItalicsElement(
                element.OuterXml,
                children),
            "em" => new EmphasisElement(
                element.OuterXml,
                children),
            "b" => new BoldElement(
                element.OuterXml,
                children),
            "br" => new LineBreakElement(
                element.OuterXml),
            "para" => new ParagraphElement(
                element.OuterXml,
                children),
            "member" => new MemberElement(
                element.OuterXml,
                element.GetAttribute("name"),
                children),
            "summary" => new SummaryElement(
                element.OuterXml,
                children),
            "remarks" => new RemarksElement(
                element.OuterXml,
                children),
            "example" => new ExampleElement(
                element.OuterXml,
                children),
            "list" => new ListElement(
                element.OuterXml,
                element.GetAttribute("type"),
                children),
            "listheader" => new ListHeaderElement(
                element.OuterXml,
                children),
            "item" => new ListItemElement(
                element.OuterXml,
                children),
            "term" => new ListTermElement(
                element.OuterXml,
                children),
            "description" => new ListDescriptionElement(
                element.OuterXml,
                children),
            "typeparam" => new TypeParameterElement(
                element.OuterXml,
                element.GetAttribute("name"),
                children),
            "param" => new ParameterElement(
                element.OuterXml,
                element.GetAttribute("name"),
                children),
            "see" => new SeeElement(
                element.GetAttribute("cref"),
                element.GetAttribute("href"),
                element.GetAttribute("langword"),
                element.GetAttribute("name"),
                element.OuterXml,
                children),
            "seealso" => new SeeAlsoElement(
                element.GetAttribute("cref"),
                element.GetAttribute("href"),
                element.GetAttribute("langword"),
                element.GetAttribute("name"),
                element.OuterXml,
                children),
            "inheritdoc" => new InheritdocElement(
                element.GetAttribute("cref"),
                element.GetAttribute("path"),
                element.OuterXml,
                children),
            "paramref" => new ParameterReferenceElement(
                element.GetAttribute("name"),
                element.OuterXml,
                children),
            "typeparamref" => new TypeParameterReferenceElement(
                element.GetAttribute("name"),
                element.OuterXml,
                children),
            _ => new UnknownElement(
                element.Name,
                CreateAttributes(element),
                element.OuterXml,
                children)
        };

        return result;
    }

    private static IReadOnlyDictionary<String, String> CreateAttributes(XmlElement element)
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
