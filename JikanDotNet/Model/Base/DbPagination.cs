using System.Text.Json.Serialization;

namespace JikanDotNet;

/// <summary>
/// Extended pagination model
/// </summary>
public class DbPagination: Pagination
{
    /// <summary>
    /// Current page
    /// </summary>
    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }
    
    /// <summary>
    /// Summary about current query
    /// </summary>
    [JsonPropertyName("items")]
    public PaginationSummary Items { get; set; }
}