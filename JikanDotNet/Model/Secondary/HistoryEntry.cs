using Newtonsoft.Json;
using System;

namespace JikanDotNet
{
	/// <summary>
	/// Model class for entry on user's history (single update).
	/// </summary>
	public class HistoryEntry
	{
		/// <summary>
		/// Metadata about updated manga/anime.
		/// </summary>
		[JsonProperty(PropertyName = "meta")]
		public MALSubItem Metadata { get; set; }

		/// <summary>
		/// New value for watched episodes/read chapters.
		/// </summary>
		[JsonProperty(PropertyName = "increment")]
		public int Increment { get; set; }

		/// <summary>
		/// Date of the update.
		/// </summary>
		[JsonProperty(PropertyName = "date")]
		public DateTime? Date { get; set; }
	}
}