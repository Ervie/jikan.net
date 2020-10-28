using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Anime title model class.
	/// </summary>
	public class AnimeTitle
	{
		/// <summary>
		/// Title of the episode.
		/// </summary>
		[JsonPropertyName("english")]
		public string English { get; set; }

		/// <summary>
		/// Title of the anime in Japanese.
		/// </summary>
		[JsonPropertyName("japanese")]
		public string Japanese { get; set; }

		/// <summary>
		/// Title of the anime in romaji.
		/// </summary>
		[JsonPropertyName("romaji")]
		public string Romaji { get; set; }
	}
}