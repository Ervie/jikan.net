using System.Text.Json.Serialization;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Anime episode model class.
	/// </summary>
	public class AnimeEpisode
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("episode_id")]
		public long Id { get; set; }

		/// <summary>
		/// Title of the episode.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Title of the anime in Japanese.
		/// </summary>
		[JsonPropertyName("title_japanese")]
		public string TitleJapanese { get; set; }

		/// <summary>
		/// Title of the anime in romanji.
		/// </summary>
		[JsonPropertyName("title_romanji")]
		public string TitleEnglish { get; set; }

		/// <summary>
		/// Date when episode aired at first.
		/// </summary>
		[JsonPropertyName("aired")]
		public DateTime? Aired { get; set; }

		/// <summary>
		/// Is the episode filler.
		/// </summary>
		[JsonPropertyName("filler")]
		public bool? Filler { get; set; }

		/// <summary>
		/// Is the episode recap.
		/// </summary>
		[JsonPropertyName("recap")]
		public bool? Recap { get; set; }

		/// <summary>
		/// URL to the video of the episode.
		/// </summary>
		[JsonPropertyName("video_url")]
		public string VideoUrl { get; set; }

		/// <summary>
		/// URL to the forum topic of the episode.
		/// </summary>
		[JsonPropertyName("forum_url")]
		public string ForumUrl { get; set; }
	}
}