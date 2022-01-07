using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Entry on manga model class (used in genres and magazines).
	/// </summary>
	public class MangaSubEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// URL to manga page.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Manga's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Title of the manga.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Manga type (e. g. "TV", "Movie").
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Manga members count on MyAnimeList.
		/// </summary>
		[JsonPropertyName("members")]
		public int? Members { get; set; }

		/// <summary>
		/// Manga's volume count.
		/// </summary>
		[JsonPropertyName("volumes")]
		public int? Volumes { get; set; }

		/// <summary>
		/// Manga's synopsis.
		/// </summary>
		[JsonPropertyName("synopsis")]
		public string Synopsis { get; set; }

		/// <summary>
		/// Manga's genres numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("genres")]
		public ICollection<MalUrl> Genres { get; set; }

		/// <summary>
		/// Manga's authors numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("authors")]
		public ICollection<MalUrl> Authors { get; set; }

		/// <summary>
		/// Manga's serialzations numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("serialization")]
		public ICollection<string> Serializations { get; set; }
	}
}