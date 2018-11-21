using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// URL to anime's page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string URL { get; set; }

		/// <summary>
		/// Anime's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Title of the anime.
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Anime's description.
		/// </summary>
		[JsonProperty(PropertyName = "synopsis")]
		public string Description { get; set; }

		/// <summary>
		/// Anime type (e. g. "TV", "Movie").
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Anime's score on MyAnimeList up to 2 decimal places.
		/// </summary>
		[JsonProperty(PropertyName = "score")]
		public float? Score { get; set; }

		/// <summary>
		/// Anime's episode count.
		/// </summary>
		[JsonProperty(PropertyName = "episodes")]
		public int? Episodes { get; set; }

		/// <summary>
		/// Anime members count on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "members")]
		public int? Members { get; set; }

		/// <summary>
		/// Is anime currently airing.
		/// </summary>
		[JsonProperty(PropertyName = "airing")]
		public bool Airing { get; set; }

		/// <summary>
		/// Datetime when anime started airing.
		/// </summary>
		[JsonProperty(PropertyName = "start_date")]
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// Datetime when anime ended airing.
		/// </summary>
		[JsonProperty(PropertyName = "end_date")]
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// Anime rating (e. g. "PG-13", "R").
		/// </summary>
		[JsonProperty(PropertyName = "rated")]
		public string Rated { get; set; }
	}
}