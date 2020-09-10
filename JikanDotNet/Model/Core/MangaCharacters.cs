using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace JikanDotNet
{
	/// <summary>
	/// Manga characters list model class.
	/// </summary>
	public class MangaCharacters: BaseJikanRequest
	{
		/// <summary>
		/// Collection of manga's characters.
		/// </summary>
		[JsonPropertyName("characters")]
		public ICollection<CharacterEntry> Characters { get; set; }
	}
}