using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Extra information stored in "more info" tab.
/// </summary>
public class MoreInfo
{
	/// <summary>
	/// Extra information stored in "more info" tab.
	/// </summary>
	[JsonPropertyName("moreinfo")]
	public string Info { get; set; }
}