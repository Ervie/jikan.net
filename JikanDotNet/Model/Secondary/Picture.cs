using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Picture model class.
	/// </summary>
	public class Picture
	{
		/// <summary>
		/// Url to small version of the picture.
		/// </summary>
		[JsonPropertyName("small")]
		public string Small { get; set; }

		/// <summary>
		/// Url to large version of the picture.
		/// </summary>
		[JsonPropertyName("large")]
		public string Large { get; set; }
	}
}