using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// User's manga list model class.
	/// </summary>
	public class UserMangaList: BaseJikanRequest
	{
		/// <summary>
		/// Collection of user's manga on their manga list.
		/// </summary>
		[JsonPropertyName("manga")]
		public ICollection<MangaListEntry> Manga { get; set; }
	}
}