using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sort field for the producer listing.
/// </summary>
public enum ProducerSearchOrderBy
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
	/// Number of anime the company is credited in; with roles set, only anime in those jobs.
	/// </summary>
	[Description("count")]
	Count,

	/// <summary>
	/// Number of favorites.
	/// </summary>
	[Description("favorites")]
	Favorites,

	/// <summary>
	/// Founding date. Companies without one come last.
	/// </summary>
	[Description("established")]
	Established,

	/// <summary>
	/// Start of the earliest credited anime.
	/// </summary>
	[Description("debut")]
	Debut,

	/// <summary>
	/// Start of the latest credited anime.
	/// </summary>
	[Description("last_credit")]
	LastCredit
}
