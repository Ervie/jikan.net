using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model representing basic user metadata
	/// </summary>
	public class UserMetadata
	{
		/// <summary>
		/// Username.
		/// </summary>
		[JsonPropertyName("username")]
		public string Username { get; set; }

		/// <summary>
		/// User's image URL
		/// </summary>
		[JsonPropertyName("images")]
		public ImagesSet Images { get; set; }

		/// <summary>
		/// User's URL
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }
	}
}