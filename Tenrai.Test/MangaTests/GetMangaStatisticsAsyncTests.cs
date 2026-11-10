using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.MangaTests;

[Collection("TenraiTests")]
public class GetMangaStatisticsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetMangaStatisticsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetMangaStatisticsAsync_InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaStatisticsAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetMangaStatistics_MonsterId_ShouldParseMonsterStats()
	{
		// When
		var monster = await _tenrai.GetMangaStatisticsAsync(1);

		// Then
		using (new AssertionScope())
		{
			monster.Data.ScoreStats.Should().NotBeNull();
			monster.Data.Completed.Should().BeGreaterThan(25000);
			monster.Data.Dropped.Should().BeGreaterThan(500);
			monster.Data.Total.Should().BeGreaterThan(160000);
			monster.Data.ScoreStats.Should().Contain(x => x.Score.Equals(8) && x.Votes > 8500);
		}
	}
}
