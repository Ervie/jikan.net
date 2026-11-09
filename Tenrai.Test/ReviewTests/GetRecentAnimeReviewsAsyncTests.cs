using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.ReviewTests;

[Collection("TenraiTests")]
public class GetRecentAnimeReviewsAsyncTests
{
    private readonly ITenrai _tenrai;

    public GetRecentAnimeReviewsAsyncTests(TenraiFixture tenraiFixture)
    {
        _tenrai = tenraiFixture.TenraiClient;
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetRecentAnimeReviewsAsync_InvalidPage_ShouldThrowValidationException(int page)
    {
        // When
        var func = _tenrai.Awaiting(x => x.GetRecentAnimeReviewsAsync(new ReviewsSearchConfig { Page = page }));

        // Then
        await func.Should().ThrowExactlyAsync<TenraiValidationException>();
    }

    [Fact]
    public async Task GetRecentAnimeReviewsAsync_ShouldParseFirstPageReviews()
    {
        // When
        var reviews = await _tenrai.GetRecentAnimeReviewsAsync();

        // Then
        using var _ = new AssertionScope();
        reviews.Pagination.HasNextPage.Should().BeTrue();
        reviews.Data.Should().HaveCount(50);
        reviews.Data.Should().OnlyContain(x => x.Type == "anime");
    }

    [Fact]
    public async Task GetRecentAnimeReviewsAsync_FirstPage_ShouldParseFirstPageReviews()
    {
        // When
        var reviews = await _tenrai.GetRecentAnimeReviewsAsync(new ReviewsSearchConfig { Page = 1 });

        // Then
        using var _ = new AssertionScope();
        reviews.Pagination.HasNextPage.Should().BeTrue();
        reviews.Data.Should().HaveCount(50);
        reviews.Data.Should().OnlyContain(x => x.Type == "anime");
    }
    
    [Fact]
    public async Task GetRecentAnimeReviewsAsync_SecondPage_ShouldParseSecondPageReviews()
    {
        // When
        var reviews = await _tenrai.GetRecentAnimeReviewsAsync(new ReviewsSearchConfig { Page = 2 });

        // Then
        using var _ = new AssertionScope();
        reviews.Pagination.HasNextPage.Should().BeTrue();
        reviews.Data.Should().HaveCount(50);
        reviews.Data.Should().OnlyContain(x => x.Type == "anime");
    }

    [Fact]
    public async Task GetRecentAnimeReviewsAsync_ReviewsHaveEntry()
    {
        // When
        var reviews = await _tenrai.GetRecentAnimeReviewsAsync();

        // Then
        reviews.Data.Should().OnlyContain(x => x.Entry != null && x.Entry.MalId > 0 && x.Entry.Title != null);
    }
}