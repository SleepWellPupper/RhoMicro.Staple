namespace RhoMicro.Staples.Benchmarks;

using System.Reflection;
using System.Text;

internal static class BenchmarksData
{
    public static readonly String SmallContextSource = BuildContextSource(10);
    public static readonly String LargeContextSource = BuildContextSource(1000);
    public const String MethodDocumentationSource = "<member name=\"M:Example.Container.SampleMethod``1(System.String,System.Int32)\"><summary>Method <b>summary</b></summary><typeparam name=\"TMethod\">Method type parameter <b>docs</b></typeparam><param name=\"value\">Parameter <i>docs</i></param></member>";
    public const String PlainContentSource = "Just plain content";
    public const String MarkupContentSource = "Summary <c>body</c> and <paramref name=\"value\"/>";
    public static readonly MethodInfo MethodInfo = typeof(SampleMemberContainer).GetMethod(nameof(SampleMemberContainer.SampleMethod))!;
    public static readonly Type MethodTypeParameterType = MethodInfo.GetGenericArguments()[0];
    public static readonly ParameterInfo ParameterInfo = MethodInfo.GetParameters()[0];

    private static String BuildContextSource(Int32 memberCount)
    {
        var builder = new StringBuilder();
        builder.Append("<doc><members>");

        for(var i = 0; i < memberCount; i++)
        {
            builder.Append("<member name=\"M:Example.Type.Method");
            builder.Append(i);
            builder.Append("\"><summary>Method ");
            builder.Append(i);
            builder.Append("</summary><param name=\"value\">Value ");
            builder.Append(i);
            builder.Append("</param></member>");
        }

        builder.Append("<member name=\"T:Example.Target\"><summary>Target summary</summary><typeparam name=\"TValue\">Target type parameter</typeparam></member>");
        builder.Append("</members></doc>");

        var result = builder.ToString();
        return result;
    }

    private sealed class SampleMemberContainer
    {
        public static void SampleMethod<TMethod>(String value, Int32 count)
        {
        }
    }
}
