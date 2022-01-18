using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Magazine model class.
	/// </summary>
	public class Magazine
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonPropertyName("mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Url to sub item main page.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Name of the magazine
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// Total count of manga assigned to this magazine
		/// </summary>
		[JsonPropertyName("count")]
		public int TotalCount { get; set; }
	}
}