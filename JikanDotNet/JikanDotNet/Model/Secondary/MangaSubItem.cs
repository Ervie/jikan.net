using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing manga sub item on MyAnimeList.
	/// </summary>
	public class MangaSubItem
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Item type (e. g. "anime").
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Url sub item main page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Title of the item
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }
	}
}