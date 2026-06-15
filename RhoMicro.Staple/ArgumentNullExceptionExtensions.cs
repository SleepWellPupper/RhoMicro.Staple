// SPDX-License-Identifier: MPL-2.0

namespace RhoMicro.Staple;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

internal static class ArgumentNullExceptionExtensions
{
    extension(ArgumentNullException)
    {
        public static T ThrowIfNull<T>(
            [NotNull] T? argument,
            [CallerArgumentExpression(nameof(argument))]
            String? paramName = null)
            => argument ?? throw new ArgumentNullException(paramName);
    }
}
