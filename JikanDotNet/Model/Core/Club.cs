using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace JikanDotNet
{
	/// <summary>
	/// Club profile model class.
	/// </summary>
	public class Club : BaseJikanRequest, IMalEntity
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Club's image URL.
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Club's URL.
		/// </summary>
		[JsonPropertyName("url")]
		public string URL { get; set; }

		/// <summary>
		/// Title of the club.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Club's members count.
		/// </summary>
		[JsonPropertyName("members_count")]
		public int? MembersCount { get; set; }

		/// <summary>
		/// Club's pictures count.
		/// </summary>
		[JsonPropertyName("pictures_count")]
		public int? PicturesCount { get; set; }

		/// <summary>
		/// Club's category (Anime/Manga/Japan etc.)
		/// </summary>
		[JsonPropertyName("category")]
		public string Category { get; set; }

		/// <summary>
		/// Club's type (public/private).
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// Club's date of creation.
		/// </summary>
		[JsonPropertyName("created")]
		public DateTime? Created { get; set; }

		/// <summary>
		/// Club's staff.
		/// </summary>
		[JsonPropertyName("staff")]
		public ICollection<MALSubItem> Staff { get; set; }

		/// <summary>
		/// Club's anime relations.
		/// </summary>
		[JsonPropertyName("anime_relations")]
		public ICollection<MALSubItem> AnimeRelations { get; set; }

		/// <summary>
		/// Club's manga relations.
		/// </summary>
		[JsonPropertyName("manga_relations")]
		public ICollection<MALSubItem> MangaRelations { get; set; }

		/// <summary>
		/// Club's character relations.
		/// </summary>
		[JsonPropertyName("character_relations")]
		public ICollection<MALSubItem> CharacterRelations { get; set; }
	}
}
