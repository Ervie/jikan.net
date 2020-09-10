using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for single result from searching character.
	/// </summary>
	public class CharacterSearchEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// URL to character's page.
		/// </summary>
		[JsonPropertyName("url")]
		public string URL { get; set; }

		/// <summary>
		/// Character's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Name of the character.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Nicknames/alternative names of the character separated by comma.
		/// </summary>
		[JsonPropertyName("alternative_names")]
		public ICollection<string> AlternativeNames { get; set; }

		/// <summary>
		/// Character's animeography (without anime type).
		/// </summary>
		[JsonPropertyName("anime")]
		public ICollection<MALSubItem> Animeography { get; set; }

		/// <summary>
		/// Character's mangaography (without anime type).
		/// </summary>
		[JsonPropertyName("manga")]
		public ICollection<MALSubItem> Mangaography { get; set; }
	}
}