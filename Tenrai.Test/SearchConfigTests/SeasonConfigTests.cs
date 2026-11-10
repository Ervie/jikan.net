using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class SeasonConfigTests
{
	[Fact]
	public void SeasonSearchConfig_TypesContinuingByScore_BuildsQuery()
	{
		var config = new SeasonSearchConfig
		{
			Types = { AnimeType.TV, AnimeType.ONA },
			IncludeContinuing = true,
			OrderBy = SeasonOrderBy.Score,
			SortDirection = SortDirection.Descending
		};

		config.ConfigToString().Should().Be("?filter=TV,ONA&continuing=true&order_by=score&sort=desc");
	}
}
