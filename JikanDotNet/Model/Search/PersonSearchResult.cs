using System.Text.Json.Serialization;
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
		[JsonPropertyName("results")]
		public ICollection<PersonSearchEntry> Results { get; set; }

		/// <summary>
		/// Index of the last page.
		/// </summary>
		[JsonPropertyName("last_page")]
		public int? ResultLastPage { get; set; }
	}
}