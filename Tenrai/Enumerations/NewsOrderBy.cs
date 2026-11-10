using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sort field for news.
/// </summary>
public enum NewsOrderBy
{
	/// <summary>
	/// Leave the order to the API (newest first).
	/// </summary>
	[Description("")]
	NoSorting,

	/// <summary>
	/// Publish date.
	/// </summary>
	[Description("published_at")]
	PublishedAt,

	/// <summary>
	/// Number of comments.
	/// </summary>
	[Description("comments")]
	Comments
}
