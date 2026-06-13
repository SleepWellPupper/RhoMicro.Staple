namespace RhoMicro.Staples;

using System.Runtime.CompilerServices;

internal static class ArgumentNullExceptionExtensions
{
    extension(ArgumentNullException)
    {
        /// <summary>
        /// Validates that an argument is not null and returns it.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <param name="argument">The argument to validate.</param>
        /// <param name="paramName">The parameter name.</param>
        /// <returns>The validated argument.</returns>
        public static T Validate<T>(T? argument, [CallerArgumentExpression(nameof(argument))] String? paramName = null)
            where T : class
        {
            if(argument is null)
            {
                throw new ArgumentNullException(paramName);
            }

            var result = argument;
            return result;
        }

        public static void ThrowIfNull(Object? argument, [CallerArgumentExpression(nameof(argument))] String? paramName = null)
        {
            if(argument is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
