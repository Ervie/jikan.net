using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Which adaptations count for the adapted manga list.
/// </summary>
public enum AdaptedMangaFilter
{
	/// <summary>
	/// Manga with any anime adaptation.
	/// </summary>
	[Description("all")]
	All,

	/// <summary>
	/// Manga whose adaptation is airing now.
	/// </summary>
	[Description("airing")]
	Airing,

	/// <summary>
	/// Manga whose adaptation has not aired yet.
	/// </summary>
	[Description("upcoming")]
	Upcoming
}
