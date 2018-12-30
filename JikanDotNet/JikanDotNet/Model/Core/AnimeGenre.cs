using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Studio model class.
	/// </summary>
	public class AnimeGenre
	{
		/// <summary>
		/// Metadata about genre.
		/// </summary>
		[JsonProperty(PropertyName = "mal_url")]
		public MALSubItem Metadata { get; set; }

		/// <summary>
		/// Total count of anime with assigned
		/// </summary>
		[JsonProperty(PropertyName = "item_count")]
		public int TotalCount { get; set; }

		/// <summary>
		/// List of anime with assigned genre.
		/// </summary>
		[JsonProperty(PropertyName = "anime")]
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