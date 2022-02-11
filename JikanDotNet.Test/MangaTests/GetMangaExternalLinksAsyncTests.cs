using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests;

public class GetMangaExternalLinksAsyncTests
{
    
    private readonly IJikan _jikan;

    public GetMangaExternalLinksAsyncTests()
    {
        _jikan = new Jikan();
    }

    [Theory]
    [InlineData(long.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetMangaExternalLinksAsync_InvalidId_ShouldThrowValidationException(long malId)
    {
        // When
        var func = _jikan.Awaiting(x => x.GetMangaExternalLinksAsync(malId));

        // Then
        await func.Should().ThrowExactlyAsync<JikanValidationException>();
    }
    
    [Fact]
    public async Task GetMangaExternalLinksAsync_MonsterId_ShouldReturnMonsterLinks()
    {
        // When
        var links = await _jikan.GetMangaExternalLinksAsync(1);

        // Then
        using var _ = new AssertionScope();
        links.Data.Should().ContainSingle();
        links.Data.Should().Contain(x => x.Name.Equals("Wikipedia") && x.Url.Equals("http://ja.wikipedia.org/wiki/MONSTER"));
    }
}