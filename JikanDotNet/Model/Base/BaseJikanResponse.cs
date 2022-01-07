using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Base wrapping class for response with data
	/// </summary>
	public class BaseJikanResponse<TResponse>
	{
		/// <summary>
		/// Hash of the request.
		/// </summary>
		[JsonPropertyName("data")]
		public TResponse Data { get; set; }

		/// <summary>
		/// Parametereless constructor, required for serialization
		/// </summary>
		public BaseJikanResponse() {}
	}
}