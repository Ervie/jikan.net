using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet.Consts;
using Xunit;

namespace JikanDotNet.Tests.PersonTests
{
    public class GetPeopleAsyncTests
    {
        private readonly IJikan _jikan;

        public GetPeopleAsyncTests()
        {
            _jikan = new Jikan();
        }

        [Fact]
        public async Task GetPeopleAsync_NoParameter_ShouldReturnFirst25Anime()
        {
            // When
            var manga = await _jikan.GetPeopleAsync();

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            manga.Data.First().Name.Should().Be("Tomokazu Seki");
            manga.Pagination.LastVisiblePage.Should().BeGreaterThan(370);
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetPeopleAsync_InvalidPage_ShouldThrowValidationException(int page)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetPeopleAsync(page, ParameterConsts.MaximumPageSize));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(26)]
        [InlineData(int.MaxValue)]
        public async Task GetPeopleAsync_InvalidPageSize_ShouldThrowValidationException(int pageSize)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetPeopleAsync(1, pageSize));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task GetPeopleAsync_GivenSecondPage_ShouldReturnSecondPage()
        {
            // When
            var manga = await _jikan.GetPeopleAsync(2, ParameterConsts.MaximumPageSize);

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            manga.Data.First().Name.Should().Be("Travis Willingham");
            manga.Pagination.LastVisiblePage.Should().BeGreaterThan(370);
        }
        
        [Fact]
        public async Task GetPeopleAsync_GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
        {
            // Given
            const int pageSize = 5;
            
            // When
            var manga = await _jikan.GetPeopleAsync(1, pageSize);

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(pageSize);
            manga.Data.First().Name.Should().Be("Tomokazu Seki");
        }
        
        [Fact]
        public async Task GetPeopleAsync_GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
        {
            // Given
            const int pageSize = 5;
            
            // When
            var manga = await _jikan.GetPeopleAsync(2, pageSize);

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(pageSize);
            manga.Data.First().Name.Should().Be("Toshiyuki Morikawa");
        }
    }
}