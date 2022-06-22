using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet;

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
}