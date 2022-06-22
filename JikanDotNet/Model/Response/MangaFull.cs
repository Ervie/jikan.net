using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JikanDotNet;

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
}