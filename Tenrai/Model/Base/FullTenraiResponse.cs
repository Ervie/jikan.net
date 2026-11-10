using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Response of a full-data request that may ask for more ids than the one in the path.
/// </summary>
public class FullTenraiResponse<TResponse> : BaseTenraiResponse<TResponse>
{
	/// <summary>
	/// The extra entries asked for in <see cref="FullDataConfig.Ids"/>, in the requested order. Null when no extra ids were sent.
	/// </summary>
	[JsonPropertyName("included")]
	public ICollection<TResponse> Included { get; set; }

	/// <summary>
	/// Extra ids not found in the searched catalogue or archive. Null when no extra ids were sent.
	/// </summary>
	[JsonPropertyName("missing_ids")]
	public ICollection<long> MissingIds { get; set; }
}
