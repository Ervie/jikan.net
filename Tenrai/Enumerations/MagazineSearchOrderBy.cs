using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sort field for the magazine listing.
/// </summary>
public enum MagazineSearchOrderBy
{
	/// <summary>
	/// Leave the order to the API.
	/// </summary>
	[Description("")]
	NoSorting,

	/// <summary>
	/// MyAnimeList id.
	/// </summary>
	[Description("mal_id")]
	MalId,

	/// <summary>
	/// Name.
	/// </summary>
	[Description("name")]
	Name,

	/// <summary>
	/// Number of manga in the magazine.
	/// </summary>
	[Description("count")]
	Count
}
