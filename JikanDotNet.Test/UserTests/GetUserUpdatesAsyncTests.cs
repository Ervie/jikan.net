using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using Xunit;

namespace JikanDotNet.Tests.UserTests;

public class GetUserUpdatesAsyncTests
{
    private readonly IJikan _jikan;

    public GetUserUpdatesAsyncTests()
    {
        _jikan = new Jikan();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("\n\n\t    \t")]
    public async Task GetUserUpdatesAsync_InvalidUsername_ShouldThrowValidationException(string username)
    {
        // When
        var func = _jikan.Awaiting(x => x.GetUserUpdatesAsync(username));

        // Then
        await func.Should().ThrowExactlyAsync<JikanValidationException>();
    }

    [Fact]
    public async Task GetUserProfileAsync_Ervelan_ShouldParseErvelanProfile()
    {
        // When
        var user = await _jikan.GetUserUpdatesAsync("Ervelan");

        // Then
        using var _ = new AssertionScope();
        user.Should().NotBeNull();
        user.Data.AnimeUpdates.Should().NotBeNullOrEmpty();
        user.Data.AnimeUpdates.First().User.Should().BeNull();
        user.Data.AnimeUpdates.First().Entry.Should().NotBeNull();
        user.Data.MangaUpdates.Should().NotBeNullOrEmpty();
        user.Data.MangaUpdates.First().User.Should().BeNull();
        user.Data.MangaUpdates.First().Entry.Should().NotBeNull();
    }
}