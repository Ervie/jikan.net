using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Producer model class.
	/// </summary>
	public class Producer: BaseJikanRequest
	{
		/// <summary>
		/// Metadata about producer.
		/// </summary>
		[JsonPropertyName("meta")]
		public MALSubItem Metadata { get; set; }

		/// <summary>
		/// List of anime published by producer.
		/// </summary>
		[JsonPropertyName("anime")]
		public ICollection<AnimeSubEntry> Anime { get; set; }

		/// <summary>
		/// ID associated with MyAnimeList.
		/// </summary>
		public long MalId
		{
			get
			{
				return Metadata.MalId;
			}
			set
			{
				Metadata.MalId = value;
			}
		}
	}
}