using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Character's picture model class.
	/// </summary>
	public class CharacterPictures: BaseJikanRequest
	{
		/// <summary>
		/// Character's extra image URLs.
		/// </summary>
		[JsonPropertyName("pictures")]
		public ICollection<Image> Pictures { get; set; }
	}
}