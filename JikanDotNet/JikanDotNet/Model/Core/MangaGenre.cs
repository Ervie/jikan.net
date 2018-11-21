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
		/// Specific data about genre.
		/// </summary>
		[JsonProperty(PropertyName = "mal_url")]
		public MALSubItem MalUrl { get; set; }

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
	}
}