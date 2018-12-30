using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Magazine model class.
	/// </summary>
	public class Magazine : IMalEntity
	{
		/// <summary>
		/// Metadata about magazine..
		/// </summary>
		[JsonProperty(PropertyName = "meta")]
		public MALSubItem Metadata { get; set; }

		/// <summary>
		/// List of manga published in magazine.
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