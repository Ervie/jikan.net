using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for promo video.
	/// </summary>
	public class PromoVideo
	{
		/// <summary>
		/// Title of the promotional video.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// Url to promotional video.
		/// </summary>
		[JsonPropertyName("video_url")]
		public string VideoUrl { get; set; }

		/// <summary>
		/// Promo's image URL.
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageURL { get; set; }
	}
}