using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for result from searching person.
	/// </summary>
	public class PersonSearchResult
	{
		/// <summary>
		/// List of search results.
		/// </summary>
		[JsonProperty(PropertyName = "result")]
		public ICollection<PersonSearchEntry> Results { get; set; }

		/// <summary>
		/// Index of the last page.
		/// </summary>
		[JsonProperty(PropertyName = "result_last_page")]
		public int? ResultLastPage { get; set; }
	}
}