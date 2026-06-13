namespace RhoMicro.Staples.Tests;

using System.Reflection;

internal static class DocumentationTestData
{
    public const String DocumentationAccessorSource = "<member name=\"X:Example.Item\"><summary>Summary <c>body</c></summary><remarks>Remarks <b>body</b></remarks><example>Example <see cref=\"T:System.String\"/></example></member>";
    public const String DocumentationAccessorBodySource = "<summary>Summary <c>body</c></summary><remarks>Remarks <b>body</b></remarks><example>Example <see cref=\"T:System.String\"/></example>";
    public const String XmlDocumentationContentSource = "Summary <c>body</c> and <paramref name=\"value\"/>";
    public const String XmlDocumentationFormattingContentSource = "<i>Italic</i> and <em>emphasized</em> and <b>bold</b>";
    public const String XmlDocumentationParagraphContentSource = "Before<br/>Middle<para>Paragraph <see cref=\"T:System.String\"/> <paramref name=\"value\"/></para>After";
    public const String XmlDocumentationReferenceContentSource = "<see cref=\"T:System.String\"/> <see langword=\"null\"/> <inheritdoc cref=\"M:Example.Method\"/>";
    public const String XmlDocumentationListContentSource = "<list type=\"table\"><listheader><term>Header term</term><description>Header description</description></listheader><item><term>Row term</term><description>Row description</description></item></list>";
    public const String MissingDocumentationAccessorSource = "<member><summary></summary></member>";
    public const String TypeDocumentationSource = "<member name=\"T:Example.GenericType`1\"><summary>Type summary</summary><remarks>Type remarks</remarks><example>Type example</example><typeparam name=\"TValue\">Type parameter <b>docs</b></typeparam></member>";
    public const String TypeDocumentationBodySource = "<summary>Type summary</summary><remarks>Type remarks</remarks><example>Type example</example><typeparam name=\"TValue\">Type parameter <b>docs</b></typeparam>";
    public const String DuplicateTypeDocumentationSource = "<member name=\"T:Example.GenericType`1\"><typeparam>Ignored</typeparam><typeparam name=\"TValue\">Named type parameter</typeparam></member>";
    public const String MemberDocumentationSource = "<member name=\"P:Example.Container.Value\"><summary>Property summary</summary><remarks>Property remarks</remarks><example>Property example</example></member>";
    public const String MethodDocumentationSource = "<member name=\"M:Example.Container.SampleMethod``1(System.String,System.Int32)\"><summary>Method summary</summary><remarks>Method remarks</remarks><example>Method example</example><typeparam name=\"TMethod\">Method type parameter <b>docs</b></typeparam><param name=\"value\">Parameter <i>docs</i></param></member>";
    public const String MethodDocumentationBodySource = "<summary>Method summary</summary><remarks>Method remarks</remarks><example>Method example</example><typeparam name=\"TMethod\">Method type parameter <b>docs</b></typeparam><param name=\"value\">Parameter <i>docs</i></param>";
    public const String DuplicateMethodDocumentationSource = "<member name=\"M:Example.Container.SampleMethod``1(System.String,System.Int32)\"><typeparam>Ignored</typeparam><typeparam name=\"TMethod\">Named type parameter</typeparam><param>Ignored</param><param name=\"value\">Named parameter</param></member>";
    public const String FallbackDocumentationSource = "<member name=\"Example.Custom\"><summary>Fallback summary</summary></member>";
    public const String FieldDocumentationSource = "<member name=\"F:Example.Container.Field\"><summary>Field summary</summary></member>";
    public const String ContextSource = "<doc><members>"
        + TypeDocumentationSource
        + MemberDocumentationSource
        + MethodDocumentationSource
        + FieldDocumentationSource
        + FallbackDocumentationSource
        + "</members></doc>";
    public const String DuplicateContextSource = "<doc><members>"
        + "<member name=\"T:Example.GenericType`1\"><summary>Known</summary></member>"
        + "<member><summary>Ignored</summary></member>"
        + "<ignored name=\"T:Ignored\"><summary>Ignored</summary></ignored>"
        + "</members></doc>";

    public static Type TypeParameterType => typeof(SampleGenericType<>).GetGenericArguments()[0];

    public static MethodInfo MethodInfo => typeof(SampleMemberContainer).GetMethod(nameof(SampleMemberContainer.SampleMethod))!;

    public static Type MethodTypeParameterType => MethodInfo.GetGenericArguments()[0];

    public static ParameterInfo DocumentedParameterInfo => MethodInfo.GetParameters()[0];

    public static ParameterInfo UndocumentedParameterInfo => MethodInfo.GetParameters()[1];

    private sealed class SampleGenericType<TValue>
    {
    }

    private sealed class SampleMemberContainer
    {
        public static void SampleMethod<TMethod>(String value, Int32 count)
        {
        }
    }
}
