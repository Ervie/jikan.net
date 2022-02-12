using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Club related anime, manga nad characters.
	/// </summary>
	public class ClubRelations
	{
		/// <summary>
		/// Club's anime relations.
		/// </summary>
		[JsonPropertyName("anime")]
		public ICollection<MalUrl> Anime { get; set; }

		/// <summary>
		/// Club's manga relations.
		/// </summary>
		[JsonPropertyName("manga")]
		public ICollection<MalUrl> Manga { get; set; }

		/// <summary>
		/// Club's character relations.
		/// </summary>
		[JsonPropertyName("characters")]
		public ICollection<MalUrl> Characters { get; set; }
	}
}