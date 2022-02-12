using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.ReviewTests;

public class GetRecentAnimeReviewsAsyncTests
{
    private readonly IJikan _jikan;

    public GetRecentAnimeReviewsAsyncTests()
    {
        _jikan = new Jikan();
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetRecentAnimeReviewsAsync_InvalidPage_ShouldThrowValidationException(int page)
    {
        // When
        var func = _jikan.Awaiting(x => x.GetRecentAnimeReviewsAsync(page));

        // Then
        await func.Should().ThrowExactlyAsync<JikanValidationException>();
    }

    [Fact]
    public async Task GetRecentAnimeReviewsAsync_ShouldParseFirstPageReviews()
    {
        // When
        var reviews = await _jikan.GetRecentAnimeReviewsAsync();

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
        var reviews = await _jikan.GetRecentAnimeReviewsAsync(1);

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
        var reviews = await _jikan.GetRecentAnimeReviewsAsync(2);

        // Then
        using var _ = new AssertionScope();
        reviews.Pagination.HasNextPage.Should().BeTrue();
        reviews.Data.Should().HaveCount(50);
        reviews.Data.Should().OnlyContain(x => x.Type == "anime");
    }
}