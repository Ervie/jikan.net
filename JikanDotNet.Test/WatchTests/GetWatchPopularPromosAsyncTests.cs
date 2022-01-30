using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.WatchTests
{
    public class GetWatchPopularPromosAsyncTests
    {
        private readonly IJikan _jikan;

        public GetWatchPopularPromosAsyncTests()
        {
            _jikan = new Jikan();
        }

        [Fact]
        public async Task GetWatchPopularPromosAsyncAsync_ShouldReturnNonEmptyCollection()
        {
            // When
            var promos = await _jikan.GetWatchPopularPromosAsync();

            // Then
            using var _ = new AssertionScope();
            promos.Data.Should().NotBeEmpty();
            promos.Data.First().Trailer.Url.Should().NotBeNullOrEmpty();
            promos.Data.First().Trailer.YoutubeId.Should().NotBeNullOrEmpty();
            promos.Data.First().Trailer.EmbedUrl.Should().NotBeNullOrEmpty();
            promos.Data.First().Entry.Title.Should().NotBeNullOrEmpty();
            promos.Data.First().Entry.Url.Should().NotBeNullOrEmpty();
            promos.Data.First().Entry.Images.Should().NotBeNull();
            promos.Data.First().Title.Should().NotBeNullOrEmpty();
        }
    }
}