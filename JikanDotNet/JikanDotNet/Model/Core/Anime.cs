using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Anime model class.
	/// </summary>
	public class Anime
	{
		#region Basic request props

		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Anime's canonical link.
		/// </summary>
		[JsonProperty(PropertyName = "link_canonical")]
		public string LinkCanonical { get; set; }

		/// <summary>
		/// Title of the anime.
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Title of the anime in English.
		/// </summary>
		[JsonProperty(PropertyName = "title_english")]
		public string TitleEnglish { get; set; }

		/// <summary>
		/// Title of the anime in Japanese.
		/// </summary>
		[JsonProperty(PropertyName = "title_japanese")]
		public string TitleJapanese { get; set; }

		/// <summary>
		/// Anime's multiple titles (if any) seperated by a comma. Return null if there is none.
		/// </summary>
		[JsonProperty(PropertyName = "title_synonyms")]
		public string TitleSynonyms { get; set; }

		/// <summary>
		/// Anime's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Anime type (e. g. "TV", "Movie").
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Anime source (e .g. "Manga" or "Original").
		/// </summary>
		[JsonProperty(PropertyName = "source")]
		public string Source { get; set; }

		/// <summary>
		/// Anime's episode count.
		/// </summary>
		[JsonProperty(PropertyName = "episodes")]
		public string Episodes { get; set; }

		/// <summary>
		/// Anime's status (e. g. "Airing").
		/// </summary>
		[JsonProperty(PropertyName = "status")]
		public string Status { get; set; }

		/// <summary>
		/// Is anime currently airing.
		/// </summary>
		[JsonProperty(PropertyName = "airing")]
		public bool Airing { get; set; }

		/// <summary>
		/// Airing string parsed from anime's page.
		/// </summary>
		[JsonProperty(PropertyName = "aired_string")]
		public string AiredString { get; set; }

		/// <summary>
		/// Assiociative keys "from" and "to" which are alternative version of AiredString in ISO8601 format.
		/// </summary>
		[JsonProperty(PropertyName = "aired")]
		public TimePeriod Aired { get; set; }

		/// <summary>
		/// Anime's duration per episode.
		/// </summary>
		[JsonProperty(PropertyName = "duration")]
		public string Duration { get; set; }

		/// <summary>
		/// Anime's age rating.
		/// </summary>
		[JsonProperty(PropertyName = "rating")]
		public string Rating { get; set; }

		/// <summary>
		/// Anime's score on MyAnimeList up to 2 decimal places.
		/// </summary>
		[JsonProperty(PropertyName = "score")]
		public float? Score { get; set; }

		/// <summary>
		/// Number of people the anime has been scored by.
		/// </summary>
		[JsonProperty(PropertyName = "scored_by")]
		public int? ScoredBy { get; set; }

		/// <summary>
		/// Anime rank on MyAnimeList (score).
		/// </summary>
		[JsonProperty(PropertyName = "rank")]
		public int? Rank { get; set; }

		/// <summary>
		/// Anime popularity rank on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "popularity")]
		public int? Popularity { get; set; }

		/// <summary>
		/// Anime members count on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "members")]
		public int? Members { get; set; }

		/// <summary>
		/// Anime favourite count on MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "favorites")]
		public int? Favorites { get; set; }

		/// <summary>
		/// Anime's synopsis.
		/// </summary>
		[JsonProperty(PropertyName = "synopsis")]
		public string Synopsis { get; set; }

		/// <summary>
		/// Anime's background info. Return null if don't have any.
		/// </summary>
		[JsonProperty(PropertyName = "background")]
		public string Background { get; set; }

		/// <summary>
		/// Season and year the anime premiered.
		/// </summary>
		[JsonProperty(PropertyName = "premiered")]
		public string Premiered { get; set; }

		/// <summary>
		/// Anime broadcast day and timings (usually JST).
		/// </summary>
		[JsonProperty(PropertyName = "broadcast")]
		public string Broadcast { get; set; }

		/// <summary>
		/// Anime's related items (anime, manga, spin offs, etc.)
		/// </summary>
		[JsonProperty(PropertyName = "related")]
		public RelatedAnime Related { get; set; }

		/// <summary>
		/// Anime's producers numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "producer")]
		public ICollection<Producer> Producers { get; set; }

		/// <summary>
		/// Anime's licensors numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "licensor")]
		public ICollection<Licensor> Licensors { get; set; }

		/// <summary>
		/// Anime's studio(s) numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "studio")]
		public ICollection<Studio> Studios { get; set; }

		/// <summary>
		/// Anime's genres numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "genre")]
		public ICollection<Genre> Genres { get; set; }

		/// <summary>
		/// Anime's opening themes numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "opening_theme")]
		public ICollection<string> OpeningTheme { get; set; }

		/// <summary>
		/// Anime's ending themes numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "ending_theme")]
		public ICollection<string> EndingTheme { get; set; }

		#endregion Basic request props

		#region Episodes extension request props

		/// <summary>
		/// Anime's ending themes numerically indexed with array values.
		/// </summary>
		[JsonProperty(PropertyName = "episode")]
		public ICollection<AnimeEpisode> EpisodeCollection { get; set; }

		#endregion Episodes extension request props

		#region Characters and staff extension request props

		/// <summary>
		/// Manga's extra image URLs.
		/// </summary>
		[JsonProperty(PropertyName = "character")]
		public ICollection<CharacterEntry> Characters { get; set; }

		/// <summary>
		/// Manga's extra image URLs.
		/// </summary>
		[JsonProperty(PropertyName = "staff")]
		public ICollection<StaffPositionEntry> Staff { get; set; }

		#endregion Characters and staff extension request props

		#region Pictures request props

		/// <summary>
		/// Manga's extra image URLs.
		/// </summary>
		[JsonProperty(PropertyName = "image")]
		public ICollection<string> Images { get; set; }

		#endregion Pictures request props

		#region Videos request props

		/// <summary>
		/// Manga's extra image URLs.
		/// </summary>
		[JsonProperty(PropertyName = "promo")]
		public ICollection<PromoVideo> PromoVideos { get; set; }

		#endregion

		#region Stats request props

		/// <summary>
		/// Number of users who labeled anime status as "watching"
		/// </summary>
		[JsonProperty(PropertyName = "watching")]
		public int? Watching { get; set; }

		/// <summary>
		/// Number of users who labeled anime status as "completed"
		/// </summary>
		[JsonProperty(PropertyName = "completed")]
		public int? Completed { get; set; }

		/// <summary>
		/// Number of users who labeled anime status as "on hold"
		/// </summary>
		[JsonProperty(PropertyName = "on_hold")]
		public int? OnHold { get; set; }

		/// <summary>
		/// Number of users who labeled anime status as "dropped"
		/// </summary>
		[JsonProperty(PropertyName = "dropped")]
		public int? Dropped { get; set; }

		/// <summary>
		/// Number of users who labeled anime status as "plan to watch"
		/// </summary>
		[JsonProperty(PropertyName = "plan_to_read")]
		public int? PlanToWatch { get; set; }

		/// <summary>
		/// Number of users who added anime to their lists.
		/// </summary>
		[JsonProperty(PropertyName = "score_stats")]
		public ScoringStats ScoreStats { get; set; }

		#endregion Stats request props

		#region News request props

		/// <summary>
		/// News related to anime.
		/// </summary>
		[JsonProperty(PropertyName = "news")]
		public ICollection<News> News { get; set; }

		#endregion News request props

		#region Forum topics request props

		/// <summary>
		/// Forum topics related to anime.
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