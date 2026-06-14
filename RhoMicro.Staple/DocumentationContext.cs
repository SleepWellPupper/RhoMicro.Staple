namespace RhoMicro.Staple;

using System.Reflection;
using System.Xml;
using Content;

/// <summary>
/// Provides lookup access to documentation contents by documentation comment id.
/// </summary>
public class DocumentationContext
{
    private readonly Dictionary<String, DocumentationFactory> _contentsById;

    private DocumentationContext(Dictionary<String, DocumentationFactory> contentsById)
    {
        _contentsById = contentsById;
    }

    /// <summary>
    /// Creates a documentation context from parsed documentation objects.
    /// </summary>
    /// <param name="contents">The documentation objects to index.</param>
    /// <returns>A documentation context containing the supplied content.</returns>
    public static DocumentationContext Create(params ReadOnlySpan<Documentation> contents)
    {
        var contentsById = new Dictionary<String, DocumentationFactory>();
        var result = new DocumentationContext(contentsById);

        foreach (var content in contents)
        {
            contentsById.Add(content.Id, content);
        }

        return result;
    }

    /// <summary>
    /// Creates a documentation context from XML documentation sources.
    /// </summary>
    /// <param name="sources">The XML documents to index.</param>
    /// <returns>A documentation context containing the parsed content.</returns>
    public static DocumentationContext Create(params ReadOnlySpan<String> sources)
    {
        var contentsById = new Dictionary<String, DocumentationFactory>();
        var result = new DocumentationContext(contentsById);

        foreach (var source in sources)
        {
            PopulateFromSource(source, contentsById);
        }

        return result;
    }

    private static void PopulateFromSource(String source, Dictionary<String, DocumentationFactory> contentsById)
    {
        var root = XmlDocument.Create(source, "Unable to parse documentation XML.").DocumentElement;
        if (root is not { Name: "members" } members)
        {
            return;
        }

        foreach (XmlNode child in members.ChildNodes)
        {
            if (child is not { Name: "member" } member)
            {
                continue;
            }

            var id = MemberElement.GetId(child);
            var factory = DocumentationFactory.Create(() => new Documentation(
                MemberElement.Create(member)));
            contentsById.Add(id, factory);
        }
    }

    /// <summary>
    /// Creates a documentation context from assemblies annotated with XML documentation metadata.
    /// </summary>
    /// <param name="assemblies">The assemblies to inspect.</param>
    /// <returns>A documentation context containing the discovered content.</returns>
    public static DocumentationContext Create(params ReadOnlySpan<Assembly> assemblies)
    {
        var contentsById = new Dictionary<String, DocumentationFactory>();
        var result = new DocumentationContext(contentsById);

        foreach (var assembly in assemblies)
        {
            PopulateFromAssembly(assembly, contentsById);
        }

        return result;
    }

    private static void PopulateFromAssembly(Assembly assembly, Dictionary<String, DocumentationFactory> contentsById)
    {
        foreach (var attribute in assembly.GetCustomAttributes<XmlDocumentationAttribute>())
        {
            var id = attribute.Id;
            var factory = DocumentationFactory.Create(() => new Documentation(MemberElement.Create(attribute.Source)));
            contentsById.Add(id, factory);
        }
    }

    /// <summary>
    /// Gets the documentation accessor for the specified documentation comment id.
    /// </summary>
    /// <param name="id">The documentation comment id.</param>
    /// <returns>The matching documentation accessor, or <see langword="null"/> if none exists.</returns>
    public Documentation? GetContent(String id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var result = _contentsById.TryGetValue(id, out var content)
            ? content.CreateContent()
            : null;

        return result;
    }
}
