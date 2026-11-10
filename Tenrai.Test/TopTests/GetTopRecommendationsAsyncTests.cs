using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Tenrai.Tests.TopTests;

[Collection("TenraiTests")]
public class GetTopRecommendationsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetTopRecommendationsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Fact]
	public async Task GetTopAnimeRecommendationsAsync_FirstPage_HasVotesAndTwoAnime()
	{
		var recommendations = await _tenrai.GetTopAnimeRecommendationsAsync();

		using var _ = new AssertionScope();
		recommendations.Data.Should().NotBeEmpty();
		recommendations.Data.Select(x => x.Votes).Should().BeInDescendingOrder();
		recommendations.Data.First().Votes.Should().BeGreaterThan(100);
		recommendations.Data.Should().OnlyContain(x => x.Url.Contains("/recommendations/anime/"));
		recommendations.Data.Should().OnlyContain(x => x.Entries.Count == 2 && x.Entries.All(e => e.Url.Contains("/anime/")));
	}

	[Fact]
	public async Task GetTopMangaRecommendationsAsync_EntriesAreManga()
	{
		var recommendations = await _tenrai.GetTopMangaRecommendationsAsync(new TopRecommendationsConfig { Limit = 5 });

		using var _ = new AssertionScope();
		recommendations.Data.Should().HaveCount(5);
		recommendations.Data.Should().OnlyContain(x => x.Entries.All(e => e.Url.Contains("/manga/")));
	}
}
