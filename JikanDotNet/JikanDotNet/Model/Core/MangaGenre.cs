using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Manga genre model class.
	/// </summary>
	public class MangaGenre
	{
		/// <summary>
		/// Metadata about genre.
		/// </summary>
		[JsonProperty(PropertyName = "mal_url")]
		public MALSubItem Metadata { get; set; }

		/// <summary>
		/// Total count of manga or manga with assigned
		/// </summary>
		[JsonProperty(PropertyName = "item_count")]
		public int TotalCount { get; set; }

		/// <summary>
		/// List of manga with assigned genre.
		/// </summary>
		[JsonProperty(PropertyName = "manga")]
		public ICollection<MangaSubEntry> Manga { get; set; }

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