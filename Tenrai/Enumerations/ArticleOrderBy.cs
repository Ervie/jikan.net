using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sort field for articles.
/// </summary>
public enum ArticleOrderBy
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
	/// View count.
	/// </summary>
	[Description("views")]
	Views
}
