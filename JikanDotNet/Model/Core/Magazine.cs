using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Magazine model class.
	/// </summary>
	public class Magazine : BaseJikanRequest
	{
		/// <summary>
		/// Metadata about magazine..
		/// </summary>
		[JsonPropertyName("meta")]
		public MalUrl Metadata { get; set; }

		/// <summary>
		/// List of manga published in magazine.
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