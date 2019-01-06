using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "pictures")]
		public ICollection<Picture> Pictures { get; set; }
	}
}