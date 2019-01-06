using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for result from searching anime.
	/// </summary>
	public class AnimeSearchResult : BaseJikanRequest
	{
		/// <summary>
		/// List of search results.
		/// </summary>
		[JsonProperty(PropertyName = "results")]
		public ICollection<AnimeSearchEntry> Results { get; set; }

		/// <summary>
		/// Index of the last page.
		/// </summary>
		[JsonProperty(PropertyName = "last_page")]
		public int? ResultLastPage { get; set; }
	}
}