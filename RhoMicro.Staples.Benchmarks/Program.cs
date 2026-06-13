using BenchmarkDotNet.Running;
using RhoMicro.BdnLogging;

BenchmarkRunner.Run<RhoMicro.Staples.Benchmarks.LibraryBenchmarks>(SpotlightConfig.Instance, args);
