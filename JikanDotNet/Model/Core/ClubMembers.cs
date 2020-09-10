using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for club members collection.
	/// </summary>
	public class ClubMembers: BaseJikanRequest
	{
		/// <summary>
		/// Collection of club members.
		/// </summary>
		[JsonPropertyName("members")]
		public ICollection<ClubMember> Members { get; set; }
	}
}