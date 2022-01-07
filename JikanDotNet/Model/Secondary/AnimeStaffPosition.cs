using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for anime staff position (person request).
	/// </summary>
	public class AnimeStaffPosition
	{
		/// <summary>
		/// Person details.
		/// </summary>
		[JsonPropertyName("person")]
		public PersonEntry Person { get; set; }

		/// <summary>
		/// Positions associated with staff member.
		/// </summary>
		[JsonPropertyName("positions")]
		public ICollection<string> Position { get; set; }
	}
}