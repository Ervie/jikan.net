using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet.Consts;
using Xunit;

namespace JikanDotNet.Tests.CharacterTests
{
    public class GetCharactersAsyncTests
    {
        private readonly IJikan _jikan;

        public GetCharactersAsyncTests()
        {
            _jikan = new Jikan();
        }

        [Fact]
        public async Task GetCharactersAsync_NoParameter_ShouldReturnFirst25Anime()
        {
            // When
            var manga = await _jikan.GetCharactersAsync();

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            manga.Data.Skip(1).First().Name.Should().Be("Spike Spiegel");
            manga.Pagination.LastVisiblePage.Should().BeGreaterThan(2350);
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetCharactersAsync_InvalidPage_ShouldThrowValidationException(int page)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetCharactersAsync(page, ParameterConsts.MaximumPageSize));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(26)]
        [InlineData(int.MaxValue)]
        public async Task GetCharactersAsync_InvalidPageSize_ShouldThrowValidationException(int pageSize)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetCharactersAsync(1, pageSize));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task GetCharactersAsync_GivenSecondPage_ShouldReturnSecondPage()
        {
            // When
            var manga = await _jikan.GetCharactersAsync(2, ParameterConsts.MaximumPageSize);

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            manga.Data.First().Name.Should().Be("Killua Zoldyck");
            manga.Pagination.LastVisiblePage.Should().BeGreaterThan(2350);
        }
        
        [Fact]
        public async Task GetCharactersAsync_GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
        {
            // Given
            const int pageSize = 5;
            
            // When
            var manga = await _jikan.GetCharactersAsync(1, pageSize);

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(pageSize);
            manga.Data.Skip(1).First().Name.Should().Be("Spike Spiegel");
        }
        
        [Fact]
        public async Task GetCharactersAsync_GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
        {
            // Given
            const int pageSize = 5;
            
            // When
            var manga = await _jikan.GetCharactersAsync(2, pageSize);

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(pageSize);
            manga.Data.First().Name.Should().Be("Ichigo Kurosaki");
        }
    }
}