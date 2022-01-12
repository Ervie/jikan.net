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
		public ICollection<MalImageSubItem> Anime { get; set; }

		/// <summary>
		/// User's favorite manga.
		/// </summary>
		[JsonPropertyName("manga")]
		public ICollection<MalImageSubItem> Manga { get; set; }

		/// <summary>
		/// User's favorite characters.
		/// </summary>
		[JsonPropertyName("characters")]
		public ICollection<MalImageSubItem> Characters { get; set; }

		/// <summary>
		/// User's favorite people.
		/// </summary>
		[JsonPropertyName("people")]
		public ICollection<MalImageSubItem> People { get; set; }
	}
}