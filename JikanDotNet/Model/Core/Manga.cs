using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Manga model class.
	/// </summary>
	public class Manga: BaseJikanRequest
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Manga's canonical link.
		/// </summary>
		[JsonPropertyName("url")]
		public string LinkCanonical { get; set; }

		/// <summary>
		/// Title of the manga.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Title of the manga in English.
		/// </summary>
		[JsonPropertyName("title_english")]
		public string TitleEnglish { get; set; }

		/// <summary>
		/// Title of the manga in English.
		/// </summary>
		[JsonPropertyName("title_japanese")]
		public string TitleJapanese { get; set; }

		/// <summary>
		/// Manga's multiple titles (if any). Return null if there is none.
		/// </summary>
		[JsonPropertyName("title_synonyms")]
		public ICollection<string> TitleSynonyms { get; set; }

		/// <summary>
		/// Manga's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Manga's status (e. g. "Finished").
		/// </summary>
		[JsonPropertyName("status")]
		public string Status { get; set; }

		/// <summary>
		/// Manga type (e. g. "Manga", "Light Novel").
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Manga's volume count.
		/// </summary>
		[JsonPropertyName("volumes")]
		public int? Volumes { get; set; }

		/// <summary>
		/// Manga's chapter count.
		/// </summary>
		[JsonPropertyName("chapters")]
		public int? Chapters { get; set; }

		/// <summary>
		/// Is manga currently being published..
		/// </summary>
		[JsonPropertyName("publishing")]
		public bool Publishing { get; set; }

		/// <summary>
		/// Assiociative keys "from" and "to" which are alternative version of PublishedString in ISO8601 format.
		/// </summary>
		[JsonPropertyName("published")]
		public TimePeriod Published { get; set; }

		/// <summary>
		/// Manga's score on MyAnimeList up to 2 decimal places.
		/// </summary>
		[JsonPropertyName("score")]
		public float? Score { get; set; }

		/// <summary>
		/// Number of people the manga has been scored by.
		/// </summary>
		[JsonPropertyName("scored_by")]
		public int? ScoredBy { get; set; }

		/// <summary>
		/// Manga rank on MyAnimeList (score).
		/// </summary>
		[JsonPropertyName("rank")]
		public int? Rank { get; set; }

		/// <summary>
		/// Manga popularity rank on MyAnimeList.
		/// </summary>
		[JsonPropertyName("popularity")]
		public int? Popularity { get; set; }

		/// <summary>
		/// Manga members count on MyAnimeList.
		/// </summary>
		[JsonPropertyName("members")]
		public int? Members { get; set; }

		/// <summary>
		/// Manga favourite count on MyAnimeList.
		/// </summary>
		[JsonPropertyName("favorites")]
		public int? Favorites { get; set; }

		/// <summary>
		/// Manga's synopsis.
		/// </summary>
		[JsonPropertyName("synopsis")]
		public string Synopsis { get; set; }

		/// <summary>
		/// Manga's background info. Return null if don't have any.
		/// </summary>
		[JsonPropertyName("background")]
		public string Background { get; set; }

		/// <summary>
		/// Manga's related items (anime, manga, spin offs, etc.)
		/// </summary>
		[JsonPropertyName("related")]
		public RelatedManga Related { get; set; }

		/// <summary>
		/// Manga's genres numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("genres")]
		public ICollection<MALSubItem> Genres { get; set; }

		/// <summary>
		/// Manga's authors numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("authors")]
		public ICollection<MALSubItem> Authors { get; set; }

		/// <summary>
		/// Manga's serialzations numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("serializations")]
		public ICollection<MALSubItem> Serializations { get; set; }
	}
}