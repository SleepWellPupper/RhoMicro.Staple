namespace RhoMicro.Staples;

/// <summary>
/// Annotates an assembly with XML documentation for a single member.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public sealed class XmlDocumentationAttribute(String source) : Attribute
{
    /// <summary>
    /// Gets the XML documentation content for the member.
    /// </summary>
    public String Source { get; } = ArgumentNullException.Validate(source);
}
