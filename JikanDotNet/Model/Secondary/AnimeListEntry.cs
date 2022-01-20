using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Entry on user's anime list model class.
	/// </summary>
	public class AnimeListEntry
	{
		/// <summary>
		/// Current user's watching status of anime.
		/// </summary>
		[JsonPropertyName("watching_status")]
		public UserAnimeListExtension WatchingStatus { get; set; }

		/// <summary>
		/// User's score for the anime. 0 if not assigned yet.
		/// </summary>
		[JsonPropertyName("score")]
		public int Score { get; set; }

		/// <summary>
		/// Anime's episodes count watched by the user.
		/// </summary>
		[JsonPropertyName("episodes_watched")]
		public int? EpisodesWatched { get; set; }

		/// <summary>
		/// Tags added by user.
		/// </summary>
		[JsonPropertyName("tags")]
		public string Tags { get; set; }

		/// <summary>
		/// Does user rewatch anime.
		/// </summary>
		[JsonPropertyName("is_rewatching")]
		public bool? IsRewatching { get; set; }

		/// <summary>
		/// Start date of user watching.
		/// </summary>
		[JsonPropertyName("watch_start_date")]
		public DateTime? WatchStartDate { get; set; }

		/// <summary>
		/// End date of user watching.
		/// </summary>
		[JsonPropertyName("watch_end_date")]
		public DateTime? WatchEndDate { get; set; }

		/// <summary>
		/// Time user has been watching anime.
		/// </summary>
		[JsonPropertyName("days")]
		public int? Days { get; set; }

		/// <summary>
		/// Type of storage.
		/// </summary>
		[JsonPropertyName("storage")]
		public string Storage { get; set; }

		/// <summary>
		/// Priority of anime on user's list.
		/// </summary>
		[JsonPropertyName("priority")]
		public string Priority { get; set; }

		/// <summary>
		/// Anime details.
		/// </summary>
		[JsonPropertyName("anime")]
		public AnimeListEntryDetails Anime { get; set; }
	}

	/// <summary>
	/// Anime details on the user list.
	/// </summary>
	public class AnimeListEntryDetails
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long? MalId { get; set; }

		/// <summary>
		/// Anime's canonical link.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Anime's images in various formats.
		/// </summary>
		[JsonPropertyName("images")]
		public ImagesSet Images { get; set; }

		/// <summary>
		/// Title of the anime.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Anime type (e. g. "TV", "Movie").
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Anime's episode count.
		/// </summary>
		[JsonPropertyName("episodes")]
		public int? Episodes { get; set; }

		/// <summary>
		/// Anime's airing status (e. g. "Currently Airing").
		/// </summary>
		[JsonPropertyName("status")]
		public string Status { get; set; }

		/// <summary>
		/// Is anime currently airing.
		/// </summary>
		[JsonPropertyName("airing")]
		public bool Airing { get; set; }

		/// <summary>
		/// Assiociative keys "from" and "to" which are alternative version of AiredString in ISO8601 format.
		/// </summary>
		[JsonPropertyName("aired")]
		public TimePeriod Aired { get; set; }

		/// <summary>
		/// Anime's age rating.
		/// </summary>
		[JsonPropertyName("rating")]
		public string Rating { get; set; }

		/// <summary>
		/// Season of the year the anime premiered.
		/// </summary>
		[JsonPropertyName("season")]
		public string Season { get; set; }

		/// <summary>
		/// Year the anime premiered.
		/// </summary>
		[JsonPropertyName("year")]
		public int? Year { get; set; }

		/// <summary>
		/// Anime's licensors numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("licensors")]
		public ICollection<MalUrl> Licensors { get; set; }

		/// <summary>
		/// Anime's studio(s) numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("studios")]
		public ICollection<MalUrl> Studios { get; set; }

		/// <summary>
		/// Anime's genres numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("genres")]
		public ICollection<MalUrl> Genres { get; set; }

		/// <summary>
		/// Anime's demographics
		/// </summary>
		[JsonPropertyName("demographics")]
		public ICollection<MalUrl> Demographics { get; set; }
	}
}