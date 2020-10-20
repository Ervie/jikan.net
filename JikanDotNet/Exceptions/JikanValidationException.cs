using System;

namespace JikanDotNet.Exceptions
{
	/// <summary>
	/// Exception class thrown when input parameters are invalid.
	/// </summary>
	public class JikanValidationException : Exception
	{
		/// <summary>
		/// Name of the argument that failed validation.
		/// </summary>
		public string ArgumentName { get; }

		/// <summary>
		/// Constructor with exception message and name of the argument that  failed validation.
		/// </summary>
		public JikanValidationException(string message, string argumentName) : base(message)
		{
			ArgumentName = argumentName;
		}
	}
}