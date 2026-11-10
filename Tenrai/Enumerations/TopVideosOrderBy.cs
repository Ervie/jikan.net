using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sort field for the top videos list.
/// </summary>
public enum TopVideosOrderBy
{
	/// <summary>
	/// Leave the order to the API (by views).
	/// </summary>
	[Description("")]
	NoSorting,

	/// <summary>
	/// YouTube view count.
	/// </summary>
	[Description("views")]
	Views,

	/// <summary>
	/// YouTube like count.
	/// </summary>
	[Description("likes")]
	Likes,

	/// <summary>
	/// YouTube dislike count.
	/// </summary>
	[Description("dislikes")]
	Dislikes,

	/// <summary>
	/// YouTube comment count.
	/// </summary>
	[Description("comments")]
	Comments,

	/// <summary>
	/// Video title.
	/// </summary>
	[Description("title")]
	Title,

	/// <summary>
	/// When the video was published on YouTube.
	/// </summary>
	[Description("published_at")]
	PublishedAt
}
