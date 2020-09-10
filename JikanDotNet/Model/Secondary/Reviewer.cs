using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for base reviewer.
	/// </summary>
	public class Reviewer
	{
		/// <summary>
		/// Username.
		/// </summary>
		[JsonPropertyName("username")]
		public string Username { get; set; }

		/// <summary>
		/// Reviewer's image URL.
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Reviewer's URL.
		/// </summary>
		[JsonPropertyName("url")]
		public string URL { get; set; }
	}
}