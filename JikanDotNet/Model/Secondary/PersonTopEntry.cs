using Newtonsoft.Json;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for entry on top people list.
	/// </summary>
	public class PersonTopEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Rank of the person on most popular list.
		/// </summary>
		[JsonProperty(PropertyName = "rank")]
		public int Rank { get; set; }

		/// <summary>
		/// URL to person page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Person's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Person's name.
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Name { get; set; }

		/// <summary>
		/// Person's name written in kanji.
		/// </summary>
		[JsonProperty(PropertyName = "name_kanji")]
		public string NameKanji { get; set; }

		/// <summary>
		/// Number of users who favorited person.
		/// </summary>
		[JsonProperty(PropertyName = "favorites")]
		public int? Favorites { get; set; }

		/// <summary>
		/// Date of airing start.
		/// </summary>
		[JsonProperty(PropertyName = "birthday")]
		public DateTime? Birthday { get; set; }
	}
}