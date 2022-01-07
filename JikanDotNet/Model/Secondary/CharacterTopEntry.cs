using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for entry on top characters list.
	/// </summary>
	public class CharacterTopEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Rank of the character on most popular list.
		/// </summary>
		[JsonPropertyName("rank")]
		public int Rank { get; set; }

		/// <summary>
		/// URL to character page.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Character's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Character's name.
		/// </summary>
		[JsonPropertyName("title")]
		public string Name { get; set; }

		/// <summary>
		/// Character's name written in kanji.
		/// </summary>
		[JsonPropertyName("name_kanji")]
		public string NameKanji { get; set; }

		/// <summary>
		/// Number of users who favorited character.
		/// </summary>
		[JsonPropertyName("favorites")]
		public int? Favorites { get; set; }

		/// <summary>
		/// Character's animeography.
		/// </summary>
		[JsonPropertyName("animeography")]
		public ICollection<MalUrl> Animeography { get; set; }

		/// <summary>
		/// Character's mangaography.
		/// </summary>
		[JsonPropertyName("mangaography")]
		public ICollection<MalUrl> Mangaography { get; set; }
	}
}