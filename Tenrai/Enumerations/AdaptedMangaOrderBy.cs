using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sort field for the adapted manga list.
/// </summary>
public enum AdaptedMangaOrderBy
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
	/// Title.
	/// </summary>
	[Description("title")]
	Title,

	/// <summary>
	/// Publishing start date.
	/// </summary>
	[Description("start_date")]
	StartDate,

	/// <summary>
	/// Publishing end date.
	/// </summary>
	[Description("end_date")]
	EndDate,

	/// <summary>
	/// Number of chapters.
	/// </summary>
	[Description("chapters")]
	Chapters,

	/// <summary>
	/// Number of volumes.
	/// </summary>
	[Description("volumes")]
	Volumes,

	/// <summary>
	/// Score.
	/// </summary>
	[Description("score")]
	Score,

	/// <summary>
	/// Number of users who scored.
	/// </summary>
	[Description("scored_by")]
	ScoredBy,

	/// <summary>
	/// Rank.
	/// </summary>
	[Description("rank")]
	Rank,

	/// <summary>
	/// Popularity.
	/// </summary>
	[Description("popularity")]
	Popularity,

	/// <summary>
	/// Number of members.
	/// </summary>
	[Description("members")]
	Members,

	/// <summary>
	/// Number of favorites.
	/// </summary>
	[Description("favorites")]
	Favorites
}
