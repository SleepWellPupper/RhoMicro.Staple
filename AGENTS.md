# AGENTS

## Repo Shape
- The solution file is intentionally still named `RhoMicro.Staple.slnx`; do not rename it when renaming projects.
- Projects in the solution:
  - `RhoMicro.Staples/`: core library for XML documentation accessors, targets `netstandard2.0`
  - `RhoMicro.Staples.Analyzers/`: Roslyn analyzer/generator package, targets `netstandard2.0`, packed as `RhoMicro.Staples.Analyzers`
  - `RhoMicro.Staples.Analyzers.Tests/`: xUnit v3 test project for the analyzer project
  - `RhoMicro.Staples.Tests/`: xUnit v3 test project for the `RhoMicro.Staples` library
  - `RhoMicro.Staples.Benchmarks/`: BenchmarkDotNet harness for `RhoMicro.Staples`

## Toolchain And Commands
- Use .NET SDK `10.0.0` from `global.json`.
- CI’s command order is `dotnet restore` -> `dotnet build --no-restore` -> `dotnet test --no-build`.
- The most reliable full verification command during edits is `dotnet build "RhoMicro.Staple.slnx"`.
- For tests, use `dotnet test "RhoMicro.Staple.slnx"`, `dotnet test "RhoMicro.Staples.Tests/RhoMicro.Staples.Tests.csproj"`, or `dotnet test "RhoMicro.Staples.Analyzers.Tests/RhoMicro.Staples.Analyzers.Tests.csproj"`.
- For benchmarks, use `dotnet run -c Release --project "RhoMicro.Staples.Benchmarks/RhoMicro.Staples.Benchmarks.csproj"`.

## Build Rules That Bite
- `Directory.Build.props` enables nullable, .NET analyzers, preview language features, and treats warnings as errors for the whole repo.
- Public API XML docs are enforced via `CS1591` outside test projects. If you add or change public/protected API, add XML documentation comments or the build will fail.
- Test projects are detected by project name ending with `.Tests`; they get relaxed warning settings automatically.
- Non-test projects automatically pick up packaging/source-link metadata from `Directory.Build.props`.
- `RhoMicro.Staples.Benchmarks/Directory.Build.props` imports the root props and locally relaxes warnings that would otherwise break BenchmarkDotNet-generated code; keep benchmark-specific build relaxations there, not in repo-wide props.

## Code Style Conventions Already In Use
- Follow `.editorconfig`: file-scoped namespaces, BCL type names (`String`, `Boolean`, `Object`), and expression-bodied members are preferred.
- In block-bodied methods, return through a local variable named `result` before the final `return`; this is an established debugging convention in `RhoMicro.Staples`.
- Null guards in `RhoMicro.Staples` use `ArgumentNullException.ThrowIfNull(...)`, but that is implemented locally via `ArgumentNullExceptionExtensions.cs` because the project targets `netstandard2.0`.
- The repo uses preview C# features intentionally, including the new extension syntax in `ArgumentNullExceptionExtensions.cs` and the `field` keyword in properties.

## Packaging And Dependencies
- Package versions are centrally managed in `Directory.Packages.props`; do not add `Version=` attributes inside individual `PackageReference`s unless you also intend to change central management.
- `RhoMicro.Staples.Analyzers` packs `../README.md` into the NuGet package; keep package-facing documentation in the root `README.md`.
- Benchmarks rely on both `BenchmarkDotNet` and `RhoMicro.BdnLogging`; the benchmark entrypoint uses `SpotlightConfig.Instance` from `RhoMicro.BdnLogging`.

## CI / Publish Notes
- CI workflows live in `.github/workflows/`.
- `test.yml` runs restore/build/test on every push.
- `publish.yml` builds with `--property:Version=${{ github.ref_name }}` and pushes any generated `.nupkg` files to NuGet.
