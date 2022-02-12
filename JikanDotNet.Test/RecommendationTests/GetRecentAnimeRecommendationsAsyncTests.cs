using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.RecommendationTests;

public class GetRecentAnimeRecommendationsAsyncTests
{
    private readonly IJikan _jikan;

    public GetRecentAnimeRecommendationsAsyncTests()
    {
        _jikan = new Jikan();
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetRecentAnimeRecommendationsAsync_InvalidPage_ShouldThrowValidationException(int page)
    {
        // When
        var func = _jikan.Awaiting(x => x.GetRecentAnimeRecommendationsAsync(page));

        // Then
        await func.Should().ThrowExactlyAsync<JikanValidationException>();
    }

    [Fact]
    public async Task GetRecentAnimeRecommendationsAsync_ShouldParseFirstPageReviews()
    {
        // When
        var recs = await _jikan.GetRecentAnimeRecommendationsAsync();

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
        var recs = await _jikan.GetRecentAnimeRecommendationsAsync(1);

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
        var recs = await _jikan.GetRecentAnimeRecommendationsAsync(2);

        // Then
        using var _ = new AssertionScope();
        recs.Pagination.HasNextPage.Should().BeTrue();
        recs.Pagination.LastVisiblePage.Should().Be(20);
        recs.Data.Should().HaveCount(100);
        recs.Data.Should().OnlyContain(x => x.Entries.Count == 2);
    }
}