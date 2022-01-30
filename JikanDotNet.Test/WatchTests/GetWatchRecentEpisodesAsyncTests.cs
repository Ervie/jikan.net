using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.WatchTests
{
    public class GetWatchRecentEpisodesAsyncTests
    {
        private readonly IJikan _jikan;

        public GetWatchRecentEpisodesAsyncTests()
        {
            _jikan = new Jikan();
        }
        
        [Fact]
        public async Task GetWatchRecentEpisodesAsync_ShouldReturnNonEmptyCollection()
        {
            // When
            var episodes = await _jikan.GetWatchRecentEpisodesAsync();

            // Then
            using var _ = new AssertionScope();
            episodes.Data.Should().NotBeEmpty();
            episodes.Data.First().Episodes.Should().HaveCount(2);
            episodes.Data.First().RegionLocked.Should().BeFalse();
            episodes.Data.First().Episodes.Should().HaveCount(2);
            episodes.Data.First().Episodes.Should().OnlyContain(x => x.Premium.HasValue);
        }
    }
}