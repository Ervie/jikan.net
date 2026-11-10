using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Current status of anime or (search config).
/// </summary>
public enum AiringStatus
{
	/// <summary>
	/// Airing status.
	/// </summary>
	[Description("airing")]
	Airing,

	/// <summary>
	/// Complete status.
	/// </summary>
	[Description("complete")]
	Complete,

	/// <summary>
	/// Upcoming status.
	/// </summary>
	[Description("upcoming")]
	Upcoming
}