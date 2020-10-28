using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime staff position (anime request).
	/// </summary>
	public class StaffPositionEntry
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Staff's name.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Url to staff  main page.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Staff's images set.
		/// </summary>
		[JsonPropertyName("image")]
		public Picture ImageURL { get; set; }

		/// <summary>
		/// Role associated with staff position.
		/// </summary>
		[JsonPropertyName("positions")]
		public ICollection<string> Role { get; set; }
	}
}