using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests;

[Collection("JikanTests")]
public class GetAnimeExternalLinksAsyncTests
{
    
    private readonly IJikan _jikan;

    public GetAnimeExternalLinksAsyncTests(JikanFixture jikanFixture)
    {
        _jikan = jikanFixture.Jikan;
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
        links.Data.Should().Contain(x => x.Name.Equals("Wikipedia") && x.Url.Equals("http://en.wikipedia.org/wiki/Cowboy_Bebop"));
    }
}