namespace RhoMicro.Staples.Benchmarks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Content;

[MemoryDiagnoser]
[ShortRunJob]
public class LibraryBenchmarks
{
    private DocumentationContext _smallContext = null!;
    private DocumentationContext _largeContext = null!;
    private MethodDocumentationAccessor _methodDocumentation = null!;
    private DocumentationContent _plainContent = null!;
    private DocumentationContent _markupContent = null!;

    [GlobalSetup]
    public void Setup()
    {
        _smallContext = new DocumentationContext(BenchmarksData.SmallContextSource);
        _largeContext = new DocumentationContext(BenchmarksData.LargeContextSource);
        _methodDocumentation = new MethodDocumentationAccessor(BenchmarksData.MethodDocumentationSource);
        _plainContent = new DocumentationContent(BenchmarksData.PlainContentSource);
        _markupContent = new DocumentationContent(BenchmarksData.MarkupContentSource);
    }

    [Benchmark]
    public DocumentationAccessor? CreateSmallContextAndLookupAccessor()
    {
        var context = new DocumentationContext(BenchmarksData.SmallContextSource);
        var result = context.GetContent("T:Example.Target");
        return result;
    }

    [Benchmark]
    public DocumentationAccessor? CreateLargeContextAndLookupAccessor()
    {
        var context = new DocumentationContext(BenchmarksData.LargeContextSource);
        var result = context.GetContent("T:Example.Target");
        return result;
    }

    [Benchmark]
    public DocumentationAccessor? LookupAccessorFromCachedLargeContext()
    {
        var result = _largeContext.GetContent("T:Example.Target");
        return result;
    }

    [Benchmark]
    public Int32 GetMethodParameterDocumentationByNameNodeCount()
    {
        var result = _methodDocumentation.GetParameter("value")?.Children.Count ?? 0;
        return result;
    }

    [Benchmark]
    public Int32 GetMethodParameterDocumentationByParameterInfoNodeCount()
    {
        var result = _methodDocumentation.GetParameter(BenchmarksData.ParameterInfo.Name ?? String.Empty)?.Children.Count ?? 0;
        return result;
    }

    [Benchmark]
    public Int32 GetMethodTypeParameterDocumentationByTypeNodeCount()
    {
        var result = _methodDocumentation.GetTypeParameter(BenchmarksData.MethodTypeParameterType.Name)?.Children.Count ?? 0;
        return result;
    }

    [Benchmark]
    public Int32 GetPlainContentNodeCount()
    {
        var result = _plainContent.Nodes.Count;
        return result;
    }

    [Benchmark]
    public Int32 GetMarkupContentNodeCount()
    {
        var result = _markupContent.Nodes.Count;
        return result;
    }
}
