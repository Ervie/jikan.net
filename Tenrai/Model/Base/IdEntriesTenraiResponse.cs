using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Response of the bulk MAL id endpoints when titles and types are asked for.
/// </summary>
public class IdEntriesTenraiResponse : BaseTenraiResponse<ICollection<IdEntry>>
{
	/// <summary>
	/// Number of entries in <see cref="BaseTenraiResponse{TResponse}.Data"/>.
	/// </summary>
	[JsonPropertyName("total")]
	public int Total { get; set; }
}
