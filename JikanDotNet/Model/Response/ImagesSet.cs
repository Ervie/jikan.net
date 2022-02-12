using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Set of images in various formats.
	/// </summary>
	public class ImagesSet

	{
		/// <summary>
		/// Images in JPG format.
		/// </summary>
		[JsonPropertyName("jpg")]
		public Image JPG { get; set; }

		/// <summary>
		/// Images in webp format.
		/// </summary>
		[JsonPropertyName("webp")]
		public Image WebP { get; set; }
	}
}