using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sort field for anime episode discussion threads.
/// </summary>
public enum AnimeForumOrderBy
{
	/// <summary>
	/// Leave the order to the API.
	/// </summary>
	[Description("")]
	NoSorting,

	/// <summary>
	/// Episode number.
	/// </summary>
	[Description("episode_number")]
	EpisodeNumber,

	/// <summary>
	/// Thread creation date.
	/// </summary>
	[Description("created_on")]
	CreatedOn,

	/// <summary>
	/// Number of comments.
	/// </summary>
	[Description("comment_count")]
	CommentCount,

	/// <summary>
	/// Date of the last post.
	/// </summary>
	[Description("last_post")]
	LastPost
}
