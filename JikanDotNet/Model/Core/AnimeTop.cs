using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime top.
	/// </summary>
    public class AnimeTop: BaseJikanRequest
	{
		/// <summary>
		/// Collection of anime entries on top list.
		/// </summary>
		[JsonPropertyName("top")]
		public ICollection<AnimeTopEntry> Top { get; set; }
    }
}
