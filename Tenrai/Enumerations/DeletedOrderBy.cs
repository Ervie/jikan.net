using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sort field for the deleted anime and manga lists.
/// </summary>
public enum DeletedOrderBy
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
	/// Score.
	/// </summary>
	[Description("score")]
	Score,

	/// <summary>
	/// Number of members.
	/// </summary>
	[Description("members")]
	Members
}
