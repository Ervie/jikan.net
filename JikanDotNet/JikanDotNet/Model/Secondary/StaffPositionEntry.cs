using Newtonsoft.Json;

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
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Staff's name.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Url to staff  main page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Staff's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Role associated with staff position.
		/// </summary>
		[JsonProperty(PropertyName = "role")]
		public string Role { get; set; }
	}
}