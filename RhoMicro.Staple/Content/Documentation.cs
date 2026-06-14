namespace RhoMicro.Staple.Content;

/// <summary>
/// Provides parsed documentation content for a member element.
/// </summary>
public sealed class Documentation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Documentation"/> class.
    /// </summary>
    /// <param name="root">The root member element.</param>
    public Documentation(MemberElement root)
    {
        Root = ArgumentNullException.ThrowIfNull(root);
    }

    /// <summary>
    /// Gets the root member element.
    /// </summary>
    public MemberElement Root { get; }

    /// <summary>
    /// Gets the documentation comment id.
    /// </summary>
    public String Id => Root.Id;

    /// <summary>
    /// Gets the summary element.
    /// </summary>
    public SummaryElement? Summary => field ??= FindFirstChild<SummaryElement>();

    /// <summary>
    /// Gets the remarks element.
    /// </summary>
    public RemarksElement? Remarks => field ??= FindFirstChild<RemarksElement>();

    /// <summary>
    /// Gets the example element.
    /// </summary>
    public ExampleElement? Example => field ??= FindFirstChild<ExampleElement>();

    /// <summary>
    /// Gets the type parameter elements.
    /// </summary>
    public IReadOnlyDictionary<String, TypeParameterElement> TypeParameters
        => field ??= CreateNamedChildrenMap<TypeParameterElement>(Root.Children, static element => element.Name);

    /// <summary>
    /// Gets the parameter elements.
    /// </summary>
    public IReadOnlyDictionary<String, ParameterElement> Parameters
        => field ??= CreateNamedChildrenMap<ParameterElement>(Root.Children, static element => element.Name);

    private TNode? FindFirstChild<TNode>() where TNode : DocumentationContentNode
    {
        TNode? result = null;

        foreach (var child in Root.Children)
        {
            if (child is not TNode typedChild)
            {
                continue;
            }

            result = typedChild;
            break;
        }

        return result;
    }

    private static Dictionary<String, TElement> CreateNamedChildrenMap<TElement>(
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
