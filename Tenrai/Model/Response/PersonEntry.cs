using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Person listed as a voice actor, with their favorite count.
/// </summary>
public class PersonEntry : MalImageSubItem
{
	/// <summary>
	/// Number of users who favorited the person. Set on anime character lists, null on character voice lists.
	/// </summary>
	[JsonPropertyName("favorites")]
	public int? Favorites { get; set; }
}
