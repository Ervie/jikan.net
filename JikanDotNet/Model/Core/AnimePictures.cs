using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Anime's picture model class.
	/// </summary>
	public class AnimePictures: BaseJikanRequest
	{
		/// <summary>
		/// Anime's extra image URLs.
		/// </summary>
		[JsonPropertyName("pictures")]
		public ICollection<Picture> Pictures { get; set; }
	}
}