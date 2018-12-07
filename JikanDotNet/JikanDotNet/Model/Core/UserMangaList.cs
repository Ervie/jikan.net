using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// User's manga list model class.
	/// </summary>
	public class UserMangaList
	{
		/// <summary>
		/// Collection of user's manga on their manga list.
		/// </summary>
		[JsonProperty(PropertyName = "manga")]
		public ICollection<MangaListEntry> Manga { get; set; }
	}
}