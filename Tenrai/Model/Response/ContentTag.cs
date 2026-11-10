using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Tag used on news items or articles, with how many carry it.
/// </summary>
public class ContentTag
{
	/// <summary>
	/// Tag name, as passed to <c>Tag</c> on a search config.
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; }

	/// <summary>
	/// Number of items with this tag.
	/// </summary>
	[JsonPropertyName("count")]
	public int Count { get; set; }
}
