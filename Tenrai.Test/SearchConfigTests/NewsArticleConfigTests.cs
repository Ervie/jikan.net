using System;
using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class NewsArticleConfigTests
{
	[Fact]
	public void NewsSearchConfig_AllFilters_BuildsQuery()
	{
		var config = new NewsSearchConfig
		{
			Query = "One Piece",
			Tag = "More Info",
			PublishedAfter = new DateTime(2025, 1, 1),
			OrderBy = NewsOrderBy.Comments,
			SortDirection = SortDirection.Descending
		};

		config.ConfigToString().Should().Be("?q=One%20Piece&tag=More%20Info&published_after=2025-01-01&order_by=comments&sort=desc");
	}

	[Fact]
	public void ArticleFilterConfig_OrderByViews_BuildsQuery()
	{
		var config = new ArticleFilterConfig { OrderBy = ArticleOrderBy.Views, Limit = 50 };

		config.ConfigToString().Should().Be("?limit=50&order_by=views&sort=asc");
	}
}
