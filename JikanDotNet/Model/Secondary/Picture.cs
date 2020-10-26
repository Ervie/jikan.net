using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Picture model class.
	/// </summary>
	public class Picture
	{
		/// <summary>
		/// Url to default version of the picture.
		/// </summary>
		[JsonPropertyName("image_url")]
		public string Default { get; set; }

		/// <summary>
		/// Url to small version of the picture.
		/// </summary>
		[JsonPropertyName("small_image_url")]
		public string Small { get; set; }

		/// <summary>
		/// Url to medium version of the picture.
		/// </summary>
		[JsonPropertyName("medium_image_url")]
		public string Medium { get; set; }

		/// <summary>
		/// Url to large version of the picture.
		/// </summary>
		[JsonPropertyName("large_image_url")]
		public string Large { get; set; }

		/// <summary>
		/// Url to version of picture with the biggest resolution.
		/// </summary>
		[JsonPropertyName("maximum_image_url")]
		public string Maximum { get; set; }
	}
}