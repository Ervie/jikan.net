using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for club members collection.
	/// </summary>
	public class ClubMembers
	{
		/// <summary>
		/// Collection of club members.
		/// </summary>
		[JsonProperty(PropertyName = "members")]
		public ICollection<ClubMember> Members { get; set; }
	}
}