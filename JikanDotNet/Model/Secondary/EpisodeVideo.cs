using Newtonsoft.Json;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for episode video.
	/// </summary>
	public class EpisodeVideo
	{
		/// <summary>
		/// Title of the episode video.
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Numbered title of the episode video.
		/// </summary>
		[JsonProperty(PropertyName = "episode")]
		public string NumberedTitle { get; set; }

		/// <summary>
		/// Url to episode video.
		/// </summary>
		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// Episode's image URL.
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }
	}
}