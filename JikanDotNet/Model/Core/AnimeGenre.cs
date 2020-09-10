using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Studio model class.
	/// </summary>
	public class AnimeGenre: BaseJikanRequest
	{
		/// <summary>
		/// Metadata about genre.
		/// </summary>
		[JsonPropertyName("mal_url")]
		public MALSubItem Metadata { get; set; }

		/// <summary>
		/// Total count of anime with assigned
		/// </summary>
		[JsonPropertyName("item_count")]
		public int TotalCount { get; set; }

		/// <summary>
		/// List of anime with assigned genre.
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