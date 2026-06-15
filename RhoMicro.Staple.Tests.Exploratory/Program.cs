// SPDX-License-Identifier: MPL-2.0

// See https://aka.ms/new-console-template for more information

using RhoMicro.Staple;

Console.WriteLine(typeof(Foo).Documentation?.Root.OuterXml);

/// <summary>
/// This is the summary.
/// </summary>
/// <example>
/// here is an example
/// </example>
sealed class Foo;
