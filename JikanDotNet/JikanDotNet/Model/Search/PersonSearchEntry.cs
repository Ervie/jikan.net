using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// URL to person's page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string URL { get; set; }

		/// <summary>
		/// Perosn's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Name of the person.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Nicknames of the person separated by comma.
		/// </summary>
		[JsonProperty(PropertyName = "nicknames")]
		public string Nicknames { get; set; }
	}
}
