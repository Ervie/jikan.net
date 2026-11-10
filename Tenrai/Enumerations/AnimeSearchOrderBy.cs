using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Properties by which anime search results can be ordered.
/// </summary>
public enum AnimeSearchOrderBy
{
	/// <summary>
	/// Does not order results.
	/// </summary>
	[Description("")]
	NoSorting,

	/// <summary>
	/// Orders results by title.
	/// </summary>
	[Description("title")]
	Title,

	/// <summary>
	/// Orders results by score.
	/// </summary>
	[Description("score")]
	Score,


	/// <summary>
	/// Orders results by count of scores.
	/// </summary>
	[Description("scored_by")]
	ScoredBy,

	/// <summary>
	/// Orders results by members.
	/// </summary>
	[Description("members")]
	Members,

	/// <summary>
	/// Orders results by favorites.
	/// </summary>
	[Description("favorites")]
	Favorites,

	/// <summary>
	/// Orders results by MalId.
	/// </summary>
	[Description("mal_id")]
	Id,

	/// <summary>
	/// Orders results by number of episodes.
	/// </summary>
	[Description("episodes")]
	Episodes,


	/// <summary>
	/// Orders results by start date.
	/// </summary>
	[Description("start_date")]
	StartDate,

	/// <summary>
	/// Orders results by end date.
	/// </summary>
	[Description("end_date")]
	EndDate,

	/// <summary>
	/// Orders results by rank.
	/// </summary>
	[Description("rank")]
	Rank,

	/// <summary>
	/// Orders results by popularity.
	/// </summary>
	[Description("popularity")]
	Popularity,

	/// <summary>
	/// Orders results by English title.
	/// </summary>
	[Description("title_english")]
	TitleEnglish,

	/// <summary>
	/// Orders results by Japanese title.
	/// </summary>
	[Description("title_japanese")]
	TitleJapanese,

	/// <summary>
	/// Orders results by when the anime was added to MyAnimeList.
	/// </summary>
	[Description("added")]
	Added
}