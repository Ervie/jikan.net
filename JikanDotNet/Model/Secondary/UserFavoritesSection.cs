using System.Text.Json.Serialization;
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
		[JsonPropertyName("anime")]
		public ICollection<MALImageSubItem> Anime { get; set; }

		/// <summary>
		/// User's favorite manga.
		/// </summary>
		[JsonPropertyName("manga")]
		public ICollection<MALImageSubItem> Manga { get; set; }

		/// <summary>
		/// User's favorite characters.
		/// </summary>
		[JsonPropertyName("characters")]
		public ICollection<MALImageSubItem> Characters { get; set; }

		/// <summary>
		/// User's favorite people.
		/// </summary>
		[JsonPropertyName("people")]
		public ICollection<MALImageSubItem> People { get; set; }
	}
}