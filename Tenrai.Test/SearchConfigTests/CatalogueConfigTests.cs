using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class CatalogueConfigTests
{
	[Fact]
	public void DeletedSearchConfig_OrderByMembers_BuildsQuery()
	{
		var config = new DeletedSearchConfig { OrderBy = DeletedOrderBy.Members, SortDirection = SortDirection.Descending };

		config.ConfigToString().Should().Be("?order_by=members&sort=desc");
	}

	[Fact]
	public void AdaptedMangaSearchConfig_AiringTypesScore_BuildsQuery()
	{
		var config = new AdaptedMangaSearchConfig
		{
			Filter = AdaptedMangaFilter.Airing,
			Types = new List<MangaType> { MangaType.Manga, MangaType.Manhwa },
			MinimumScore = 8
		};

		config.ConfigToString().Should().Be("?filter=airing&type=manga,manhwa&min_score=8");
	}

	[Fact]
	public void StatisticsHistoryConfig_StartDateAscending_BuildsQuery()
	{
		var config = new StatisticsHistoryConfig { StartDate = new DateTime(2026, 4, 1), SortDirection = SortDirection.Ascending };

		config.ConfigToString().Should().Be("?start_date=2026-04-01&sort=asc");
	}
}
