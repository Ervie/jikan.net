using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime top.
	/// </summary>
	public class MangaTop
	{
		/// <summary>
		/// Collection of anime entries on top list.
		/// </summary>
		[JsonProperty(PropertyName = "top")]
		public ICollection<MangaTopEntry> Top { get; set; }
	}
}