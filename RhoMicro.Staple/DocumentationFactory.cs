// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple;

using System.Diagnostics.CodeAnalysis;
using CodeAnalysis;
using Content;

[SuppressMessage("Usage", "RMJ0007", Justification = "It is an internal struct and expected to be handled correctly")]
[UnionType<Documentation, Func<Documentation>>]
internal readonly partial struct DocumentationFactory
{
    public Documentation CreateContent() => Switch(
        onDocumentation: static c => c,
        onFunc: static f => f.Invoke());
}
