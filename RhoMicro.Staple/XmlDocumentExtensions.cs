// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple;

using System.Xml;

internal static class XmlDocumentExtensions
{
    extension(XmlDocument)
    {
        public static XmlDocument Create(String source, String exceptionMessage)
        {
            ArgumentNullException.Validate(source);

            try
            {
                var result = new XmlDocument();

                result.LoadXml(source);

                return result;
            }
            catch (XmlException ex)
            {
                throw new InvalidOperationException(exceptionMessage, ex);
            }
        }
    }
}
