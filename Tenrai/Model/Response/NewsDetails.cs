using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// News item with its full text and related entries.
/// </summary>
public class NewsDetails : News
{
	/// <summary>
	/// Full text as HTML and Markdown.
	/// </summary>
	[JsonPropertyName("content")]
	public ContentBody Content { get; set; }

	/// <summary>
	/// Anime and manga the news is about.
	/// </summary>
	[JsonPropertyName("relations")]
	public NewsRelations Relations { get; set; }
}
