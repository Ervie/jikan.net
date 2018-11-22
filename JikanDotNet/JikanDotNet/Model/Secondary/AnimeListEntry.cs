using Newtonsoft.Json;
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
		/// Anime's URL
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string URL { get; set; }

		/// <summary>
		/// Anime's video URL.
		/// </summary>
		[JsonProperty(PropertyName = "video_url")]
		public string VideoUrl { get; set; }

		/// <summary>
		/// Anime type (e. g. "TV", "Movie").
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Anime's episodes count watched by the user.
		/// </summary>
		[JsonProperty(PropertyName = "watched_episodes")]
		public int? WatchedEpisodes { get; set; }

		/// <summary>
		/// Anime's episodes total count. 0 if not finished.
		/// </summary>
		[JsonProperty(PropertyName = "total_episodes")]
		public int? TotalEpisodes { get; set; }

		/// <summary>
		/// User's score for the anime. 0 if not assigned yet.
		/// </summary>
		[JsonProperty(PropertyName = "score")]
		public int Score { get; set; }

		/// <summary>
		/// Does anime have episode video.
		/// </summary>
		[JsonProperty(PropertyName = "has_episode_video")]
		public bool? HasEpisodeVideo { get; set; }

		/// <summary>
		/// Does anime have promo video.
		/// </summary>
		[JsonProperty(PropertyName = "has_promo_video")]
		public bool? HasPromoVideo { get; set; }

		/// <summary>
		/// Does anime have video.
		/// </summary>
		[JsonProperty(PropertyName = "has_video")]
		public bool? HasVideo { get; set; }

		/// <summary>
		/// Does user rewatch anime.
		/// </summary>
		[JsonProperty(PropertyName = "is_rewatching")]
		public bool? IsRewatching { get; set; }

		/// <summary>
		/// Anime's age rating.
		/// </summary>
		[JsonProperty(PropertyName = "rating")]
		public string Rating { get; set; }

		/// <summary>
		/// Start date of anime airing.
		/// </summary>
		[JsonProperty(PropertyName = "start_date")]
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// End date of anime airing.
		/// </summary>
		[JsonProperty(PropertyName = "end_date")]
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// Start date of user watching.
		/// </summary>
		[JsonProperty(PropertyName = "watch_start_date")]
		public DateTime? WatchStartDate { get; set; }

		/// <summary>
		/// End date of user watching.
		/// </summary>
		[JsonProperty(PropertyName = "watch_end_date")]
		public DateTime? WatchEndDate { get; set; }

		/// <summary>
		/// Time user has been watching anime.
		/// </summary>
		[JsonProperty(PropertyName = "days")]
		public int? Days { get; set; }

		/// <summary>
		/// Priority of anime on user's list.
		/// </summary>
		[JsonProperty(PropertyName = "priority")]
		public string Priority { get; set; }
	}
}