using Tenrai.Helpers;

namespace Tenrai;

/// <summary>
/// Search over the full article catalogue: the <see cref="ArticleFilterConfig"/> filters plus search text and tag.
/// </summary>
public class ArticleSearchConfig : ArticleFilterConfig
{
	/// <summary>
	/// Search text.
	/// </summary>
	public string Query { get; set; }

	/// <summary>
	/// Only articles with this tag. <c>GetArticleTagsAsync</c> lists the tags.
	/// </summary>
	public string Tag { get; set; }

	private protected override void AddSearchTerms(QueryBuilder query)
	{
		query.Add("q", Query).Add("tag", Tag);
	}
}
