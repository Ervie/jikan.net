using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.RecommendationTests;

[Collection("TenraiTests")]
public class GetRecentAnimeRecommendationsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetRecentAnimeRecommendationsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetRecentAnimeRecommendationsAsync_InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetRecentAnimeRecommendationsAsync(page));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetRecentAnimeRecommendationsAsync_ShouldParseFirstPageReviews()
	{
		// When
		var recs = await _tenrai.GetRecentAnimeRecommendationsAsync();

		// Then
		using var _ = new AssertionScope();
		recs.Pagination.HasNextPage.Should().BeTrue();
		recs.Pagination.LastVisiblePage.Should().Be(20);
		recs.Data.Should().HaveCount(100);
		recs.Data.Should().OnlyContain(x => x.Entries.Count == 2);
	}

	[Fact]
	public async Task GetRecentAnimeRecommendationsAsync_FirstPage_ShouldParseFirstPageReviews()
	{
		// When
		var recs = await _tenrai.GetRecentAnimeRecommendationsAsync(1);

		// Then
		using var _ = new AssertionScope();
		recs.Pagination.HasNextPage.Should().BeTrue();
		recs.Pagination.LastVisiblePage.Should().Be(20);
		recs.Data.Should().HaveCount(100);
		recs.Data.Should().OnlyContain(x => x.Entries.Count == 2);
	}

	[Fact]
	public async Task GetRecentAnimeRecommendationsAsync_SecondPage_ShouldParseSecondPageReviews()
	{
		// When
		var recs = await _tenrai.GetRecentAnimeRecommendationsAsync(2);

		// Then
		using var _ = new AssertionScope();
		recs.Pagination.HasNextPage.Should().BeTrue();
		recs.Pagination.LastVisiblePage.Should().Be(20);
		recs.Data.Should().HaveCount(100);
		recs.Data.Should().OnlyContain(x => x.Entries.Count == 2);
	}

	[Fact]
	public async Task GetRecentAnimeRecommendationsAsync_Limit10_ReturnsTen()
	{
		// When
		var recs = await _tenrai.GetRecentAnimeRecommendationsAsync(new RecentRecommendationsConfig { Limit = 10 });

		// Then
		recs.Data.Should().HaveCount(10);
	}
}