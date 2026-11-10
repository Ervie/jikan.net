using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Response of the bulk MAL id endpoints.
/// </summary>
public class IdsTenraiResponse : BaseTenraiResponse<ICollection<long>>
{
	/// <summary>
	/// Number of ids in <see cref="BaseTenraiResponse{TResponse}.Data"/>.
	/// </summary>
	[JsonPropertyName("total")]
	public int Total { get; set; }
}
