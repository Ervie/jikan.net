using System.Text.Json.Serialization;
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
		[JsonPropertyName("meta")]
		public MALSubItem Metadata { get; set; }

		/// <summary>
		/// New value for watched episodes/read chapters.
		/// </summary>
		[JsonPropertyName("increment")]
		public int Increment { get; set; }

		/// <summary>
		/// Date of the update.
		/// </summary>
		[JsonPropertyName("date")]
		public DateTime? Date { get; set; }
	}
}