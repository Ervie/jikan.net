using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Manga's picture model class.
	/// </summary>
	public class MangaPictures: BaseJikanRequest
	{
		/// <summary>
		/// Manga's extra image URLs.
		/// </summary>
		[JsonPropertyName("pictures")]
		public ICollection<Image> Pictures { get; set; }
	}
}