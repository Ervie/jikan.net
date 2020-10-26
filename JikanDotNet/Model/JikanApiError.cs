using System.Net;
using System.Text.Json.Serialization;

namespace JikanDotNet.Model
{
	/// <summary>
	/// Class of error data returned in case http call was unsucessfull
	/// </summary>
	public class JikanApiError
	{
		/// <summary>
		/// Response code received from HttpResponseMessage.
		/// </summary>
		[JsonPropertyName("status")]
		public HttpStatusCode Status { get; private set; }

		/// <summary>
		/// Type of http error.
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; private set; }

		/// <summary>
		/// Message of the error.
		/// </summary>
		[JsonPropertyName("message")]
		public string Message { get; private set; }

		/// <summary>
		/// Additional data.
		/// </summary>
		[JsonPropertyName("error")]
		public string Error { get; private set; }
	}
}