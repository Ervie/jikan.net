using System.Text.Json.Serialization;

namespace Tenrai;

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

	/// <summary>
	/// Number of users who favorited the character.
	/// </summary>
	[JsonPropertyName("favorites")]
	public int Favorites { get; set; }
}