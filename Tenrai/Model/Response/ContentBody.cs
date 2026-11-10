using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Full text of a news item or article in two formats.
/// </summary>
public class ContentBody
{
	/// <summary>
	/// Text as HTML.
	/// </summary>
	[JsonPropertyName("html")]
	public string Html { get; set; }

	/// <summary>
	/// Text as Markdown.
	/// </summary>
	[JsonPropertyName("markdown")]
	public string Markdown { get; set; }
}
