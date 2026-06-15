// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple;

/// <summary>
/// Annotates an assembly with XML documentation for a single member.
/// </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
public sealed class XmlDocumentationAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="XmlDocumentationAttribute"/> class.
    /// </summary>
    /// <param name="id">The documentation comment id.</param>
    /// <param name="source">The XML documentation source.</param>
    public XmlDocumentationAttribute(String id, String source)
    {
        Id = id;
        Source = source;
    }

    /// <summary>
    /// Gets the documentation comment id.
    /// </summary>
    public String Id { get; }

    /// <summary>
    /// Gets the XML documentation content for the member.
    /// </summary>
    public String Source { get; }
}
