using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests;

public class GetAnimeStreamingLinksAsyncTests
{
    
    private readonly IJikan _jikan;

    public GetAnimeStreamingLinksAsyncTests()
    {
        _jikan = new Jikan();
    }

    [Theory]
    [InlineData(long.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetAnimeStreamingLinksAsync_InvalidId_ShouldThrowValidationException(long malId)
    {
        // When
        var func = _jikan.Awaiting(x => x.GetAnimeStreamingLinksAsync(malId));

        // Then
        await func.Should().ThrowExactlyAsync<JikanValidationException>();
    }
    
    [Fact]
    public async Task GetAnimeStreamingLinksAsync_BebopId_ShouldReturnBebopLinks()
    {
        // When
        var links = await _jikan.GetAnimeStreamingLinksAsync(1);

        // Then
        using var _ = new AssertionScope();
        links.Data.Should().HaveCount(4);
        links.Data.Should().Contain(x => x.Name.Equals("Crunchyroll") && x.Url.Equals("http://www.crunchyroll.com/series-271225"));
        links.Data.Should().Contain(x => x.Name.Equals("Netflix") && x.Url.Equals("https://www.netflix.com/title/80001305"));
    }
}