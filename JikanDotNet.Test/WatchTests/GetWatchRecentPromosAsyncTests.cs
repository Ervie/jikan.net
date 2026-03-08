using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.WatchTests
{
    [Collection("JikanTests")]
    public class GetWatchRecentPromosAsyncTests
    {
        private readonly IJikan _jikan;

        public GetWatchRecentPromosAsyncTests(JikanFixture jikanFixture)
        {
            _jikan = jikanFixture.Jikan;
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetWatchRecentPromosAsync_InvalidPage_ShouldThrowValidationException(int page)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetWatchRecentPromosAsync(page));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }

        [Fact]
        public async Task GetWatchRecentPromosAsync_ShouldReturnNonEmptyCollection()
        {
            // When
            var promos = await _jikan.GetWatchRecentPromosAsync();

            // Then
            using var _ = new AssertionScope();
            promos.Data.Should().NotBeEmpty();
            promos.Data.First().Trailer.EmbedUrl.Should().NotBeNullOrEmpty();
            promos.Data.First().Entry.Title.Should().NotBeNullOrEmpty();
            promos.Data.First().Entry.Images.Should().NotBeNull();
            promos.Data.First().Title.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GetWatchRecentPromosAsync_FirstPage_ShouldReturnNonEmptyCollection()
        {
            // When
            var promos = await _jikan.GetWatchRecentPromosAsync(1);

            // Then
            using var _ = new AssertionScope();
            promos.Data.Should().NotBeEmpty();
            promos.Data.First().Trailer.Should().NotBeNull();
            promos.Data.First().Entry.Title.Should().NotBeNullOrEmpty();
            promos.Pagination.Should().NotBeNull();
        }
    }
}