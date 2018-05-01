using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing url link on MyAnimeList.
	/// </summary>
	public class MALUrl
	{
		/// <summary>
		/// Url to link.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Text displayed on link.
		/// </summary>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }
	}
}