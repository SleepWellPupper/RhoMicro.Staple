namespace RhoMicro.Staples;

using System.Collections.Concurrent;
using System.Reflection;
using System.Xml;
using Content;

/// <summary>
/// Provides lookup access to documentation contents by documentation comment id.
/// </summary>
public class DocumentationContext
{
    private readonly Dictionary<String, DocumentationContent> _contentsById = new(StringComparer.Ordinal);

    private DocumentationContext()
    {
    }

    public static DocumentationContext Create(IEnumerable<DocumentationContent> contents)
    {
        var result = new DocumentationContext();

        foreach (var content in contents)
        {
            result._contentsById.Add(content.Id, content);
        }

        return result;
    }

    public static DocumentationContext Create(String source)
    {
        var document = new XmlDocument();
        document.LoadXml(source);
        var result = new DocumentationContext();

        foreach (XmlNode node in document.ChildNodes)
        {
            var content = new DocumentationContent(MemberElement.Create(node, result));

            result._contentsById[content.Id] = content;
        }

        return result;
    }

    /// <summary>
    /// Creates a documentation context from the XML documentation attributes applied to the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    /// <returns>The created documentation context.</returns>
    public static DocumentationContext Create(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var attributes = assembly.GetCustomAttributes<XmlDocumentationAttribute>();
        var result = new DocumentationContext();
        var document = new XmlDocument();

        foreach (var attribute in attributes)
        {
            document.LoadXml(attribute.Source);
            var content = new DocumentationContent(MemberElement.Create(document, result));
            result._contentsById[content.Id] = content;
        }

        return result;
    }

    public DocumentationContent? GetContent(String id)
        => _contentsById.TryGetValue(id, out var result)
            ? result
            : null;
}
