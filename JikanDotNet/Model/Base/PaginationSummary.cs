using System.Text.Json.Serialization;

namespace JikanDotNet;

/// <summary>
/// Pagination sumamry model (about current query)
/// </summary>
public class PaginationSummary
{
    /// <summary>
    /// Count of items returned
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
    
    /// <summary>
    /// Total items count.
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }
    
    /// <summary>
    /// Count of items in the single page
    /// </summary>
    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }
}