using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Person's picture model class.
	/// </summary>
	public class PersonPictures: BaseJikanRequest
	{
		/// <summary>
		/// Person's extra image URLs.
		/// </summary>
		[JsonPropertyName("pictures")]
		public ICollection<Picture> Pictures { get; set; }
	}
}