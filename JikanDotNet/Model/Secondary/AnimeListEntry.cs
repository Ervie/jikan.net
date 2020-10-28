using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Entry on user's anime list model class.
	/// </summary>
	public class AnimeListEntry
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
		public AnimeTitle Title { get; set; }

		/// <summary>
		/// Anime's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Anime's URL
		/// </summary>
		[JsonPropertyName("url")]
		public string URL { get; set; }

		/// <summary>
		/// Anime's video URL.
		/// </summary>
		[JsonPropertyName("video_url")]
		public string VideoUrl { get; set; }

		/// <summary>
		/// Anime type (e. g. "TV", "Movie").
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Anime's episodes count watched by the user.
		/// </summary>
		[JsonPropertyName("watched_episodes")]
		public int? WatchedEpisodes { get; set; }

		/// <summary>
		/// Anime's episodes total count. 0 if not finished.
		/// </summary>
		[JsonPropertyName("total_episodes")]
		public int? TotalEpisodes { get; set; }

		/// <summary>
		/// User's score for the anime. 0 if not assigned yet.
		/// </summary>
		[JsonPropertyName("score")]
		public int Score { get; set; }

		/// <summary>
		/// Does anime have episode video.
		/// </summary>
		[JsonPropertyName("has_episode_video")]
		public bool? HasEpisodeVideo { get; set; }

		/// <summary>
		/// Does anime have promo video.
		/// </summary>
		[JsonPropertyName("has_promo_video")]
		public bool? HasPromoVideo { get; set; }

		/// <summary>
		/// Does anime have video.
		/// </summary>
		[JsonPropertyName("has_video")]
		public bool? HasVideo { get; set; }

		/// <summary>
		/// Does user rewatch anime.
		/// </summary>
		[JsonPropertyName("is_rewatching")]
		public bool? IsRewatching { get; set; }

		/// <summary>
		/// Anime's age rating.
		/// </summary>
		[JsonPropertyName("rating")]
		public string Rating { get; set; }

		/// <summary>
		/// Start date of anime airing.
		/// </summary>
		[JsonPropertyName("start_date")]
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// End date of anime airing.
		/// </summary>
		[JsonPropertyName("end_date")]
		public DateTime? EndDate { get; set; }

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
		/// Priority of anime on user's list.
		/// </summary>
		[JsonPropertyName("priority")]
		public string Priority { get; set; }

		/// <summary>
		/// Current airing status of anime.
		/// </summary>
		[JsonPropertyName("airing_status")]
		public AiringStatus AiringStatus { get; set; }

		/// <summary>
		/// Current user's watching status of anime.
		/// </summary>
		[JsonPropertyName("watching_status")]
		public UserAnimeListExtension WatchingStatus { get; set; }
	}
}