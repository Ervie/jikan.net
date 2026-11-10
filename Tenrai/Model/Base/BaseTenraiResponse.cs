using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Base wrapping class for response with data
/// </summary>
public class BaseTenraiResponse<TResponse>
{
	/// <summary>
	/// Data of the request.
	/// </summary>
	[JsonPropertyName("data")]
	public TResponse Data { get; set; }

	/// <summary>
	/// Parametereless constructor, required for serialization
	/// </summary>
	public BaseTenraiResponse() {}
}