using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Archive a full-data request searches instead of the regular catalogue.
/// </summary>
public enum FullDataMode
{
	/// <summary>
	/// Entries MyAnimeList denied.
	/// </summary>
	[Description("denied")]
	Denied,

	/// <summary>
	/// Entries deleted from MyAnimeList.
	/// </summary>
	[Description("deleted")]
	Deleted
}
