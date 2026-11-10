using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.AnimeTests;

[Collection("TenraiTests")]
public class GetAnimeStatisticsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetAnimeStatisticsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetAnimeStatisticsAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeStatisticsAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetAnimeStatistics_BebopId_ShouldParseCowboyBebopStats()
	{
		// When
		var bebop = await _tenrai.GetAnimeStatisticsAsync(1);

		// Then
		using (new AssertionScope())
		{
			bebop.Data.ScoreStats.Should().NotBeNull();
			bebop.Data.Completed.Should().BeGreaterThan(450000);
			bebop.Data.PlanToWatch.Should().BeGreaterThan(50000);
			bebop.Data.Total.Should().BeGreaterThan(1500000);
			bebop.Data.ScoreStats.Should().HaveCount(10);
			bebop.Data.ScoreStats.Should().Contain(score => score.Score == 5 && score.Votes > 10000);
		}
	}
}