using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Image model class.
	/// </summary>
	public class Image
	{
		/// <summary>
		/// Url to default version of the image.
		/// </summary>
		[JsonPropertyName("image_url")]
		public string ImageUrl { get; set; }

		/// <summary>
		/// Url to small version of the image.
		/// </summary>
		[JsonPropertyName("small_image_url")]
		public string SmallImageUrl { get; set; }

		/// <summary>
		/// Url to medium version of the image.
		/// </summary>
		[JsonPropertyName("medium_image_url")]
		public string MediumImageUrl { get; set; }

		/// <summary>
		/// Url to large version of the image.
		/// </summary>
		[JsonPropertyName("large_image_url")]
		public string LargeImageUrl { get; set; }

		/// <summary>
		/// Url to version of image with the biggest resolution.
		/// </summary>
		[JsonPropertyName("maximum_image_url")]
		public string MaximumImageUrl { get; set; }
	}
}
