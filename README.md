# RhoMicro.Staple

This is a library for accessing documentation comments at runtime.

## Licensing

This work is licensed to you under the [MPL-2.0](https://spdx.org/licenses/MPL-2.0.html) license.

## Features

- Parses XML documentation into typed content objects.
- Reads documentation from XML sources, parsed `Documentation` objects, or assemblies, loading the associated XML documentation file automatically when present.
- Exposes `Documentation` lookup as extension members on `Assembly`, `Type`, and `MemberInfo`.
- Preserves common XML documentation elements such as `summary`, `remarks`, `example`, `typeparam`, `param`, and inline formatting nodes.
- Caches discovered documentation for repeated lookups.

## Installation

Library:
```bash
dotnet add package RhoMicro.Staple
```

Generator:
```bash
dotnet add package RhoMicro.Staple.Analyzers
```

## How To Use

Annotate your types with docs comments:

```cs
namespace Foo
{
    /// <summary>This is the <c>Bar</c> class.</summary>
    public static class Bar
    {
        /// <summary>This is the <see cref = "Baz"/> method.</summary>
        /// <returns>This method always returns <c>0</c> (<see langword="default"/>).</returns>
        public static int Baz() => 0;
    }
}
```

[//]:(rmmdt://RhoMicro.Staple.Tests/ReadmeTests.cs@Foo)

Retrieve them using reflection:

```cs
var barSummary = typeof(Bar).Documentation?.Summary?.InnerXml;
var baz = typeof(Bar).GetMethod("Baz");
var bazSummary = baz?.Documentation?.Summary?.InnerXml;
var bazReturns = baz?.Documentation?.Returns?.InnerXml;
Assert.Equal("This is the <c>Bar</c> class.", barSummary);
Assert.Equal("This is the <see cref=\"M:Foo.Bar.Baz\"/> method.", bazSummary);
Assert.Equal("This method always returns <c>0</c> (<see langword=\"default\"/>).", bazReturns);
```

[//]:(rmmdt://RhoMicro.Staple.Tests/ReadmeTests.cs@GeneratedAccess.4.*)

### Create Documentation Manually

Create a `DocumentationContext` instance. Assemblies passed to `DocumentationContext.Create(...)` automatically load their associated XML documentation file when one is available:

```cs
var context = DocumentationContext.Create("""
                <members>
                <member name="T:Foo.Bar">
                <summary>This is an artificial summary.</summary>
                </member>
                </members>
                """);
var barSummary = context.GetContent(typeof(Bar).DocumentationId)?.Summary?.InnerXml;
Assert.Equal("This is an artificial summary.", barSummary);
```

[//]:(rmmdt://RhoMicro.Staple.Tests/ReadmeTests.cs@ManualAccess.4.*)

### Create Artificial Documentation

You can add the `XmlDocumentationAttribute` to your assembly manually and access those docs through the same API. The source
generator can automate that when you do not want to emit a docs XML file.

The analyzer package provides a generator for attaching documentation in metadata via assembly attributes as opposed to
an XML file (`GenerateDocumentationFile`).
