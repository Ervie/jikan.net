using Tenrai.Helpers;

namespace Tenrai;

/// <summary>
/// Search over the full news catalogue: the <see cref="NewsFilterConfig"/> filters plus search text and tag.
/// </summary>
public class NewsSearchConfig : NewsFilterConfig
{
	/// <summary>
	/// Search text.
	/// </summary>
	public string Query { get; set; }

	/// <summary>
	/// Only news with this tag. <c>GetNewsTagsAsync</c> lists the tags.
	/// </summary>
	public string Tag { get; set; }

	private protected override void AddSearchTerms(QueryBuilder query)
	{
		query.Add("q", Query).Add("tag", Tag);
	}
}
