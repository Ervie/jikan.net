using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for people top.
	/// </summary>
	public class PeopleTop: BaseJikanRequest
	{
		/// <summary>
		/// Collection of people entries on top list.
		/// </summary>
		[JsonPropertyName("top")]
		public ICollection<PersonTopEntry> Top { get; set; }
	}
}