using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sort field for manga chapter discussion threads.
/// </summary>
public enum MangaForumOrderBy
{
	/// <summary>
	/// Leave the order to the API.
	/// </summary>
	[Description("")]
	NoSorting,

	/// <summary>
	/// Chapter number.
	/// </summary>
	[Description("chapter_number")]
	ChapterNumber,

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
