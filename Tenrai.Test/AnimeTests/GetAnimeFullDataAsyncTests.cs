using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Tenrai;
using Xunit;

namespace Tenrai.Tests.AnimeTests
{
    [Collection("TenraiTests")]
    public class GetAnimeFullDataAsyncTests
    {
        private readonly ITenrai _tenrai;

        public GetAnimeFullDataAsyncTests(TenraiFixture tenraiFixture)
        {
            _tenrai = tenraiFixture.TenraiClient;
        }

        [Theory]
        [InlineData(long.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetAnimeFullDataAsync_InvalidId_ShouldThrowValidationException(long malId)
        {
            // When
            var func = _tenrai.Awaiting(x => x.GetAnimeFullDataAsync(malId));

            // Then
            await func.Should().ThrowExactlyAsync<TenraiValidationException>();
        }


        [Fact]
        public async Task GetAnimeFullDataAsync_BebopId_ShouldParseCowboyBebop()
        {
            // When
            var bebopAnime = await _tenrai.GetAnimeFullDataAsync(1);

            // Then
            using var _ = new AssertionScope();
            bebopAnime.Data.Title.Should().Be("Cowboy Bebop");
            bebopAnime.Data.ExternalLinks.Should().Contain(x =>
                x.Name.Equals("Wikipedia") && x.Url.Equals("http://en.wikipedia.org/wiki/Cowboy_Bebop"));
            bebopAnime.Data.MusicThemes.Openings.Should().ContainSingle().Which
                .Equals("\"Tank!\" by The Seatbelts (eps 1-25)");
            bebopAnime.Data.MusicThemes.Endings.Should().HaveCount(3);
            bebopAnime.Data.Relations.Should().HaveCount(3);
            bebopAnime.Data.Relations.Should()
                .ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 2);
            bebopAnime.Data.Relations.Should()
                .ContainSingle(x => x.Relation.Equals("Side story") && x.Entry.Count == 2);
            bebopAnime.Data.Relations.Should().ContainSingle(x => x.Relation.Equals("Summary") && x.Entry.Count == 1);
            bebopAnime.Data.StreamingLinks.Should().Contain(x => x.Name.Equals("Crunchyroll") && x.Url.Equals("http://www.crunchyroll.com/series-271225"));
            bebopAnime.Data.StreamingLinks.Should().Contain(x => x.Name.Equals("Netflix") && x.Url.Equals("https://www.netflix.com/title/80001305"));
        }

        [Fact]
        public async Task GetAnimeFullDataAsync_TooManyIds_ShouldThrowValidationException()
        {
            // Given
            var config = new FullDataConfig { Ids = Enumerable.Range(2, 25).Select(i => (long)i).ToList() };

            // When
            var func = _tenrai.Awaiting(x => x.GetAnimeFullDataAsync(1, config));

            // Then
            await func.Should().ThrowExactlyAsync<TenraiValidationException>();
        }

        [Fact]
        public async Task GetAnimeFullDataAsync_BatchIds_ReturnsIncludedAndMissing()
        {
            // When
            var bebop = await _tenrai.GetAnimeFullDataAsync(1, new FullDataConfig { Ids = new List<long> { 5, 6, 99999999 } });

            // Then
            using var _ = new AssertionScope();
            bebop.Data.MalId.Should().Be(1);
            bebop.Included.Select(x => x.MalId).Should().Equal(5, 6);
            bebop.MissingIds.Should().Equal(99999999);
        }

        [Fact]
        public async Task GetAnimeFullDataAsync_DeletedMode_ReturnsArchivedEntry()
        {
            // When
            var doraemon = await _tenrai.GetAnimeFullDataAsync(2632, new FullDataConfig { Mode = FullDataMode.Deleted });

            // Then
            doraemon.Data.Titles.Should().Contain(x => x.Title == "Doraemon: Time Machine de Oshougatsu");
        }
    }
}