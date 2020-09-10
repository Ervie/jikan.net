using System.Text.Json.Serialization;
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
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Rank of the person on most popular list.
		/// </summary>
		[JsonPropertyName("rank")]
		public int Rank { get; set; }

		/// <summary>
		/// URL to person page.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Person's image URL
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Person's name.
		/// </summary>
		[JsonPropertyName("title")]
		public string Name { get; set; }

		/// <summary>
		/// Person's name written in kanji.
		/// </summary>
		[JsonPropertyName("name_kanji")]
		public string NameKanji { get; set; }

		/// <summary>
		/// Number of users who favorited person.
		/// </summary>
		[JsonPropertyName("favorites")]
		public int? Favorites { get; set; }

		/// <summary>
		/// Date of airing start.
		/// </summary>
		[JsonPropertyName("birthday")]
		public DateTime? Birthday { get; set; }
	}
}