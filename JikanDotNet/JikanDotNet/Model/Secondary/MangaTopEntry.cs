using Newtonsoft.Json;

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
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Rank of the manga on selected list.
		/// </summary>
		[JsonProperty(PropertyName = "rank")]
		public int Rank { get; set; }

		/// <summary>
		/// URL to manga page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

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
		/// Manga members count on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "members")]
		public int? Members { get; set; }

		/// <summary>
		/// Date of publishing start.
		/// </summary>
		[JsonProperty(PropertyName = "publishing_start")]
		public string PublishingStart { get; set; }

		/// <summary>
		/// Date of publishing end.
		/// </summary>
		[JsonProperty(PropertyName = "publishing_end")]
		public string PublishingEnd { get; set; }

		/// <summary>
		/// Manga's volumes count.
		/// </summary>
		[JsonProperty(PropertyName = "volumes")]
		public int Volumes { get; set; }
	}
}