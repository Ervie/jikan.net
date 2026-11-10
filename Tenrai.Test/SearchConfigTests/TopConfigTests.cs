using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class TopConfigTests
{
	[Fact]
	public void TopVideosConfig_MusicFromYearByLikes_BuildsQuery()
	{
		var config = new TopVideosConfig
		{
			Type = TopVideoType.Music,
			MinimumYear = 2020,
			OrderBy = TopVideosOrderBy.Likes,
			SortDirection = SortDirection.Descending
		};

		config.ConfigToString().Should().Be("?type=music&min_year=2020&order_by=likes&sort=desc");
	}

	[Fact]
	public void TopEpisodesConfig_OrderByVotes_BuildsQuery()
	{
		var config = new TopEpisodesConfig { OrderBy = TopEpisodesOrderBy.Votes };

		config.ConfigToString().Should().Be("?order_by=votes");
	}

	[Fact]
	public void AnimeTopSearchConfig_TypesReversed_BuildsQuery()
	{
		var config = new AnimeTopSearchConfig { Types = { AnimeType.TV, AnimeType.Movie }, SortDirection = SortDirection.Descending };

		config.ConfigToString().Should().Be("?type=TV,Movie&sort=desc");
	}
}
