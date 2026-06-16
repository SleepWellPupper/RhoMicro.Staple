// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

internal static class ArgumentNullExceptionExtensions
{
    extension(ArgumentNullException)
    {
        public static T Validate<T>(
            [NotNull] T? argument,
            [CallerArgumentExpression(nameof(argument))]
            String? paramName = null)
            => argument ?? throw new ArgumentNullException(paramName);

#if NETSTANDARD2_0
        public static void ThrowIfNull(
            [NotNull] Object? argument,
            [CallerArgumentExpression(nameof(argument))]
            String? paramName = null)
            => _ = argument ?? throw new ArgumentNullException(paramName);
#endif
    }
}
