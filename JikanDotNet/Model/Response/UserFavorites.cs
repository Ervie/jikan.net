using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Model representing user favorites
	/// </summary>
	public class UserFavorites
	{
		/// <summary>
		/// User's favorite anime.
		/// </summary>
		[JsonPropertyName("anime")]
		public ICollection<FavoritesEntry> Anime { get; set; }

		/// <summary>
		/// User's favorite manga.
		/// </summary>
		[JsonPropertyName("manga")]
		public ICollection<FavoritesEntry> Manga { get; set; }

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