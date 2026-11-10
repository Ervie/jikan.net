using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Anime with full data model class.
/// </summary>
public class AnimeFull: Anime
{
	/// <summary>
	/// Anime related entries.
	/// </summary>
	[JsonPropertyName("relations")]
	public ICollection<RelatedEntry> Relations { get; set; }

	/// <summary>
	/// Anime music themes (openings and endings).
	/// </summary>
	[JsonPropertyName("theme")]
	public AnimeThemes MusicThemes { get; set; }

	/// <summary>
	/// Anime external links.
	/// </summary>
	[JsonPropertyName("external")]
	public ICollection<ExternalLink> ExternalLinks { get; set; }

	/// <summary>
	/// Anime streaming links.
	/// </summary>
	[JsonPropertyName("streaming")]
	public ICollection<ExternalLink> StreamingLinks { get; set; }

	/// <summary>
	/// Free-text extra information, e.g. a suggested viewing order.
	/// </summary>
	[JsonPropertyName("moreinfo")]
	public string MoreInfo { get; set; }
}