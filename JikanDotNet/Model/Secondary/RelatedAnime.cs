using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Model class representing collection of related anime entries.
	/// </summary>
	public class RelatedAnime
	{
		/// <summary>
		/// Type of relation, e.g. "Adaptation" or "Side Story".
		/// </summary>
		[JsonPropertyName("relation")]
		public string Relation { get; set; }

		/// <summary>
		/// Collection of related anime of given relation type.
		/// </summary>
		[JsonPropertyName("items")]
		public ICollection<MALSubItem> Items { get; set; }
	}
}