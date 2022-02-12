using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model representing genre.
	/// </summary>
	public class Genre
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Url to sub item main page.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Name of the genre
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Total count of anime/manga assigned to this genre
		/// </summary>
		[JsonPropertyName("count")]
		public int TotalCount { get; set; }
	}
}