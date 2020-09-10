using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for single result from searching anime.
	/// </summary>
	public class AnimeSearchEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// URL to anime's page.
		/// </summary>
		[JsonPropertyName("url")]
		public string URL { get; set; }

		/// <summary>
		/// Anime's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Title of the anime.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Anime's description.
		/// </summary>
		[JsonPropertyName("synopsis")]
		public string Description { get; set; }

		/// <summary>
		/// Anime type (e. g. "TV", "Movie").
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Anime's score on MyAnimeList up to 2 decimal places.
		/// </summary>
		[JsonPropertyName("score")]
		public float? Score { get; set; }

		/// <summary>
		/// Anime's episode count.
		/// </summary>
		[JsonPropertyName("episodes")]
		public int? Episodes { get; set; }

		/// <summary>
		/// Anime members count on MyAnimeList.
		/// </summary>
		[JsonPropertyName("members")]
		public int? Members { get; set; }

		/// <summary>
		/// Is anime currently airing.
		/// </summary>
		[JsonPropertyName("airing")]
		public bool Airing { get; set; }

		/// <summary>
		/// Datetime when anime started airing.
		/// </summary>
		[JsonPropertyName("start_date")]
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// Datetime when anime ended airing.
		/// </summary>
		[JsonPropertyName("end_date")]
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// Anime rating (e. g. "PG-13", "R").
		/// </summary>
		[JsonPropertyName("rated")]
		public string Rated { get; set; }
	}
}