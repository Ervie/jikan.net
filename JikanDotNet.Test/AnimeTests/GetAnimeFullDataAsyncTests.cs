using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using JikanDotNet;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
    public class GetAnimeFullDataAsyncTests
    {
        private readonly IJikan _jikan;

        public GetAnimeFullDataAsyncTests()
        {
            _jikan = new Jikan();
        }

        [Theory]
        [InlineData(long.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetAnimeFullDataAsync_InvalidId_ShouldThrowValidationException(long malId)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetAnimeFullDataAsync(malId));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }


        [Fact]
        public async Task GetAnimeFullDataAsync_BebopId_ShouldParseCowboyBebop()
        {
            // When
            var bebopAnime = await _jikan.GetAnimeFullDataAsync(1);

            // Then
            using var _ = new AssertionScope();
            bebopAnime.Data.Title.Should().Be("Cowboy Bebop");
            bebopAnime.Data.ExternalLinks.Should().Contain(x =>
                x.Name.Equals("Wikipedia") && x.Url.Equals("http://en.wikipedia.org/wiki/Cowboy_Bebop"));
            bebopAnime.Data.ExternalLinks.Should().Contain(x =>
                x.Name.Equals("AnimeDB") && x.Url.Equals("http://anidb.info/perl-bin/animedb.pl?show=anime&aid=23"));
            bebopAnime.Data.MusicThemes.Openings.Should().ContainSingle().Which
                .Equals("\"Tank!\" by The Seatbelts (eps 1-25)");
            bebopAnime.Data.MusicThemes.Endings.Should().HaveCount(3);
            bebopAnime.Data.Relations.Should().HaveCount(3);
            bebopAnime.Data.Relations.Should()
                .ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 2);
            bebopAnime.Data.Relations.Should()
                .ContainSingle(x => x.Relation.Equals("Side story") && x.Entry.Count == 2);
            bebopAnime.Data.Relations.Should().ContainSingle(x => x.Relation.Equals("Summary") && x.Entry.Count == 1);
        }
    }
}