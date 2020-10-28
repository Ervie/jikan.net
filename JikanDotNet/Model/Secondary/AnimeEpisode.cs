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
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// URL to the episode.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Title of the episode.
		/// </summary>
		[JsonPropertyName("title")]
		public AnimeTitle Title { get; set; }

		/// <summary>
		/// Episode's duration.
		/// </summary>
		[JsonPropertyName("duration")]
		public string Duration { get; set; }

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
		/// Episode's synopsis.
		/// </summary>
		[JsonPropertyName("synopsis")]
		public string Synopsis { get; set; }

		/// <summary>
		/// URL to the Crunchyroll video.
		/// </summary>
		[JsonPropertyName("crunchyroll")]
		public AnimeCrunchyrollVideo Crunchyroll { get; set; }
	}
}