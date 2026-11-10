using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// One entry of the bulk ids with its title and type.
/// </summary>
public class IdEntry
{
	/// <summary>
	/// MyAnimeList id.
	/// </summary>
	[JsonPropertyName("mal_id")]
	public long MalId { get; set; }

	/// <summary>
	/// Title.
	/// </summary>
	[JsonPropertyName("title")]
	public string Title { get; set; }

	/// <summary>
	/// Type of the entry (e.g. "TV", "Manga").
	/// </summary>
	[JsonPropertyName("type")]
	public string Type { get; set; }
}
