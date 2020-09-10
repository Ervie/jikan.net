using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Anime model class.
	/// </summary>
	public class Anime : BaseJikanRequest, IMalEntity
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Anime's canonical link.
		/// </summary>
		[JsonPropertyName("url")]
		public string LinkCanonical { get; set; }

		/// <summary>
		/// Title of the anime.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Title of the anime in English.
		/// </summary>
		[JsonPropertyName("title_english")]
		public string TitleEnglish { get; set; }

		/// <summary>
		/// Title of the anime in Japanese.
		/// </summary>
		[JsonPropertyName("title_japanese")]
		public string TitleJapanese { get; set; }

		/// <summary>
		/// Anime's multiple titles (if any). Return null if there is none.
		/// </summary>
		[JsonPropertyName("title_synonyms")]
		public ICollection<string> TitleSynonyms { get; set; }

		/// <summary>
		/// Anime's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Anime type (e. g. "TV", "Movie").
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Anime source (e .g. "Manga" or "Original").
		/// </summary>
		[JsonPropertyName("source")]
		public string Source { get; set; }

		/// <summary>
		/// Anime's episode count.
		/// </summary>
		[JsonPropertyName("episodes")]
		public int? Episodes { get; set; }

		/// <summary>
		/// Anime's status (e. g. "Airing").
		/// </summary>
		[JsonPropertyName("status")]
		public string Status { get; set; }

		/// <summary>
		/// Is anime currently airing.
		/// </summary>
		[JsonPropertyName("airing")]
		public bool Airing { get; set; }
		
		/// <summary>
		/// Assiociative keys "from" and "to" which are alternative version of AiredString in ISO8601 format.
		/// </summary>
		[JsonPropertyName("aired")]
		public TimePeriod Aired { get; set; }

		/// <summary>
		/// Anime's duration per episode.
		/// </summary>
		[JsonPropertyName("duration")]
		public string Duration { get; set; }

		/// <summary>
		/// Anime's age rating.
		/// </summary>
		[JsonPropertyName("rating")]
		public string Rating { get; set; }

		/// <summary>
		/// Anime's score on MyAnimeList up to 2 decimal places.
		/// </summary>
		[JsonPropertyName("score")]
		public float? Score { get; set; }

		/// <summary>
		/// Number of people the anime has been scored by.
		/// </summary>
		[JsonPropertyName("scored_by")]
		public int? ScoredBy { get; set; }

		/// <summary>
		/// Anime rank on MyAnimeList (score).
		/// </summary>
		[JsonPropertyName("rank")]
		public int? Rank { get; set; }

		/// <summary>
		/// Anime popularity rank on MyAnimeList.
		/// </summary>
		[JsonPropertyName("popularity")]
		public int? Popularity { get; set; }

		/// <summary>
		/// Anime members count on MyAnimeList.
		/// </summary>
		[JsonPropertyName("members")]
		public int? Members { get; set; }

		/// <summary>
		/// Anime favourite count on MyAnimeList.
		/// </summary>
		[JsonPropertyName("favorites")]
		public int? Favorites { get; set; }

		/// <summary>
		/// Anime's synopsis.
		/// </summary>
		[JsonPropertyName("synopsis")]
		public string Synopsis { get; set; }

		/// <summary>
		/// Anime's background info. Return null if don't have any.
		/// </summary>
		[JsonPropertyName("background")]
		public string Background { get; set; }

		/// <summary>
		/// Season and year the anime premiered.
		/// </summary>
		[JsonPropertyName("premiered")]
		public string Premiered { get; set; }

		/// <summary>
		/// Anime broadcast day and timings (usually JST).
		/// </summary>
		[JsonPropertyName("broadcast")]
		public string Broadcast { get; set; }

		/// <summary>
		/// Anime's related items (anime, manga, spin offs, etc.)
		/// </summary>
		[JsonPropertyName("related")]
		public RelatedAnime Related { get; set; }

		/// <summary>
		/// Anime's producers numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("producers")]
		public ICollection<MALSubItem> Producers { get; set; }

		/// <summary>
		/// Anime's licensors numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("licensors")]
		public ICollection<MALSubItem> Licensors { get; set; }

		/// <summary>
		/// Anime's studio(s) numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("studios")]
		public ICollection<MALSubItem> Studios { get; set; }

		/// <summary>
		/// Anime's genres numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("genres")]
		public ICollection<MALSubItem> Genres { get; set; }

		/// <summary>
		/// Anime's opening themes numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("opening_themes")]
		public ICollection<string> OpeningTheme { get; set; }

		/// <summary>
		/// Anime's ending themes numerically indexed with array values.
		/// </summary>
		[JsonPropertyName("ending_themes")]
		public ICollection<string> EndingTheme { get; set; }
	}
}