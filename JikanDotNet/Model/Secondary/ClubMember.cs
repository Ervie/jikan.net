using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Club member model class.
	/// </summary>
	public class ClubMember
	{
		/// <summary>
		/// Club member's Username.
		/// </summary>
		[JsonPropertyName("username")]
		public string Username { get; set; }

		/// <summary>
		/// Club member's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Club member's URL
		/// </summary>
		[JsonPropertyName("url")]
		public string URL { get; set; }
	}
}