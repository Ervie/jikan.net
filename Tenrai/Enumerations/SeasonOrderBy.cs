using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sort field for the season lists.
/// </summary>
public enum SeasonOrderBy
{
	/// <summary>
	/// Leave the order to the API (members, descending).
	/// </summary>
	[Description("")]
	NoSorting,

	/// <summary>
	/// Score.
	/// </summary>
	[Description("score")]
	Score,

	/// <summary>
	/// Number of members.
	/// </summary>
	[Description("members")]
	Members,

	/// <summary>
	/// Start date.
	/// </summary>
	[Description("start_date")]
	StartDate,

	/// <summary>
	/// MyAnimeList id.
	/// </summary>
	[Description("mal_id")]
	MalId,

	/// <summary>
	/// Number of episodes.
	/// </summary>
	[Description("episodes")]
	Episodes,

	/// <summary>
	/// Number of favorites.
	/// </summary>
	[Description("favorites")]
	Favorites,

	/// <summary>
	/// End date.
	/// </summary>
	[Description("end_date")]
	EndDate,

	/// <summary>
	/// Number of users who scored it.
	/// </summary>
	[Description("scored_by")]
	ScoredBy,

	/// <summary>
	/// Title.
	/// </summary>
	[Description("title")]
	Title
}
