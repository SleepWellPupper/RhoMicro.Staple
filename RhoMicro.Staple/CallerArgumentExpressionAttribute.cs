// SPDX-License-Identifier: MPL-2.0

namespace System.Runtime.CompilerServices;

/// <summary>
/// Specifies that a parameter captures the expression passed for another parameter.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
internal sealed class CallerArgumentExpressionAttribute(String parameterName) : Attribute
{
    /// <summary>
    /// Gets the associated parameter name.
    /// </summary>
    public String ParameterName { get; } = parameterName;
}
