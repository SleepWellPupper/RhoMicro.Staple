using Microsoft.CodeAnalysis;

namespace RhoMicro.Staples.Analyzers;

/// <summary>
/// Minimal incremental generator boilerplate.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed class Generator : IIncrementalGenerator
{
    /// <summary>
    /// Initializes the generator pipeline.
    /// </summary>
    /// <param name="context">The generator initialization context.</param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
    }
}
