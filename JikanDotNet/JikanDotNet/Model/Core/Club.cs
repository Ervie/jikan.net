using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JikanDotNet
{
	/// <summary>
	/// Club profile model class.
	/// </summary>
	public class Club : IMalEntity
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Club's image URL.
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Club's URL.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string URL { get; set; }

		/// <summary>
		/// Title of the club.
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Club's members count.
		/// </summary>
		[JsonProperty(PropertyName = "members_count")]
		public int? MembersCount { get; set; }

		/// <summary>
		/// Club's pictures count.
		/// </summary>
		[JsonProperty(PropertyName = "pictures_count")]
		public int? PicturesCount { get; set; }

		/// <summary>
		/// Club's category (Anime/Manga/Japan etc.)
		/// </summary>
		[JsonProperty(PropertyName = "category")]
		public string Category { get; set; }

		/// <summary>
		/// Club's type (public/private).
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Club's date of creation.
		/// </summary>
		[JsonProperty(PropertyName = "created")]
		public DateTime? Created { get; set; }

		/// <summary>
		/// Club's staff.
		/// </summary>
		[JsonProperty(PropertyName = "staff")]
		public ICollection<MALSubItem> Staff { get; set; }

		/// <summary>
		/// Club's anime relations.
		/// </summary>
		[JsonProperty(PropertyName = "anime_relations")]
		public ICollection<MALSubItem> AnimeRelations { get; set; }

		/// <summary>
		/// Club's manga relations.
		/// </summary>
		[JsonProperty(PropertyName = "manga_relations")]
		public ICollection<MALSubItem> MangaRelations { get; set; }

		/// <summary>
		/// Club's character relations.
		/// </summary>
		[JsonProperty(PropertyName = "character_relations")]
		public ICollection<MALSubItem> CharacterRelations { get; set; }
	}
}
