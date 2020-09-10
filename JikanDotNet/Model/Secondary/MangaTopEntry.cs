using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for entry on top manga list.
	/// </summary>
	public class MangaTopEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Rank of the manga on selected list.
		/// </summary>
		[JsonPropertyName("rank")]
		public int Rank { get; set; }

		/// <summary>
		/// URL to manga page.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Manga's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Title of the manga.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Manga type (e. g. "TV", "Movie").
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Manga's score on MyAnimeList up to 2 decimal places.
		/// </summary>
		[JsonPropertyName("score")]
		public float? Score { get; set; }

		/// <summary>
		/// Manga members count on MyAnimeList.
		/// </summary>
		[JsonPropertyName("members")]
		public int? Members { get; set; }

		/// <summary>
		/// Date of publishing start.
		/// </summary>
		[JsonPropertyName("start_date")]
		public string PublishingStart { get; set; }

		/// <summary>
		/// Date of publishing end.
		/// </summary>
		[JsonPropertyName("end_date")]
		public string PublishingEnd { get; set; }

		/// <summary>
		/// Manga's volumes count.
		/// </summary>
		[JsonPropertyName("volumes")]
		public int? Volumes { get; set; }
	}
}