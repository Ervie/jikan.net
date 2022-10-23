using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using Xunit;

namespace JikanDotNet.Tests.UserTests;

public class GetUserExternalLinksAsyncTests
{
    private readonly IJikan _jikan;

    public GetUserExternalLinksAsyncTests()
    {
        _jikan = new Jikan();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\n\n\t    \t")]
    public async Task GetUserExternalLinksAsync_InvalidUsername_ShouldThrowValidationException(string username)
    {
        // When
        var func = _jikan.Awaiting(x => x.GetUserReviewsAsync(username));

        // Then
        await func.Should().ThrowExactlyAsync<JikanValidationException>();
    }

    [Fact]
    public async Task GetUserExternalLinksAsync_Ervelan_ShouldParseErvelanUrls()
    {
        // When
        var links = await _jikan.GetUserExternalLinksAsync("Ervelan");

        // Then
        links.Data.Should().ContainSingle(x => x.Name.Equals("seiyuu.moe") && x.Url.Equals("https://seiyuu.moe/"));
    }

    [Fact]
    public async Task GetUserExternalLinksAsync_SonMati_ShouldParseSonMatiUrls()
    {
        // When
        var links = await _jikan.GetUserExternalLinksAsync("sonmati");

        // Then
        using var _ = new AssertionScope();
        links.Data.Should().HaveCount(2);
        links.Data.Should().ContainSingle(x => x.Name.Equals("vndb.org") && x.Url.Equals("https://vndb.org/u55837"));
        links.Data.Should().ContainSingle(x => x.Name.Equals("steamcommunity.com") && x.Url.Equals("http://steamcommunity.com/profiles/76561198169895549/"));
    }
}