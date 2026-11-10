using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Defines the sort order used when querying reviews.
/// </summary>
public enum ReviewSortOrder
{
	/// <summary>
	/// Sort by the most helpful reviews first (API default).
	/// </summary>
	[Description("most_helpful")]
	MostHelpful,

	/// <summary>
	/// Sort by the newest reviews first.
	/// </summary>
	[Description("newest")]
	Newest,

	/// <summary>
	/// Sort by the oldest reviews first.
	/// </summary>
	[Description("oldest")]
	Oldest
}
