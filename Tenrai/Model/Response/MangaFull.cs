using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Manga with full data model class.
/// </summary>
public class MangaFull: Manga
{
	/// <summary>
	/// Manga related entries.
	/// </summary>
	[JsonPropertyName("relations")]
	public ICollection<RelatedEntry> Relations { get; set; }

	/// <summary>
	/// Manga external links.
	/// </summary>
	[JsonPropertyName("external")]
	public ICollection<ExternalLink> ExternalLinks { get; set; }

	/// <summary>
	/// Free-text extra information, e.g. related works or a suggested reading order.
	/// </summary>
	[JsonPropertyName("moreinfo")]
	public string MoreInfo { get; set; }
}