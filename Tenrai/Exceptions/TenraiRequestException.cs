using System;

namespace Tenrai.Exceptions;

/// <summary>
/// Exception class thrown when request is not handled properly.
/// </summary>
public class TenraiRequestException : Exception
{
	/// <summary>
	/// Details of error returned from Tenrai Api.
	/// </summary>
	public TenraiApiError ApiError { get; private set; }

	/// <summary>
	/// Parameterless constructor.
	/// </summary>
	public TenraiRequestException()
	{
	}

	/// <summary>
	/// Constructor with exception message.
	/// </summary>
	public TenraiRequestException(string message) : base(message)
	{
	}

	/// <summary>
	/// Constructor with exception message and code.
	/// </summary>
	public TenraiRequestException(string message, TenraiApiError apiError) : base(message)
	{
		ApiError = apiError;
	}

	/// <summary>
	/// Constructor with exception message and inner exception.
	/// </summary>
	public TenraiRequestException(string message, Exception innerException) : base(message, innerException)
	{
	}
}