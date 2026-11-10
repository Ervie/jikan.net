using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sort field for the top episodes list. The API always sorts descending.
/// </summary>
public enum TopEpisodesOrderBy
{
	/// <summary>
	/// Leave the order to the API. Its ranking is not raw score: a 4.9 with 4341 votes ranks above a 4.97 with 260.
	/// </summary>
	[Description("")]
	NoSorting,

	/// <summary>
	/// Episode score.
	/// </summary>
	[Description("score")]
	Score,

	/// <summary>
	/// Number of votes on the episode.
	/// </summary>
	[Description("votes")]
	Votes,

	/// <summary>
	/// Number of replies in the episode discussion thread.
	/// </summary>
	[Description("comments")]
	Comments
}
