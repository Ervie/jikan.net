using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for single result from searching person.
	/// </summary>
	public class PersonSearchEntry
    {
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// URL to person's page.
		/// </summary>
		[JsonPropertyName("url")]
		public string URL { get; set; }

		/// <summary>
		/// Perosn's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Name of the person.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Nicknames/alternative names of the person separated by comma.
		/// </summary>
		[JsonPropertyName("alternative_names")]
		public ICollection<string> AlternativeNames { get; set; }
	}
}
