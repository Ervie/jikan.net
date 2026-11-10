using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// Extra information about user
/// </summary>
public class UserAbout
{
	/// <summary>
	/// User self description
	/// </summary>
	[JsonPropertyName("about")]
	public string About { get; set; }
}