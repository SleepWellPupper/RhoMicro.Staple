namespace RhoMicro.Staple.Tests;

using System.Reflection;

internal static class DocumentationTestData
{
    public const String DocumentationAccessorSource =
        "<member name=\"X:Example.Item\"><summary>Summary <c>body</c></summary><remarks>Remarks <b>body</b></remarks><example>Example <see cref=\"T:System.String\"/></example></member>";

    public const String DocumentedTypeId = "T:RhoMicro.Staple.Tests.DocumentationTestData.SampleGenericType`1";

    public const String DocumentedMethodId =
        "M:RhoMicro.Staple.Tests.DocumentationTestData.SampleMemberContainer.SampleMethod``1(System.String,System.Int32)";

    public const String DocumentedConstructorId =
        "M:RhoMicro.Staple.Tests.DocumentationTestData.SampleMemberContainer.#ctor";

    public const String DocumentedPropertyId =
        "P:RhoMicro.Staple.Tests.DocumentationTestData.SampleMemberContainer.Value";

    public const String DocumentedFieldId = "F:RhoMicro.Staple.Tests.DocumentationTestData.SampleMemberContainer.Field";

    public const String DocumentedNestedTypeId = "T:RhoMicro.Staple.Tests.DocumentationTestData.OuterType.InnerType";

    public const String DocumentedTypeSource =
        "<member name=\"T:RhoMicro.Staple.Tests.DocumentationTestData.SampleGenericType`1\"><summary>Type summary</summary><remarks>Type remarks</remarks><example>Type example</example><typeparam name=\"TValue\">Type parameter <b>docs</b></typeparam></member>";

    public const String DocumentedMethodSource =
        "<member name=\"M:RhoMicro.Staple.Tests.DocumentationTestData.SampleMemberContainer.SampleMethod``1(System.String,System.Int32)\"><summary>Method summary</summary><remarks>Method remarks</remarks><example>Method example</example><typeparam name=\"TMethod\">Method type parameter <b>docs</b></typeparam><param name=\"value\">Parameter <i>docs</i></param></member>";

    public const String MissingDocumentationAccessorSource = "<member><summary></summary></member>";

    public const String TypeDocumentationSource =
        "<member name=\"T:Example.GenericType`1\"><summary>Type summary</summary><remarks>Type remarks</remarks><example>Type example</example><typeparam name=\"TValue\">Type parameter <b>docs</b></typeparam></member>";

    public const String MemberDocumentationSource =
        "<member name=\"P:Example.Container.Value\"><summary>Property summary</summary><remarks>Property remarks</remarks><example>Property example</example></member>";

    public const String MethodDocumentationSource =
        "<member name=\"M:Example.Container.SampleMethod``1(System.String,System.Int32)\"><summary>Method summary</summary><remarks>Method remarks</remarks><example>Method example</example><typeparam name=\"TMethod\">Method type parameter <b>docs</b></typeparam><param name=\"value\">Parameter <i>docs</i></param></member>";

    public const String FallbackDocumentationSource =
        "<member name=\"Example.Custom\"><summary>Fallback summary</summary></member>";

    public const String FieldDocumentationSource =
        "<member name=\"F:Example.Container.Field\"><summary>Field summary</summary></member>";

    public const String MembersContextSource = "<members>"
                                             + TypeDocumentationSource
                                             + MemberDocumentationSource
                                             + MethodDocumentationSource
                                             + FieldDocumentationSource
                                             + FallbackDocumentationSource
                                             + "</members>";

    public const String ContextSource = "<doc><members>"
                                      + TypeDocumentationSource
                                      + MemberDocumentationSource
                                      + MethodDocumentationSource
                                      + FieldDocumentationSource
                                      + FallbackDocumentationSource
                                      + "</members></doc>";

    public const String DuplicateContextSource = "<members>"
                                               + "<member name=\"T:Example.GenericType`1\"><summary>Known</summary></member>"
                                               + "<member><summary>Ignored</summary></member>"
                                               + "<ignored name=\"T:Ignored\"><summary>Ignored</summary></ignored>"
                                               + "</members>";

    public static Type DocumentedType => typeof(SampleGenericType<>);

    public static MethodInfo MethodInfo =>
        typeof(SampleMemberContainer).GetMethod(nameof(SampleMemberContainer.SampleMethod))!;

    public static MethodInfo DocumentedMethodInfo => MethodInfo;

    public static ConstructorInfo DocumentedConstructorInfo => typeof(SampleMemberContainer).GetConstructor(
        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, binder: null, Type.EmptyTypes,
        modifiers: [])!;

    public static PropertyInfo DocumentedPropertyInfo => typeof(SampleMemberContainer).GetProperty(
        nameof(SampleMemberContainer.Value), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!;

    public static FieldInfo DocumentedFieldInfo => typeof(SampleMemberContainer).GetField(
        nameof(SampleMemberContainer.Field), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)!;

    public static EventInfo DocumentedEventInfo => typeof(SampleMemberContainer).GetEvent(
        nameof(SampleMemberContainer.Changed), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!;

    public static Type DocumentedNestedType => typeof(OuterType.InnerType);

    private sealed class SampleGenericType<TValue>
    {
    }

    private sealed class SampleMemberContainer
    {
        public SampleMemberContainer()
        {
        }

        public static void SampleMethod<TMethod>(String value, Int32 count)
        {
        }

        public Int32 Value { get; set; }

        public static Int32 Field = 1;

        public event EventHandler? Changed;
    }

    internal sealed class OuterType
    {
        internal sealed class InnerType
        {
        }
    }
}
