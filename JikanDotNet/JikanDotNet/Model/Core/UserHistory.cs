using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// User's history model class.
	/// </summary>
	public class UserHistory
	{
		/// <summary>
		/// Collection of user's history updates.
		/// </summary>
		[JsonProperty(PropertyName = "history")]
		public ICollection<HistoryEntry> History { get; set; }
	}
}