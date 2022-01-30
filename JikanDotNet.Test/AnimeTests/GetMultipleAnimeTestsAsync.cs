using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet.Consts;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
    public class GetMultipleAnimeTestsAsync
    {
        private readonly IJikan _jikan;

        public GetMultipleAnimeTestsAsync()
        {
            _jikan = new Jikan();
        }

        [Fact]
        public async Task GetAnimeAsync_NoParameter_ShouldReturnFirst25Anime()
        {
            // When
            var anime = await _jikan.GetAnimeAsync();

            // Then
            using var _ = new AssertionScope();
            anime.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            anime.Data.First().Title.Should().Be("Cowboy Bebop");
            anime.Pagination.LastVisiblePage.Should().BeGreaterThan(780);
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetAnimeAsync_InvalidPage_ShouldThrowValidationException(int page)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetAnimeAsync(page, ParameterConsts.MaximumPageSize));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(26)]
        [InlineData(int.MaxValue)]
        public async Task GetAnimeAsync_InvalidPageSize_ShouldThrowValidationException(int pageSize)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetAnimeAsync(1, pageSize));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task GetAnimeAsync_GivenSecondPage_ShouldReturnSecondPage()
        {
            // When
            var anime = await _jikan.GetAnimeAsync(2, ParameterConsts.MaximumPageSize);

            // Then
            using var _ = new AssertionScope();
            anime.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            anime.Data.First().Title.Should().Be("Rurouni Kenshin: Meiji Kenkaku Romantan - Tsuioku-hen");
            anime.Pagination.LastVisiblePage.Should().BeGreaterThan(780);
        }
        
        [Fact]
        public async Task GetAnimeAsync_GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
        {
            // Given
            const int pageSize = 5;
            
            // When
            var anime = await _jikan.GetAnimeAsync(1, pageSize);

            // Then
            using var _ = new AssertionScope();
            anime.Data.Should().HaveCount(pageSize);
            anime.Data.First().Title.Should().Be("Cowboy Bebop");
        }
        
        [Fact]
        public async Task GetAnimeAsync_GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
        {
            // Given
            const int pageSize = 5;
            
            // When
            var anime = await _jikan.GetAnimeAsync(2, pageSize);

            // Then
            using var _ = new AssertionScope();
            anime.Data.Should().HaveCount(pageSize);
            anime.Data.First().Title.Should().Be("Eyeshield 21");
        }
    }
}