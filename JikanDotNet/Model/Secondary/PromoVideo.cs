using Newtonsoft.Json;

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
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Url to promotional video.
		/// </summary>
		[JsonProperty(PropertyName = "video_url")]
		public string VideoUrl { get; set; }

		/// <summary>
		/// Promo's image URL.
		/// </summary>
		[JsonProperty(PropertyName = "image_url")]
		public string ImageURL { get; set; }
	}
}