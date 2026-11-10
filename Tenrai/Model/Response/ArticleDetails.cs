using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Article with its full text and related entries.
/// </summary>
public class ArticleDetails : Article
{
	/// <summary>
	/// Full text as HTML and Markdown.
	/// </summary>
	[JsonPropertyName("content")]
	public ContentBody Content { get; set; }

	/// <summary>
	/// Anime, manga, people and characters the article is about.
	/// </summary>
	[JsonPropertyName("relations")]
	public ArticleRelations Relations { get; set; }
}
