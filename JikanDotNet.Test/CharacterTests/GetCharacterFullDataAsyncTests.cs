using System.Linq;
using FluentAssertions;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using FluentAssertions.Execution;
using Xunit;

namespace JikanDotNet.Tests.CharacterTests
{
    public class GetCharacterFullDataAsyncTests
    {
        private readonly IJikan _jikan;

        public GetCharacterFullDataAsyncTests()
        {
            _jikan = new Jikan();
        }

        [Theory]
        [InlineData(long.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetCharacterFullDataAsync_InvalidId_ShouldThrowValidationException(long malId)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetCharacterFullDataAsync(malId));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task GetCharacterFullDataAsync_IchigoKurosakiId_ShouldParseIchigoKurosaki()
        {
            // When
            var ichigo = await _jikan.GetCharacterFullDataAsync(5);

            // Then
            using var _ = new AssertionScope();
            ichigo.Data.Name.Should().Be("Ichigo Kurosaki");
            ichigo.Data.NameKanji.Should().Be("黒崎 一護");
            ichigo.Data.Animeography.Should().HaveCount(9);
            ichigo.Data.Animeography.Select(x => x.Anime.Title).Should().Contain("Bleach");
            ichigo.Data.Mangaography.Should().HaveCount(7);
            ichigo.Data.VoiceActors.Should().HaveCount(14);
            ichigo.Data.VoiceActors.Should().Contain(x => x.Person.Name.Equals("Morita, Masakazu"));
        }
    }
}