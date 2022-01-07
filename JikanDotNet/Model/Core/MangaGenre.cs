using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Manga genre model class.
	/// </summary>
	public class MangaGenre: BaseJikanRequest
	{
		/// <summary>
		/// Metadata about genre.
		/// </summary>
		[JsonPropertyName("mal_url")]
		public MalUrl Metadata { get; set; }

		/// <summary>
		/// Total count of manga or manga with assigned
		/// </summary>
		[JsonPropertyName("item_count")]
		public int TotalCount { get; set; }

		/// <summary>
		/// List of manga with assigned genre.
		/// </summary>
		[JsonPropertyName("manga")]
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