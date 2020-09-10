using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Entry on manga model class (used in genres, schedules, seasons and producers).
	/// </summary>
	public class AnimeSubEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Title of the anime.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Anime's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Anime type (e. g. "TV", "Movie").
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Anime's URL
		/// </summary>
		[JsonPropertyName("url")]
		public string URL { get; set; }

		/// <summary>
		/// Anime's episode count.
		/// </summary>
		[JsonPropertyName("episodes")]
		public int? Episodes { get; set; }

		/// <summary>
		/// Anime's synopsis.
		/// </summary>
		[JsonPropertyName("synopsis")]
		public string Synopsis { get; set; }

		/// <summary>
		/// Anime's producers numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("producers")]
		public ICollection<MALSubItem> Producers { get; set; }

		/// <summary>
		/// Anime's licensors as strings.
		/// </summary>
		[JsonPropertyName("licensors")]
		public ICollection<string> Licensors { get; set; }

		/// <summary>
		/// Anime source (e .g. "Manga" or "Original").
		/// </summary>
		[JsonPropertyName("source")]
		public string Source { get; set; }

		/// <summary>
		/// Anime's genres numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("genres")]
		public ICollection<MALSubItem> Genres { get; set; }

		/// <summary>
		/// Date when anime started/will start airing.
		/// </summary>
		[JsonPropertyName("airing_start")]
		public DateTime? AiringStart { get; set; }

		/// <summary>
		/// Anime's score on MyAnimeList up to 2 decimal places.
		/// </summary>
		[JsonPropertyName("score")]
		public float? Score { get; set; }

		/// <summary>
		/// Anime members count on MyAnimeList.
		/// </summary>
		[JsonPropertyName("members")]
		public int? Members { get; set; }

		/// <summary>
		/// Is anime suitable for kids.
		/// </summary>
		[JsonPropertyName("kids")]
		public bool? Kids { get; set; }

		/// <summary>
		/// Is anime marked as 18+.
		/// </summary>
		[JsonPropertyName("r18")]
		public bool? R18 { get; set; }

		/// <summary>
		/// Is anime continuing from last season.
		/// </summary>
		[JsonPropertyName("continuing")]
		public bool? Continued { get; set; }
	}
}