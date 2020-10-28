using System;
using System.Text.Json.Serialization;

namespace JikanDotNet.Model
{
	/// <summary>
	/// Anime episode summary model class.
	/// </summary>
	internal class AnimeEpisodeSummary
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long Id { get; set; }

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
		/// URL to the episode.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Snippet of the forum topic of the episode.
		/// </summary>
		[JsonPropertyName("topic")]
		public ForumTopicSnippet Topic { get; set; }
	}
}