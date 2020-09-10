using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// User's history model class.
	/// </summary>
	public class UserHistory: BaseJikanRequest
	{
		/// <summary>
		/// Collection of user's history updates.
		/// </summary>
		[JsonPropertyName("history")]
		public ICollection<HistoryEntry> History { get; set; }
	}
}