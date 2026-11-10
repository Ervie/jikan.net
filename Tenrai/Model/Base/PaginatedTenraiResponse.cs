using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Base wrapping class for response with paginated data
/// </summary>
public class PaginatedTenraiResponse<TResponse> : BaseTenraiResponse<TResponse>
{
	/// <summary>
	/// Pagination
	/// </summary>
	[JsonPropertyName("pagination")]
	public Pagination Pagination { get; set; }

	/// <summary>
	/// Parameterless constructor, required for serialization
	/// </summary>
	public PaginatedTenraiResponse()
	{ }
}