using System.Text.Json.Serialization;

namespace JikanDotNet;

/// <summary>
/// Base wrapping class for response with paginated data
/// </summary>
public class DbPaginatedJikanResponse<TResponse> : PaginatedJikanResponse<TResponse>
{
    /// <summary>
    /// Pagination
    /// </summary>
    [JsonPropertyName("pagination")]
    public new DbPagination Pagination { get; set; }

    /// <summary>
    /// Parameterless constructor, required for serialization
    /// </summary>
    public DbPaginatedJikanResponse()
    { }
}