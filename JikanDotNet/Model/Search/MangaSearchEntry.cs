using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for single result from searching manga.
	/// </summary>
	public class MangaSearchEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// URL to manga's page.
		/// </summary>
		[JsonPropertyName("url")]
		public string URL { get; set; }

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
		/// Manga's description.
		/// </summary>
		[JsonPropertyName("synopsis")]
		public string Description { get; set; }

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
		/// Manga's episode count.
		/// </summary>
		[JsonPropertyName("volumes")]
		public int? Volumes { get; set; }

		/// <summary>
		/// Manga members count on MyAnimeList.
		/// </summary>
		[JsonPropertyName("members")]
		public int? Members { get; set; }

		/// <summary>
		/// Is manga currently publishing.
		/// </summary>
		[JsonPropertyName("publishing")]
		public bool Publishing { get; set; }

		/// <summary>
		/// Datetime when manga started publishing.
		/// </summary>
		[JsonPropertyName("start_date")]
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// Datetime when manga ended publishing.
		/// </summary>
		[JsonPropertyName("end_date")]
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// Number of published chapters.
		/// </summary>
		[JsonPropertyName("chapters")]
		public int? Chapters { get; set; }
	}
}