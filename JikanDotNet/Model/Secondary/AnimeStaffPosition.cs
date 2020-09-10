using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime staff position (person request).
	/// </summary>
	public class AnimeStaffPosition
	{
		/// <summary>
		/// Anime associated with staff position.
		/// </summary>
		[JsonPropertyName("anime")]
		public MALImageSubItem Anime { get; set; }

		/// <summary>
		/// Position associated with staff position.
		/// </summary>
		[JsonPropertyName("position")]
		public string Position { get; set; }
	}
}