using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Producer model class.
	/// </summary>
	public class Producer
	{
		/// <summary>
		/// Metadata about producer.
		/// </summary>
		[JsonProperty(PropertyName = "meta")]
		public MALSubItem Metadata { get; set; }

		/// <summary>
		/// Name of the producer.
		/// </summary>
		[JsonProperty(PropertyName = "anime")]
		public ICollection<AnimeSubEntry> Anime { get; set; }
	}
}