using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class ForumSearchConfigTests
{
	[Fact]
	public void AnimeForumSearchConfig_OrderBy_SendsEpisodeNumber()
	{
		var config = new AnimeForumSearchConfig { Page = 2, OrderBy = AnimeForumOrderBy.EpisodeNumber, SortDirection = SortDirection.Descending };

		config.ConfigToString().Should().Be("?page=2&order_by=episode_number&sort=desc");
	}

	[Fact]
	public void MangaForumSearchConfig_OrderBy_SendsChapterNumber()
	{
		var config = new MangaForumSearchConfig { OrderBy = MangaForumOrderBy.ChapterNumber, SortDirection = SortDirection.Descending };

		config.ConfigToString().Should().Be("?order_by=chapter_number&sort=desc");
	}
}
