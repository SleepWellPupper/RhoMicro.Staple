namespace RhoMicro.Staple;

using System.Collections.Concurrent;
using System.Reflection;
using System.Xml;
using Content;

/// <summary>
/// Provides lookup access to documentation contents by documentation comment id.
/// </summary>
public class DocumentationContext
{
    /// <summary>
    /// Builds <see cref="DocumentationContext"/> instances from sources, assemblies, and parsed documentation content.
    /// </summary>
    /// <param name="throwOnDuplicateId">
    /// Whether adding the same documentation comment id more than once should throw.
    /// </param>
    /// <param name="loadAssemblyDocumentationSynchronously">
    /// Whether assembly XML documentation should be loaded synchronously.
    /// When enabled, the builder's assembly-loading members may return a completed <see cref="ValueTask"/> synchronously.
    /// </param>
    public sealed class Builder(
        Boolean throwOnDuplicateId = true,
        Boolean loadAssemblyDocumentationSynchronously = false)
    {
        private readonly ConcurrentDictionary<String, DocumentationFactory> _contentsById = [];

        /// <summary>
        /// Creates a <see cref="DocumentationContext"/> from the content that has been added to the builder.
        /// </summary>
        /// <returns>The constructed documentation context.</returns>
        public DocumentationContext Build()
        {
            var result = new DocumentationContext(
                new Dictionary<String, DocumentationFactory>(
                    _contentsById));

            return result;
        }

        private void AddFactory(String id, DocumentationFactory factory)
        {
            if (throwOnDuplicateId)
            {
                if (!_contentsById.TryAdd(id, factory))
                {
                    throw new InvalidOperationException(
                        $"Another documentation content with the id `{id}` has already been added.");
                }
            }
            else
            {
                _contentsById[id] = factory;
            }
        }

        /// <summary>
        /// Adds the documentation for each assembly in the sequence.
        /// </summary>
        /// <remarks>
        /// Each assembly is loaded from its metadata first and then from a sibling XML documentation file, if one exists.
        /// When the builder was constructed with <c>loadAssemblyDocumentationSynchronously</c> set to <c>true</c>, the returned
        /// <see cref="ValueTask"/> will complete synchronously.
        /// </remarks>
        /// <param name="assemblies">The assemblies to add documentation from.</param>
        /// <param name="ct">A token that can be used to cancel the operation.</param>
        public async ValueTask AddAssemblies(IEnumerable<Assembly> assemblies, CancellationToken ct = default)
        {
            foreach (var assembly in assemblies)
            {
                await AddAssembly(assembly, ct);
            }
        }

        /// <summary>
        /// Adds documentation from the specified assembly's metadata and XML documentation file, if present.
        /// </summary>
        /// <remarks>
        /// The XML documentation file is resolved by replacing the assembly location's extension with <c>.xml</c>.
        /// If the file does not exist, only documentation gathered from assembly metadata is added.
        /// When the builder was constructed with <c>loadAssemblyDocumentationSynchronously</c> set to <c>true</c>, the returned
        /// <see cref="ValueTask"/> will complete synchronously.
        /// </remarks>
        /// <param name="assembly">The assembly to add documentation from.</param>
        /// <param name="ct">A token that can be used to cancel the operation.</param>
        public async ValueTask AddAssembly(Assembly assembly, CancellationToken ct = default)
        {
            AddAssemblyMetadata(assembly);

            var path = Path.ChangeExtension(assembly.Location, "xml");
            if (!File.Exists(path))
            {
                return;
            }

            if (loadAssemblyDocumentationSynchronously)
            {
                // ReSharper disable once MethodHasAsyncOverloadWithCancellation
                AddAssemblySource(path);
            }
            else
            {
                var cts = new CancellationTokenSource();
                await AddAssemblySourceAsync(path, cts.Token);
            }
        }

        private void AddAssemblySource(String path)
        {
            var source = File.ReadAllText(path);

            AddSource(source);
        }

        private async Task AddAssemblySourceAsync(String path, CancellationToken ct)
        {
            var source = await File.ReadAllTextAsync(path, ct);

            AddSource(source);
        }

        private void AddAssemblyMetadata(Assembly assembly)
        {
            foreach (var attribute in assembly.GetCustomAttributes<XmlDocumentationAttribute>())
            {
                var id = attribute.Id;
                var factory =
                    DocumentationFactory.Create(() => new Documentation(MemberElement.Create(attribute.Source)));
                AddFactory(id, factory);
            }
        }

        /// <summary>
        /// Adds documentation from each XML documentation source in the sequence.
        /// </summary>
        /// <param name="sources">The XML documentation sources to add.</param>
        public void AddSources(params ReadOnlySpan<String> sources)
        {
            foreach (var source in sources)
            {
                AddSource(source);
            }
        }

        /// <summary>
        /// Adds parsed documentation content items to the builder.
        /// </summary>
        /// <param name="contents">The documentation content items to add.</param>
        public void AddContents(params ReadOnlySpan<Documentation> contents)
        {
            foreach (var content in contents)
            {
                AddContent(content);
            }
        }

        /// <summary>
        /// Adds a parsed documentation content item to the builder.
        /// </summary>
        /// <param name="content">The documentation content item to add.</param>
        public void AddContent(Documentation content) => AddFactory(content.Id, content);

        /// <summary>
        /// Adds documentation entries from an XML documentation source string.
        /// </summary>
        /// <param name="source">The XML documentation source.</param>
        public void AddSource(String source)
        {
            var root = XmlDocument.Create(source, "Unable to parse documentation XML.");
            var members = root.DocumentElement is { Name: "members" } rootMembers
                ? rootMembers
                : root.DocumentElement?.ChildNodes.OfType<XmlNode>().FirstOrDefault(n => n.Name is "members");

            if (members is null)
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
                AddFactory(id, factory);
            }
        }
    }

    private readonly Dictionary<String, DocumentationFactory> _contentsById;

    private DocumentationContext(Dictionary<String, DocumentationFactory> contentsById)
    {
        _contentsById = contentsById;
    }

    /// <summary>
    /// Creates a documentation context from the specified assemblies.
    /// </summary>
    /// <remarks>
    /// This creates a builder that ignores duplicate ids and loads each assembly's metadata plus its XML
    /// documentation file, if one exists.
    /// </remarks>
    /// <param name="assemblies">The assemblies to load documentation from.</param>
    /// <param name="ct">A token that can be used to cancel the operation.</param>
    /// <returns>The constructed documentation context.</returns>
    public static async ValueTask<DocumentationContext> Create(
        IEnumerable<Assembly> assemblies,
        CancellationToken ct = default)
    {
        var builder = new Builder(
            throwOnDuplicateId: false,
            loadAssemblyDocumentationSynchronously: false);
        await builder.AddAssemblies(assemblies, ct);
        var result = builder.Build();

        return result;
    }

    /// <summary>
    /// Creates a documentation context from the specified assembly.
    /// </summary>
    /// <remarks>
    /// This creates a builder that ignores duplicate ids and loads the assembly's metadata plus its XML
    /// documentation file, if one exists.
    /// </remarks>
    /// <param name="assembly">The assembly to load documentation from.</param>
    /// <param name="ct">A token that can be used to cancel the operation.</param>
    /// <returns>The constructed documentation context.</returns>
    public static async ValueTask<DocumentationContext> Create(Assembly assembly, CancellationToken ct = default)
    {
        var builder = new Builder(
            throwOnDuplicateId: false,
            loadAssemblyDocumentationSynchronously: false);
        await builder.AddAssembly(assembly, ct);
        var result = builder.Build();

        return result;
    }

    /// <summary>
    /// Creates a documentation context from the specified XML documentation sources.
    /// </summary>
    /// <remarks>
    /// This creates a builder that ignores duplicate ids and adds the provided XML documentation source strings
    /// directly.
    /// </remarks>
    /// <param name="sources">The XML documentation sources to load.</param>
    /// <returns>The constructed documentation context.</returns>
    public static DocumentationContext Create(params ReadOnlySpan<String> sources)
    {
        var builder = new Builder(
            throwOnDuplicateId: false,
            loadAssemblyDocumentationSynchronously: false);
        builder.AddSources(sources);
        var result = builder.Build();

        return result;
    }

    /// <summary>
    /// Creates a documentation context from the specified parsed documentation content.
    /// </summary>
    /// <remarks>
    /// This creates a builder that ignores duplicate ids and adds the provided parsed documentation content directly.
    /// </remarks>
    /// <param name="contents">The documentation content to load.</param>
    /// <returns>The constructed documentation context.</returns>
    public static DocumentationContext Create(params ReadOnlySpan<Documentation> contents)
    {
        var builder = new Builder(
            throwOnDuplicateId: false,
            loadAssemblyDocumentationSynchronously: false);
        builder.AddContents(contents);
        var result = builder.Build();

        return result;
    }

    /// <summary>
    /// Creates a documentation context from the specified documentation content item.
    /// </summary>
    /// <remarks>
    /// This creates a builder that ignores duplicate ids and adds the provided parsed documentation content directly.
    /// </remarks>
    /// <param name="content">The documentation content to load.</param>
    /// <returns>The constructed documentation context.</returns>
    public static DocumentationContext Create(Documentation content)
    {
        var builder = new Builder(
            throwOnDuplicateId: false,
            loadAssemblyDocumentationSynchronously: false);
        builder.AddContent(content);
        var result = builder.Build();

        return result;
    }

    /// <summary>
    /// Creates a documentation context from the specified XML documentation source.
    /// </summary>
    /// <remarks>
    /// This creates a builder that ignores duplicate ids and adds the provided XML documentation source directly.
    /// </remarks>
    /// <param name="source">The XML documentation source to load.</param>
    /// <returns>The constructed documentation context.</returns>
    public static DocumentationContext Create(String source)
    {
        var builder = new Builder(
            throwOnDuplicateId: false,
            loadAssemblyDocumentationSynchronously: false);
        builder.AddSource(source);
        var result = builder.Build();

        return result;
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
