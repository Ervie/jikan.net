using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing sub item on MyAnimeList without image.
	/// </summary>
	public class MALSubItem
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Item type (e. g. "anime", "manga").
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		/// <summary>
		/// Url to sub item main page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Title of the item
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Overriden ToString method.
		/// </summary>
		/// <returns>Title if not null, base method elsewhere.</returns>
		public override string ToString()
		{
			return Title ?? base.ToString();
		}
	}
}