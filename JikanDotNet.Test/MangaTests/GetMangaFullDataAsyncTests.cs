using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
    public class GetMangaFullDataAsyncTests
    {
        private readonly IJikan _jikan;

        public GetMangaFullDataAsyncTests()
        {
            _jikan = new Jikan();
        }

        [Theory]
        [InlineData(long.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetMangaFullDataAsync_InvalidId_ShouldThrowValidationException(long malId)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetMangaFullDataAsync(malId));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }


        [Fact]
        public async Task GetMangaFullDataAsync_MonsterId_ShouldParseMonster()
        {
            // When
            var monsterManga = await _jikan.GetMangaFullDataAsync(1);

            // Then
            using var _ = new AssertionScope();
            monsterManga.Data.Title.Should().Be("Monster");
            monsterManga.Data.ExternalLinks.Should().ContainSingle();
            monsterManga.Data.ExternalLinks.Should().Contain(x => x.Name.Equals("Wikipedia") && x.Url.Equals("http://ja.wikipedia.org/wiki/MONSTER"));
            monsterManga.Data.Relations.Should().HaveCount(2);
            monsterManga.Data.Relations.Should().ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 1);
            monsterManga.Data.Relations.Should().ContainSingle(x => x.Relation.Equals("Side story") && x.Entry.Count == 1);
        }
    }
}