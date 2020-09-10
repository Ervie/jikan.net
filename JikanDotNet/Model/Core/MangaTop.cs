using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime top.
	/// </summary>
	public class MangaTop: BaseJikanRequest
	{
		/// <summary>
		/// Collection of anime entries on top list.
		/// </summary>
		[JsonPropertyName("top")]
		public ICollection<MangaTopEntry> Top { get; set; }
	}
}