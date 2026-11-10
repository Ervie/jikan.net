using System;
using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class IdsConfigTests
{
	[Fact]
	public void AnimeIdsConfig_UpcomingMoviesAndOvasUpdatedSince_BuildsQuery()
	{
		var config = new AnimeIdsConfig
		{
			Types = { AnimeType.Movie, AnimeType.OVA },
			Status = AiringStatus.Upcoming,
			UpdatedAfter = new DateTime(2026, 11, 1)
		};

		config.ConfigToString().Should().Be("?type=Movie,OVA&status=upcoming&updated_at=2026-11-01");
	}

	[Fact]
	public void MangaIdsConfig_LightNovelsOnHiatus_BuildsQuery()
	{
		var config = new MangaIdsConfig
		{
			Types = { MangaType.LightNovel },
			Status = PublishingStatus.Hiatus
		};

		config.ConfigToString().Should().Be("?type=lightnovel&status=hiatus");
	}
}
