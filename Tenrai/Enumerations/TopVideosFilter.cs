using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Airing state of the anime behind a video in the top videos list.
/// </summary>
public enum TopVideosFilter
{
	/// <summary>
	/// Videos of any anime.
	/// </summary>
	[Description("all")]
	All,

	/// <summary>
	/// Videos of anime airing now.
	/// </summary>
	[Description("airing")]
	Airing,

	/// <summary>
	/// Videos of anime that have not aired yet.
	/// </summary>
	[Description("upcoming")]
	Upcoming,

	/// <summary>
	/// Videos of anime that finished airing.
	/// </summary>
	[Description("finished")]
	Finished
}
