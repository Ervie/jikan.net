using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.SeasonTests;

[Collection("TenraiTests")]
public class GetSeasonArchiveAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetSeasonArchiveAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Fact]
	public async Task GetSeasonArchiveAsync_NoParameter_ShouldParseFirstQueryableYear()
	{
		// When
		var seasonArchives = await _tenrai.GetSeasonArchiveAsync();

		// Then
		var oldestSeason = seasonArchives.Data.Last();
		using (new AssertionScope())
		{
			oldestSeason.Year.Should().Be(1917);
			oldestSeason.Season.Should().HaveCount(4);
		}
	}

	[Fact]
	public async Task GetSeasonArchiveAsync_NoParameter_ShouldParseLatestQueryableYear()
	{
		// When
		var seasonArchives = await _tenrai.GetSeasonArchiveAsync();

		// Then
		using (new AssertionScope())
		{
			seasonArchives.Data.First().Year.Should().BeGreaterOrEqualTo(DateTime.UtcNow.Year);
			seasonArchives.Data.Last().Season.Should().HaveCountGreaterOrEqualTo(1);
			seasonArchives.Data.Last().Season.Should().HaveCountLessOrEqualTo(4);
		}
	}
}