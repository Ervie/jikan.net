using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Rank of the character on most popular list.
		/// </summary>
		[JsonProperty(PropertyName = "rank")]
		public int Rank { get; set; }

		/// <summary>
		/// URL to character page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Character's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Character's name.
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Name { get; set; }

		/// <summary>
		/// Character's name written in kanji.
		/// </summary>
		[JsonProperty(PropertyName = "name_kanji")]
		public string NameKanji { get; set; }

		/// <summary>
		/// Number of users who favorited character.
		/// </summary>
		[JsonProperty(PropertyName = "favorites")]
		public int? Favorites { get; set; }

		/// <summary>
		/// Character's animeography.
		/// </summary>
		[JsonProperty(PropertyName = "animeography")]
		public ICollection<MALSubItem> Animeography { get; set; }

		/// <summary>
		/// Character's mangaography.
		/// </summary>
		[JsonProperty(PropertyName = "mangaography")]
		public ICollection<MALSubItem> Mangaography { get; set; }
	}
}