namespace RhoMicro.Staples.Content;

using Staples;

public sealed class DocumentationContent
{
    public DocumentationContent(DocumentationContentNode root)
    {
        Root = root;

        Id = root is MemberElement { Id: { } id }
            ? id
            : String.Empty;

        _summary = new Lazy<SummaryElement?>(FindFirstChild<SummaryElement>);
        _remarks = new Lazy<RemarksElement?>(FindFirstChild<RemarksElement>);
        _example = new Lazy<ExampleElement?>(FindFirstChild<ExampleElement>);
        _typeParameters = new Lazy<Dictionary<String, TypeParameterElement>>(CreateTypeParameterCache);
        _parameters = new Lazy<Dictionary<String, ParameterElement>>(CreateParameterCache);
    }

    private readonly Lazy<SummaryElement?> _summary;
    private readonly Lazy<RemarksElement?> _remarks;
    private readonly Lazy<ExampleElement?> _example;
    private readonly Lazy<Dictionary<String, TypeParameterElement>> _typeParameters;
    private readonly Lazy<Dictionary<String, ParameterElement>> _parameters;

    public DocumentationContentNode Root { get; }

    /// <summary>
    /// Gets the documentation comment id.
    /// </summary>
    public String Id { get; }

    /// <summary>
    /// Gets the summary element.
    /// </summary>
    public SummaryElement? Summary => _summary.Value;

    /// <summary>
    /// Gets the remarks element.
    /// </summary>
    public RemarksElement? Remarks => _remarks.Value;

    /// <summary>
    /// Gets the example element.
    /// </summary>
    public ExampleElement? Example => _example.Value;

    /// <summary>
    /// Gets the type parameter elements.
    /// </summary>
    public IReadOnlyDictionary<String, TypeParameterElement> TypeParameters => _typeParameters.Value;

    /// <summary>
    /// Gets the parameter elements.
    /// </summary>
    public IReadOnlyDictionary<String, ParameterElement> Parameters => _parameters.Value;

    private TNode? FindFirstChild<TNode>() where TNode : DocumentationContentNode
    {
        TNode? result = null;

        foreach (var child in Root.Children)
        {
            if (child is TNode typedChild)
            {
                result = typedChild;
                break;
            }
        }

        return result;
    }

    private Dictionary<String, TypeParameterElement> CreateTypeParameterCache()
    {
        var result = CreateNamedChildrenCache<TypeParameterElement>(Root.Children, static element => element.Name);
        return result;
    }

    private Dictionary<String, ParameterElement> CreateParameterCache()
    {
        var result = CreateNamedChildrenCache<ParameterElement>(Root.Children, static element => element.Name);
        return result;
    }

    private static Dictionary<String, TElement> CreateNamedChildrenCache<TElement>(
        IReadOnlyList<DocumentationContentNode> children, Func<TElement, String> getName)
        where TElement : DocumentationContentNode
    {
        ArgumentNullException.ThrowIfNull(children);
        ArgumentNullException.ThrowIfNull(getName);

        var result = new Dictionary<String, TElement>(StringComparer.Ordinal);

        foreach (var child in children)
        {
            if (child is not TElement element)
            {
                continue;
            }

            var name = getName(element);
            if (name.Length > 0)
            {
                result[name] = element;
            }
        }

        return result;
    }
}
