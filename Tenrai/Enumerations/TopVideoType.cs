using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Kind of video in the top videos list.
/// </summary>
public enum TopVideoType
{
	/// <summary>
	/// Promotional videos and trailers.
	/// </summary>
	[Description("promo")]
	Promo,

	/// <summary>
	/// Opening and ending music videos.
	/// </summary>
	[Description("music")]
	Music
}
