using System.Text.Json.Serialization;

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
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Numbered title of the episode video.
		/// </summary>
		[JsonPropertyName("episode")]
		public string NumberedTitle { get; set; }

		/// <summary>
		/// Url to episode video.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Episode's image URL.
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }
	}
}