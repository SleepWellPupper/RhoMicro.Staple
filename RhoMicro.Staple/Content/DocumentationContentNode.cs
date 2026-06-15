// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple.Content;

using System.Text;

/// <summary>
/// Represents a node in documentation content.
/// </summary>
public abstract class DocumentationContentNode
{
    /// <summary>
    /// Gets a reconstruction of the outer XML used to create the node.
    /// </summary>
    public String OuterXml
    {
        get
        {
            if (field is not null)
            {
                return field;
            }

            var builder = new StringBuilder();
            var visitor = new OuterXmlVisitor(builder);
            Accept(visitor);
            var value = builder.ToString();

            Interlocked.CompareExchange(ref field, value, null);

            return field;
        }
    }

    /// <summary>
    /// Gets a reconstruction of the inner XML used to create the node.
    /// </summary>
    public String InnerXml
    {
        get
        {
            if (field is not null)
            {
                return field;
            }

            var builder = new StringBuilder();
            var visitor = new InnerXmlVisitor(builder);
            Accept(visitor);
            var value = builder.ToString();

            Interlocked.CompareExchange(ref field, value, null);

            return field;
        }
    }

    /// <summary>
    /// Accepts a visitor.
    /// </summary>
    /// <param name="visitor">The visitor.</param>
    public abstract void Accept(IDocumentationContentVisitor visitor);

    /// <summary>
    /// Accepts a visitor.
    /// </summary>
    /// <typeparam name="TResult">The visit result type.</typeparam>
    /// <param name="visitor">The visitor.</param>
    /// <returns>The visit result.</returns>
    public abstract TResult Accept<TResult>(IDocumentationContentVisitor<TResult> visitor);

    /// <inheritdoc />
    public override String ToString() => OuterXml;
}
