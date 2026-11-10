using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.ReviewTests;

[Collection("TenraiTests")]
public class GetRecentMangaReviewsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetRecentMangaReviewsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetRecentMangaReviewsAsync_InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetRecentMangaReviewsAsync(new RecentReviewsConfig { Page = page }));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetRecentMangaReviewsAsync_ShouldParseFirstPageReviews()
	{
		// When
		var reviews = await _tenrai.GetRecentMangaReviewsAsync();

		// Then
		using var _ = new AssertionScope();
		reviews.Pagination.HasNextPage.Should().BeTrue();
		reviews.Data.Should().HaveCount(50);
		reviews.Data.Should().OnlyContain(x => x.Type == "manga");
	}

	[Fact]
	public async Task GetRecentMangaReviewsAsync_FirstPage_ShouldParseFirstPageReviews()
	{
		// When
		var reviews = await _tenrai.GetRecentMangaReviewsAsync(new RecentReviewsConfig { Page = 1 });

		// Then
		using var _ = new AssertionScope();
		reviews.Pagination.HasNextPage.Should().BeTrue();
		reviews.Data.Should().HaveCount(50);
		reviews.Data.Should().OnlyContain(x => x.Type == "manga");
	}

	[Fact]
	public async Task GetRecentMangaReviewsAsync_SecondPage_ShouldParseSecondPageReviews()
	{
		// When
		var reviews = await _tenrai.GetRecentMangaReviewsAsync(new RecentReviewsConfig { Page = 2 });

		// Then
		using var _ = new AssertionScope();
		reviews.Pagination.HasNextPage.Should().BeTrue();
		reviews.Data.Should().HaveCount(50);
		reviews.Data.Should().OnlyContain(x => x.Type == "manga");
	}
}