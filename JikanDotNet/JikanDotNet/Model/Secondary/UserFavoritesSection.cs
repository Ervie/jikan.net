using Newtonsoft.Json;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// User favorites section model class.
	/// </summary>
	public class UserFavoritesSection
	{
		/// <summary>
		/// User's favorite anime.
		/// </summary>
		[JsonProperty(PropertyName = "anime")]
		public ICollection<MALImageSubItem> Anime { get; set; }

		/// <summary>
		/// User's favorite manga.
		/// </summary>
		[JsonProperty(PropertyName = "manga")]
		public ICollection<MALImageSubItem> Manga { get; set; }

		/// <summary>
		/// User's favorite characters.
		/// </summary>
		[JsonProperty(PropertyName = "characters")]
		public ICollection<MALImageSubItem> Characters { get; set; }

		/// <summary>
		/// User's favorite people.
		/// </summary>
		[JsonProperty(PropertyName = "people")]
		public ICollection<MALImageSubItem> People { get; set; }
	}
}