using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Manga's picture model class.
	/// </summary>
	public class MangaPictures
	{
		/// <summary>
		/// Manga's extra image URLs.
		/// </summary>
		[JsonProperty(PropertyName = "pictures")]
		public ICollection<Picture> Pictures { get; set; }
	}
}