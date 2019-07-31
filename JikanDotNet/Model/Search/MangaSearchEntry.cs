using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// URL to manga's page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string URL { get; set; }

		/// <summary>
		/// Manga's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Title of the manga.
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Manga's description.
		/// </summary>
		[JsonProperty(PropertyName = "synopsis")]
		public string Description { get; set; }

		/// <summary>
		/// Manga type (e. g. "TV", "Movie").
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Manga's score on MyAnimeList up to 2 decimal places.
		/// </summary>
		[JsonProperty(PropertyName = "score")]
		public float? Score { get; set; }

		/// <summary>
		/// Manga's episode count.
		/// </summary>
		[JsonProperty(PropertyName = "volumes")]
		public int? Volumes { get; set; }

		/// <summary>
		/// Manga members count on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "members")]
		public int? Members { get; set; }

		/// <summary>
		/// Is manga currently publishing.
		/// </summary>
		[JsonProperty(PropertyName = "publishing")]
		public bool Publishing { get; set; }

		/// <summary>
		/// Datetime when manga started publishing.
		/// </summary>
		[JsonProperty(PropertyName = "start_date")]
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// Datetime when manga ended publishing.
		/// </summary>
		[JsonProperty(PropertyName = "end_date")]
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// Number of published chapters.
		/// </summary>
		[JsonProperty(PropertyName = "chapters")]
		public int Chapters { get; set; }
	}
}