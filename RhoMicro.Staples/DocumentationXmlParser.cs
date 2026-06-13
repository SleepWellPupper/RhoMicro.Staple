namespace RhoMicro.Staples;

using System.IO;
using System.Xml;

internal static class DocumentationXmlParser
{
    public static XmlDocument LoadDocument(String source)
    {
        var result = LoadDocument(source, "The XML documentation source is not valid XML.");
        return result;
    }

    public static XmlDocumentFragment LoadContentFragment(String source)
    {
        ArgumentNullException.ThrowIfNull(source);

        try
        {
            var document = new XmlDocument();
            var result = document.CreateDocumentFragment();
            result.InnerXml = source;

            return result;
        }
        catch(XmlException ex)
        {
            throw new InvalidOperationException("The XML documentation content is not valid XML.", ex);
        }
    }

    public static String? GetMemberSource(String source, String id)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(id);

        try
        {
            using var stringReader = new StringReader(source);
            using var reader = XmlReader.Create(stringReader);

            String? result = null;

            while(!reader.EOF)
            {
                if(reader.NodeType != XmlNodeType.Element || !reader.Name.Equals("member", StringComparison.Ordinal))
                {
                    reader.Read();
                    continue;
                }

                var currentId = reader.GetAttribute("name");

                if(currentId is null || !currentId.Equals(id, StringComparison.Ordinal))
                {
                    reader.Skip();
                    continue;
                }

                result = reader.ReadOuterXml();
            }

            return result;
        }
        catch(XmlException ex)
        {
            throw new InvalidOperationException("The XML documentation source is not valid XML.", ex);
        }
    }

    private static XmlDocument LoadDocument(String source, String invalidMessage)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(invalidMessage);

        try
        {
            var result = new XmlDocument();
            result.LoadXml(source);

            return result;
        }
        catch(XmlException ex)
        {
            throw new InvalidOperationException(invalidMessage, ex);
        }
    }
}
