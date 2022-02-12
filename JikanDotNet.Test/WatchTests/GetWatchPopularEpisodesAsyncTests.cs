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
            var episodes = await _jikan.GetWatchPopularEpisodesAsync();

            // Then
            using var _ = new AssertionScope();
            episodes.Data.Should().NotBeEmpty();
            episodes.Pagination.HasNextPage.Should().BeFalse();
            episodes.Pagination.LastVisiblePage.Should().Be(1);
            episodes.Data.First().Episodes.Should().HaveCount(2);
            episodes.Data.First().RegionLocked.Should().BeTrue();
            episodes.Data.First().Episodes.Should().HaveCount(2);
            episodes.Data.First().Episodes.Should().OnlyContain(x => x.Premium.HasValue);
        }
    }
}