using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.WatchTests
{
    public class GetWatchPopularEpisodesAsyncTests
    {
        
        private readonly IJikan _jikan;

        public GetWatchPopularEpisodesAsyncTests()
        {
            _jikan = new Jikan();
        }
        
        [Fact]
        public async Task GetWatchPopularEpisodesAsync_ShouldReturnNonEmptyCollection()
        {
            // When
            var recentEpisodes = await _jikan.GetWatchPopularEpisodesAsync();

            // Then
            using var _ = new AssertionScope();
            recentEpisodes.Data.Should().NotBeEmpty();
            recentEpisodes.Pagination.HasNextPage.Should().BeFalse();
            recentEpisodes.Pagination.LastVisiblePage.Should().Be(1);
            recentEpisodes.Data.First().Episodes.Should().HaveCount(2);
            recentEpisodes.Data.First().RegionLocked.Should().BeTrue();
            recentEpisodes.Data.First().Episodes.Should().HaveCount(2);
            recentEpisodes.Data.First().Episodes.Should().OnlyContain(x => x.Premium.HasValue);
        }
    }
}