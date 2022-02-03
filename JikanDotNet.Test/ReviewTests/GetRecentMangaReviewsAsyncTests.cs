using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.ReviewTests;

public class GetRecentMangaReviewsAsyncTests
{
    private readonly IJikan _jikan;

    public GetRecentMangaReviewsAsyncTests()
    {
        _jikan = new Jikan();
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetRecentMangaReviewsAsync_InvalidPage_ShouldThrowValidationException(int page)
    {
        // When
        var func = _jikan.Awaiting(x => x.GetRecentMangaReviewsAsync(page));

        // Then
        await func.Should().ThrowExactlyAsync<JikanValidationException>();
    }

    [Fact]
    public async Task GetRecentMangaReviewsAsync_ShouldParseFirstPageReviews()
    {
        // When
        var reviews = await _jikan.GetRecentMangaReviewsAsync();

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
        var reviews = await _jikan.GetRecentMangaReviewsAsync(1);

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
        var reviews = await _jikan.GetRecentMangaReviewsAsync(2);

        // Then
        using var _ = new AssertionScope();
        reviews.Pagination.HasNextPage.Should().BeTrue();
        reviews.Data.Should().HaveCount(50);
        reviews.Data.Should().OnlyContain(x => x.Type == "manga");
    }
}