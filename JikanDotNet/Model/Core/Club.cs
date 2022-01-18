using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Club profile model class.
	/// </summary>
	public class Club
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Club's URL.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Club's image set
		/// </summary>
		[JsonPropertyName("images")]
		public ImagesSet Images { get; set; }

		/// <summary>
		/// Name of the club.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Club's members count.
		/// </summary>
		[JsonPropertyName("members")]
		public int? MembersCount { get; set; }

		/// <summary>
		/// Club's category (Anime/Manga/Japan etc.)
		/// </summary>
		[JsonPropertyName("category")]
		public string Category { get; set; }

		/// <summary>
		/// Club's access type (public/private).
		/// </summary>
		[JsonPropertyName("access")]
		public string Access { get; set; }

		/// <summary>
		/// Club's date of creation.
		/// </summary>
		[JsonPropertyName("created")]
		public DateTime? Created { get; set; }

		/// <summary>
		/// Club's staff.
		/// </summary>
		[JsonPropertyName("staff")]
		public ICollection<MalUrl> Staff { get; set; }

		/// <summary>
		/// Club's anime relations.
		/// </summary>
		[JsonPropertyName("anime_relations")]
		public ICollection<MalUrl> AnimeRelations { get; set; }

		/// <summary>
		/// Club's manga relations.
		/// </summary>
		[JsonPropertyName("manga_relations")]
		public ICollection<MalUrl> MangaRelations { get; set; }

		/// <summary>
		/// Club's character relations.
		/// </summary>
		[JsonPropertyName("character_relations")]
		public ICollection<MalUrl> CharacterRelations { get; set; }
	}
}