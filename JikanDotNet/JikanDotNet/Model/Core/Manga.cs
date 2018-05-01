using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JikanDotNet
{
	/// <summary>
	/// Manga model class.
	/// </summary>
	public class Manga
    {
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Manga's canonical link.
		/// </summary>
		[JsonProperty(PropertyName = "link_canonical")]
		public string LinkCanonical { get; set; }

		/// <summary>
		/// Title of the manga.
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Title of the manga in English.
		/// </summary>
		[JsonProperty(PropertyName = "title_english")]
		public string TitleEnglish { get; set; }

		/// <summary>
		/// Title of the manga in English.
		/// </summary>
		[JsonProperty(PropertyName = "title_japanese")]
		public string TitleJapanese { get; set; }

		/// <summary>
		/// Manga's multiple titles (if any) seperated by a comma. Return null if there is none.
		/// </summary>
		[JsonProperty(PropertyName = "title_synonyms")]
		public string TitleSynonyms { get; set; }

		/// <summary>
		/// Manga's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Manga's status (e. g. "Finished").
		/// </summary>
		[JsonProperty(PropertyName = "status")]
		public string Status { get; set; }

		/// <summary>
		/// Manga type (e. g. "Manga", "Light Novel").
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Manga's volume count.
		/// </summary>
		[JsonProperty(PropertyName = "volumes")]
		public string Volumes { get; set; }

		/// <summary>
		/// Manga's chapter count.
		/// </summary>
		[JsonProperty(PropertyName = "chapters")]
		public string Chapters { get; set; }

		/// <summary>
		/// Is manga currently being published..
		/// </summary>
		[JsonProperty(PropertyName = "publishing")]
		public bool Publishing { get; set; }

		/// <summary>
		/// Airing string parsed from manga's page.
		/// </summary>
		[JsonProperty(PropertyName = "published_string")]
		public string PublishedString { get; set; }

		/// <summary>
		/// Assiociative keys "from" and "to" which are alternative version of PublishedString in ISO8601 format.
		/// </summary>
		[JsonProperty(PropertyName = "published")]
		public TimePeriod Published { get; set; }

		/// <summary>
		/// Manga's score on MyAnimeList up to 2 decimal places.
		/// </summary>
		[JsonProperty(PropertyName = "score")]
		public float Score { get; set; }

		/// <summary>
		/// Number of people the manga has been scored by.
		/// </summary>
		[JsonProperty(PropertyName = "scored_by")]
		public int ScoredBy { get; set; }

		/// <summary>
		/// Manga rank on MyAnimeList (score).
		/// </summary>
		[JsonProperty(PropertyName = "rank")]
		public int Rank { get; set; }

		/// <summary>
		/// Manga popularity rank on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "popularity")]
		public int Popularity { get; set; }

		/// <summary>
		/// Manga members count on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "members")]
		public int Members { get; set; }

		/// <summary>
		/// Manga favourite count on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "favorites")]
		public int Favorites { get; set; }

		/// <summary>
		/// Manga's synopsis.
		/// </summary>
		[JsonProperty(PropertyName = "synopsis")]
		public string Synopsis { get; set; }

		/// <summary>
		/// Manga's background info. Return null if don't have any.
		/// </summary>
		[JsonProperty(PropertyName = "background")]
		public string Background { get; set; }

		/// <summary>
		/// Manga's related items (anime, manga, spin offs, etc.)
		/// </summary>
		[JsonProperty(PropertyName = "related")]
		public RelatedManga Related { get; set; }

		/// <summary>
		/// Manga's genres numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "genre")]
		public ICollection<MALUrl> Genres { get; set; }

		/// <summary>
		/// Manga's authors numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "author")]
		public ICollection<MALUrl> Authors { get; set; }

		/// <summary>
		/// Manga's serialzations numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "serialization")]
		public ICollection<MALUrl> Serializations { get; set; }
	}
}
