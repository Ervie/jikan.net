using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing collection of related anime entries.
	/// </summary>
	public class RelatedEntry
	{
		/// <summary>
		/// Type of relation, e.g. "Adaptation" or "Side Story".
		/// </summary>
		[JsonPropertyName("relation")]
		public string Relation { get; set; }

		/// <summary>
		/// Collection of related anime/manga of given relation type.
		/// </summary>
		[JsonPropertyName("entry")]
		public ICollection<MalUrl> Entry { get; set; }
	}
}