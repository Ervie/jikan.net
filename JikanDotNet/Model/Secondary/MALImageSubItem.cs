using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing sub item on MyAnimeList with image.
	/// </summary>
	public class MALImageSubItem
	{
		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		[JsonProperty(PropertyName = "mal_id")]
		public long MalId { get; set; }

		/// <summary>
		/// Item's name.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Url to sub item main page.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Item's image URL
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }

		/// <summary>
		/// Role of character in sub item (anime or manga). Not available in all requests.
		/// </summary>
		[JsonProperty(PropertyName = "role")]
		public string Role { get; set; }

		/// <summary>
		/// Overriden ToString method.
		/// </summary>
		/// <returns>Name if not null, base method elsewhere.</returns>
		public override string ToString()
		{
			return Name ?? base.ToString();
		}
	}
}