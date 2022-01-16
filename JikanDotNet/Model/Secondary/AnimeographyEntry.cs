using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model for animeography entry
	/// </summary>
	public class AnimeographyEntry
	{
		/// <summary>
		/// Character's animeography entry.
		/// </summary>
		[JsonPropertyName("anime")]
		public MalImageSubItem Anime { get; set; }

		/// <summary>
		/// Role of character in sub item (anime or manga). Not available in all requests.
		/// </summary>
		[JsonPropertyName("role")]
		public string Role { get; set; }
	}
}