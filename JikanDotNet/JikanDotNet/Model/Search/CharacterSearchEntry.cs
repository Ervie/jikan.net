using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// URL to character's page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string URL { get; set; }

		/// <summary>
		/// Character's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Name of the character.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Nicknames of the character separated by comma.
		/// </summary>
		[JsonProperty(PropertyName = "nicknames")]
		public string Nicknames { get; set; }

		/// <summary>
		/// Character's animeography (without anime type).
		/// </summary>
		[JsonProperty(PropertyName = "anime")]
		public ICollection<MALSubItem> Animeography { get; set; }

		/// <summary>
		/// Character's mangaography (without anime type).
		/// </summary>
		[JsonProperty(PropertyName = "manga")]
		public ICollection<MALSubItem> Mangaography { get; set; }
	}
}