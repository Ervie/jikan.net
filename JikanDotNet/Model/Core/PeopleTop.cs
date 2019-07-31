using Newtonsoft.Json;
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
		[JsonProperty(PropertyName = "top")]
		public ICollection<PersonTopEntry> Top { get; set; }
	}
}