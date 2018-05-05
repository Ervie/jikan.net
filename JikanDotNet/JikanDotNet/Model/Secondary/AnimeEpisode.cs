using Newtonsoft.Json;

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
		[JsonProperty(PropertyName = "id")]
		public long Id { get; set; }

		/// <summary>
		/// Title of the episode.
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Title of the anime in Japanese.
		/// </summary>
		[JsonProperty(PropertyName = "title_japanese")]
		public string TitleJapanese { get; set; }

		/// <summary>
		/// Title of the anime in romanji.
		/// </summary>
		[JsonProperty(PropertyName = "title_romanji")]
		public string TitleEnglish { get; set; }

		/// <summary>
		/// Episode number as a string.
		/// </summary>
		[JsonProperty(PropertyName = "episode")]
		public string EpisodeNumber { get; set; }

		/// <summary>
		/// Date when episode aired at first.
		/// </summary>
		[JsonProperty(PropertyName = "aired")]
		public string Aired { get; set; }

		/// <summary>
		/// Is the episode filler.
		/// </summary>
		[JsonProperty(PropertyName = "filler")]
		public bool? Filler { get; set; }

		/// <summary>
		/// Is the episode recap.
		/// </summary>
		[JsonProperty(PropertyName = "recap")]
		public bool? Recap { get; set; }

		/// <summary>
		/// URL to the video of the episode.
		/// </summary>
		[JsonProperty(PropertyName = "video_url")]
		public string VideoUrl { get; set; }

		/// <summary>
		/// URL to the forum topic of the episode.
		/// </summary>
		[JsonProperty(PropertyName = "forum_url")]
		public string ForumUrl { get; set; }
	}
}