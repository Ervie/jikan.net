using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests;

public class GetAnimeExternalLinksAsyncTests
{
    
    private readonly IJikan _jikan;

    public GetAnimeExternalLinksAsyncTests()
    {
        _jikan = new Jikan();
    }

    [Theory]
    [InlineData(long.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetAnimeExternalLinksAsync_InvalidId_ShouldThrowValidationException(long malId)
    {
        // When
        var func = _jikan.Awaiting(x => x.GetAnimeExternalLinksAsync(malId));

        // Then
        await func.Should().ThrowExactlyAsync<JikanValidationException>();
    }
    
    [Fact]
    public async Task GetAnimeExternalLinksAsync_BebopId_ShouldReturnBebopLinks()
    {
        // When
        var links = await _jikan.GetAnimeExternalLinksAsync(1);

        // Then
        using var _ = new AssertionScope();
        links.Data.Should().Contain(x => x.Name.Equals("Wikipedia") && x.Url.Equals("http://en.wikipedia.org/wiki/Cowboy_Bebop"));
        links.Data.Should().Contain(x => x.Name.Equals("AnimeDB") && x.Url.Equals("http://anidb.info/perl-bin/animedb.pl?show=anime&aid=23"));
    }
}