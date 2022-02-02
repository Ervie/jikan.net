using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet.Consts;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
    public class GetMultipleMangaTestsAsync
    {
        private readonly IJikan _jikan;

        public GetMultipleMangaTestsAsync()
        {
            _jikan = new Jikan();
        }

        [Fact]
        public async Task GetMangaAsync_NoParameter_ShouldReturnFirst25Anime()
        {
            // When
            var manga = await _jikan.GetMangaAsync();

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            manga.Data.First().Title.Should().Be("Monster");
            manga.Pagination.LastVisiblePage.Should().BeGreaterThan(780);
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetMangaAsync_InvalidPage_ShouldThrowValidationException(int page)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetMangaAsync(page, ParameterConsts.MaximumPageSize));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(26)]
        [InlineData(int.MaxValue)]
        public async Task GetMangaAsync_InvalidPageSize_ShouldThrowValidationException(int pageSize)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetMangaAsync(1, pageSize));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task GetMangaAsync_GivenSecondPage_ShouldReturnSecondPage()
        {
            // When
            var manga = await _jikan.GetMangaAsync(2, ParameterConsts.MaximumPageSize);

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            manga.Data.First().Title.Should().Be("Nana");
            manga.Pagination.LastVisiblePage.Should().BeGreaterThan(780);
        }
        
        [Fact]
        public async Task GetMangaAsync_GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
        {
            // Given
            const int pageSize = 5;
            
            // When
            var manga = await _jikan.GetMangaAsync(1, pageSize);

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(pageSize);
            manga.Data.First().Title.Should().Be("Monster");
        }
        
        [Fact]
        public async Task GetMangaAsync_GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
        {
            // Given
            const int pageSize = 5;
            
            // When
            var manga = await _jikan.GetMangaAsync(2, pageSize);

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(pageSize);
            manga.Data.First().Title.Should().Be("Full Moon wo Sagashite");
        }
    }
}