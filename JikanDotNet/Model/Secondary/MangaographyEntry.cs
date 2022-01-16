using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model for character mangaography
	/// </summary>
	public class MangaographyEntry
	{
		/// <summary>
		/// Character's animeography entry.
		/// </summary>
		[JsonPropertyName("manga")]
		public MalImageSubItem Manga { get; set; }

		/// <summary>
		/// Role of character in sub item (anime or manga). Not available in all requests.
		/// </summary>
		[JsonPropertyName("role")]
		public string Role { get; set; }
	}
}