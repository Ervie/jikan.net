using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Countries where a YouTube video is allowed or blocked.
/// </summary>
public class RegionRestriction
{
	/// <summary>
	/// Country codes of the only countries where the video plays.
	/// </summary>
	[JsonPropertyName("allowed")]
	public ICollection<string> Allowed { get; set; }

	/// <summary>
	/// Country codes of countries where the video is blocked.
	/// </summary>
	[JsonPropertyName("blocked")]
	public ICollection<string> Blocked { get; set; }
}
