using System.Text.Json.Serialization;

namespace JikanDotNet
{
	/// <summary>
	/// Manga characters list model class.
	/// </summary>
	public class MangaCharacter
	{
		/// <summary>
		/// Character details
		/// </summary>
		[JsonPropertyName("character")]
		public CharacterEntry Character { get; set; }

		/// <summary>
		/// Character's role (e. g. "main", "supporting")
		/// </summary>
		[JsonPropertyName("role")]
		public string Role { get; set; }
	}
}