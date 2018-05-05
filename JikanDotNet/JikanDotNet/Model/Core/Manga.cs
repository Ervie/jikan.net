using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Manga model class.
	/// </summary>
	public class Manga
	{
		#region Basic request props

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
		public float? Score { get; set; }

		/// <summary>
		/// Number of people the manga has been scored by.
		/// </summary>
		[JsonProperty(PropertyName = "scored_by")]
		public int? ScoredBy { get; set; }

		/// <summary>
		/// Manga rank on MyAnimeList (score).
		/// </summary>
		[JsonProperty(PropertyName = "rank")]
		public int? Rank { get; set; }

		/// <summary>
		/// Manga popularity rank on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "popularity")]
		public int? Popularity { get; set; }

		/// <summary>
		/// Manga members count on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "members")]
		public int? Members { get; set; }

		/// <summary>
		/// Manga favourite count on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "favorites")]
		public int? Favorites { get; set; }

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
		public ICollection<Genre> Genres { get; set; }

		/// <summary>
		/// Manga's authors numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "author")]
		public ICollection<Author> Authors { get; set; }

		/// <summary>
		/// Manga's serialzations numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "serialization")]
		public ICollection<Serialization> Serializations { get; set; }

		#endregion Basic request props

		#region Pictures request props

		/// <summary>
		/// Manga's extra image URLs.
		/// </summary>
		[JsonProperty(PropertyName = "image")]
		public ICollection<string> Images { get; set; }

		#endregion Pictures request props

		#region Characters request props

		/// <summary>
		/// Manga's extra image URLs.
		/// </summary>
		[JsonProperty(PropertyName = "character")]
		public ICollection<CharacterEntry> Characters { get; set; }

		#endregion Characters request props

		#region Stats request props

		/// <summary>
		/// Number of users who labeled manga status as "reading"
		/// </summary>
		[JsonProperty(PropertyName = "reading")]
		public int? Reading { get; set; }

		/// <summary>
		/// Number of users who labeled manga status as "completed"
		/// </summary>
		[JsonProperty(PropertyName = "completed")]
		public int? Completed { get; set; }

		/// <summary>
		/// Number of users who labeled manga status as "on hold"
		/// </summary>
		[JsonProperty(PropertyName = "on_hold")]
		public int? OnHold { get; set; }

		/// <summary>
		/// Number of users who labeled manga status as "dropped"
		/// </summary>
		[JsonProperty(PropertyName = "dropped")]
		public int? Dropped { get; set; }

		/// <summary>
		/// Number of users who labeled manga status as "plan to read"
		/// </summary>
		[JsonProperty(PropertyName = "plan_to_read")]
		public int? PlanToRead { get; set; }

		/// <summary>
		/// Number of users who added manga to their lists.
		/// </summary>
		[JsonProperty(PropertyName = "score_stats")]
		public ScoringStats ScoreStats { get; set; }

		#endregion Stats request props

		#region News request props

		/// <summary>
		/// News related to manga.
		/// </summary>
		[JsonProperty(PropertyName = "news")]
		public ICollection<News> News { get; set; }

		#endregion News request props

		#region Forum topics request props

		/// <summary>
		/// Forum topics related to manga.
		/// </summary>
		[JsonProperty(PropertyName = "topic")]
		public ICollection<ForumTopic> Topics { get; set; }

		#endregion Forum topics request props

		#region More info request props

		/// <summary>
		/// Extra information stored in "more info" tab.
		/// </summary>
		[JsonProperty(PropertyName = "more_info")]
		public string MoreInfo { get; set; }

		#endregion More info request props
	}
}