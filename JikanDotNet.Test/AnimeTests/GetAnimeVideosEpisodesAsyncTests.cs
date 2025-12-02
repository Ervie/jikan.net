using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Test.AnimeTests
{
    public class GetAnimeVideosEpisodesAsyncTests
    {
        private readonly IJikan _jikan;

        public GetAnimeVideosEpisodesAsyncTests()
        {
            _jikan = new Jikan();
        }

        [Fact]
        public async Task GetAnimeVideosEpisodesAsync_Id1_ReturnsEpisodes()
        {
            // Arrange
            long animeId = 1; // Cowboy Bebop

            // Act
            var result = await _jikan.GetAnimeVideosEpisodesAsync(animeId);

            // Assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Data.Should().NotBeNull();
                result.Data.Should().NotBeEmpty();
                result.Data.Should().OnlyContain(e => !string.IsNullOrWhiteSpace(e.Title) && !string.IsNullOrWhiteSpace(e.Url));
                result.Data.Select(e => e.Title).Should().Contain("Asteroid Blues"); // Example episode title
            }
        }
    }
}
