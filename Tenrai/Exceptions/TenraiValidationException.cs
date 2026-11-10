using System;

namespace Tenrai.Exceptions;

/// <summary>
/// Exception class thrown when input parameters are invalid.
/// </summary>
public class TenraiValidationException : Exception
{
	/// <summary>
	/// Name of the argument that failed validation.
	/// </summary>
	public string ArgumentName { get; }

	/// <summary>
	/// Constructor with exception message and name of the argument that  failed validation.
	/// </summary>
	public TenraiValidationException(string message, string argumentName) : base(message)
	{
		ArgumentName = argumentName;
	}
}