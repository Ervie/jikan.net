using JikanDotNet.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JikanDotNet.Helpers
{
    internal static class Guard
    {
        internal static void IsNotNullOrWhiteSpace(string arg, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                throw new JikanValidationException("Can't be null or whitespace.", argumentName);
            }
        }

        internal static void IsNotEmpty(Guid arg, string argumentName)
        {
            if (Guid.Empty.Equals(arg))
            {
                throw new JikanValidationException("GUID cannot be empty.", argumentName);
            }
        }

        internal static void IsNaturalNumber(int arg, string argumentName)
        {
            if (arg < 0)
            {
                throw new JikanValidationException("Argument must be a natural number.", argumentName);
            }
        }

        internal static void IsValid<T>(Func<T, bool> isValidFunc, T arg, string argumentName, string? message = null)
        {
            if (isValidFunc(arg))
            {
                return;
            }

            if (String.IsNullOrWhiteSpace(message))
            {
                message = "Argument is not valid.";
            }

            throw new JikanValidationException(message, argumentName);
        }
    }
}
