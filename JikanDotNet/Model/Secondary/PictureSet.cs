using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet.Model.Secondary
{
	/// <summary>
	/// Set of pictures in various formats.
	/// </summary>
	public class PictureSet
	{
		/// <summary>
		/// Pictures in JPG format.
		/// </summary>
		[JsonPropertyName("jpg")]
		public ICollection<Picture> JPG { get; set; }

		/// <summary>
		/// Pictures in webp format.
		/// </summary>
		[JsonPropertyName("webp")]
		public ICollection<Picture> WebP { get; set; }
	}
}