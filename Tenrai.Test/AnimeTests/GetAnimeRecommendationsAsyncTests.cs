using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.AnimeTests;

[Collection("TenraiTests")]
public class GetAnimeRecommendationsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetAnimeRecommendationsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetAnimeRecommendationsAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeRecommendationsAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetAnimeRecommendationsAsync_BebopId_ShouldParseCowboyBebopRecommendations()
	{
		// When
		var bebop = await _tenrai.GetAnimeRecommendationsAsync(1);

		// Then
		using (new AssertionScope())
		{
			bebop.Data.First().Entry.MalId.Should().Be(205);
			bebop.Data.First().Entry.Title.Should().Be("Samurai Champloo");
			bebop.Data.First().Votes.Should().BeGreaterThan(70);
			bebop.Data.Count.Should().BeGreaterThan(100);
		}
	}
}