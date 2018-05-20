using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for single season entry.
	/// </summary>
	public class SeasonEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Title of the anime.
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Anime's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Anime type (e. g. "TV", "Movie").
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Anime's URL
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string URL { get; set; }

		/// <summary>
		/// Anime's episode count.
		/// </summary>
		[JsonProperty(PropertyName = "episodes")]
		public string Episodes { get; set; }

		/// <summary>
		/// Anime's synopsis.
		/// </summary>
		[JsonProperty(PropertyName = "synopsis")]
		public string Synopsis { get; set; }

		/// <summary>
		/// Anime's producers numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "producer")]
		public ICollection<Producer> Producers { get; set; }

		/// <summary>
		/// Anime's licensors as strings.
		/// </summary>
		[JsonProperty(PropertyName = "licensor")]
		public ICollection<string> Licensors { get; set; }

		/// <summary>
		/// Anime source (e .g. "Manga" or "Original").
		/// </summary>
		[JsonProperty(PropertyName = "source")]
		public string Source { get; set; }

		/// <summary>
		/// Anime's genres numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "genre")]
		public ICollection<Genre> Genres { get; set; }

		/// <summary>
		/// Date when anime started/will start airing.
		/// </summary>
		[JsonProperty(PropertyName = "airing_start")]
		public string AiringStart { get; set; }

		/// <summary>
		/// Anime's score on MyAnimeList up to 2 decimal places.
		/// </summary>
		[JsonProperty(PropertyName = "score")]
		public float? Score { get; set; }

		/// <summary>
		/// Anime members count on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "members")]
		public int? Members { get; set; }

		/// <summary>
		/// Is anime suitable for kids.
		/// </summary>
		[JsonProperty(PropertyName = "kids")]
		public bool? Kids { get; set; }

		/// <summary>
		/// Is anime marked as 18+.
		/// </summary>
		[JsonProperty(PropertyName = "r18_plus")]
		public bool? R18 { get; set; }

		/// <summary>
		/// Is anime continuing from last season.
		/// </summary>
		[JsonProperty(PropertyName = "continued")]
		public bool? Continued { get; set; }
	}
}