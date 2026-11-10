using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class SubListConfigTests
{
	[Fact]
	public void TopReviewsConfig_UserWithoutPreliminary_BuildsQuery()
	{
		var config = new TopReviewsConfig { Username = "x", Preliminary = false };

		config.ConfigToString().Should().Be("?username=x&preliminary=false");
	}

	[Fact]
	public void InterestStackListConfig_SpoilersOnlyByRestacks_BuildsQuery()
	{
		var config = new InterestStackListConfig
		{
			Spoilers = InclusionFilter.Only,
			OrderBy = InterestStackSearchOrderBy.RestackCount,
			SortDirection = SortDirection.Descending
		};

		config.ConfigToString().Should().Be("?spoilers=only&order_by=restack_count&sort=desc");
	}
}
