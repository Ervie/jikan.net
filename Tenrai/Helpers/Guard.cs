using Tenrai.Exceptions;
using System;

namespace Tenrai.Helpers;

internal static class Guard
{
	internal static void IsDefaultEndpoint(string endpoint, string methodName)
	{
		if (endpoint.Equals(DefaultHttpClientProvider.DefaultEndpoint)) throw new NotSupportedException($"Operation {methodName} is not available on the default endpoint.");
	}

	internal static void IsNotNullOrWhiteSpace(string arg, string argumentName)
	{
		if (string.IsNullOrWhiteSpace(arg)) throw new TenraiValidationException("Argument can't be null or whitespace.", argumentName);
	}

	internal static void IsNotNull(object arg, string argumentName)
	{
		if (arg == null) throw new TenraiValidationException("Argument can't be a null.", argumentName);
	}

	internal static void IsLongerThan2Characters(string arg, string argumentName)
	{
		if (string.IsNullOrWhiteSpace(arg) || arg.Length < 3) throw new TenraiValidationException("Argument must be at least 3 characters long", argumentName);
	}

	internal static void IsGreaterThanZero(long arg, string argumentName)
	{
		if (arg < 1) throw new TenraiValidationException("Argument must be a natural number greater than 0.", argumentName);
	}

	internal static void IsLesserOrEqualThan(long arg, long max, string argumentName)
	{
		if (arg > max) throw new TenraiValidationException($"Argument must not be greater than {max}.", argumentName);
	}

	internal static void IsValid<T>(Func<T, bool> isValidFunc, T arg, string argumentName, string message = null)
	{
		if (isValidFunc(arg)) return;

		if (string.IsNullOrWhiteSpace(message))
		{
			message = "Argument is not valid.";
		}

		throw new TenraiValidationException(message, argumentName);
	}

	internal static void IsValidEnum<TEnum>(TEnum arg, string argumentName) where TEnum : struct, Enum
	{
		if (!Enum.IsDefined(typeof(TEnum), arg)) throw new TenraiValidationException("Enum value must be valid", argumentName);
	}

	internal static void IsLetter(char character, string argumentName)
	{
		if (!Char.IsLetter(character)) throw new TenraiValidationException("Character must be a letter", argumentName);
	}
}