using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "pictures")]
		public ICollection<Picture> Pictures { get; set; }
	}
}