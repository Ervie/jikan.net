using System;
using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet.Consts;
using Xunit;

namespace JikanDotNet.Tests.PersonTests
{
    public class SearchPersonAsyncTests
    {
        private readonly IJikan _jikan;

        public SearchPersonAsyncTests()
        {
            _jikan = new Jikan();
        }

        [Fact]
        public async Task SearchPersonAsync_EmptyConnfig_ShouldReturnFirst25People()
        {
            // Given
            var config = new PersonSearchConfig();
            
            // When
            var people = await _jikan.SearchPersonAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            people.Data.First().Name.Should().Be("Tomokazu Seki");
            people.Pagination.LastVisiblePage.Should().BeGreaterThan(370);
            people.Pagination.CurrentPage.Should().Be(1);
            people.Pagination.Items.Count.Should().Be(25);
            people.Pagination.Items.PerPage.Should().Be(25);
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task SearchPersonAsync_InvalidPage_ShouldThrowValidationException(int page)
        {
            // Given
            var config = new PersonSearchConfig{Page = page};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchPersonAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(26)]
        [InlineData(int.MaxValue)]
        public async Task SearchPersonAsync_InvalidPageSize_ShouldThrowValidationException(int pageSize)
        {
            // Given
            var config = new PersonSearchConfig{PageSize = pageSize};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchPersonAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task SearchPersonAsync_GivenSecondPage_ShouldReturnSecondPage()
        {
            // Given
            var config = new PersonSearchConfig{Page = 2};
            
            // When
            var people = await _jikan.SearchPersonAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            people.Data.First().Name.Should().Be("Travis Willingham");
            people.Pagination.LastVisiblePage.Should().BeGreaterThan(370);
            people.Pagination.CurrentPage.Should().Be(2);
            people.Pagination.Items.Count.Should().Be(25);
            people.Pagination.Items.PerPage.Should().Be(25);
        }
        
        [Fact]
        public async Task SearchPersonAsync_GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
        {
            // Given
            const int pageSize = 5;
            var config = new PersonSearchConfig{PageSize = pageSize};
            
            // When
            var people = await _jikan.SearchPersonAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().HaveCount(pageSize);
            people.Data.First().Name.Should().Be("Tomokazu Seki");
            people.Pagination.CurrentPage.Should().Be(1);
            people.Pagination.Items.Count.Should().Be(pageSize);
            people.Pagination.Items.PerPage.Should().Be(pageSize);
        }
        
        [Fact]
        public async Task SearchPersonAsync_GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
        {
            // Given
            const int pageSize = 5;
            var config = new PersonSearchConfig{PageSize = pageSize, Page = 2};
            
            // When
            var people = await _jikan.SearchPersonAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().HaveCount(pageSize);
            people.Data.First().Name.Should().Be("Toshiyuki Morikawa");
            people.Pagination.CurrentPage.Should().Be(2);
            people.Pagination.Items.Count.Should().Be(pageSize);
            people.Pagination.Items.PerPage.Should().Be(pageSize);
        }
        
        [Theory]
        [InlineData('1')]
        [InlineData('0')]
        [InlineData('[')]
        [InlineData('\n')]
        [InlineData('_')]
        public async Task SearchPersonAsync_InvalidLetter_ShouldThrowValidationException(char notLetter)
        {
            // Given
            var config = new PersonSearchConfig{Letter = notLetter};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchPersonAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData('A')]
        [InlineData('L')]
        [InlineData('S')]
        public async Task SearchPersonAsync_ValidLetter_ShouldReturnRecordsOnlyStartingOnLetter(char letter)
        {
            // Given
            var config = new PersonSearchConfig{Letter = letter};
            
            // When
            var people = await _jikan.SearchPersonAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            people.Data.Should().OnlyContain(x => x.Name.StartsWith(letter));
        }
        
        [Fact]
        public async Task SearchPersonAsync_KanaQuery_ShouldReturnKanas()
        {
            // Given
            var config = new PersonSearchConfig{Query = "Kana"};
            
            // When
            var people = await _jikan.SearchPersonAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().Contain(x => x.Name.Equals("Kana Hanazawa"));
            people.Data.Should().Contain(x => x.Name.Equals("Kana Ueda"));
            people.Data.Should().Contain(x => x.Name.Equals("Yukana"));
            people.Data.First().Name.Should().Be("Kana Ueda");
        }
        
        [Fact]
        public async Task SearchPersonAsync_KanaQueryByPopularity_ShouldReturnKanaWithHanazawaFirst()
        {
            // Given
            var config = new PersonSearchConfig{Query = "Kana", OrderBy = PersonSearchOrderBy.Favorites, SortDirection = SortDirection.Descending};
            
            // When
            var people = await _jikan.SearchPersonAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().Contain(x => x.Name.Equals("Kana Hanazawa"));
            people.Data.Should().Contain(x => x.Name.Equals("Kana Ueda"));
            people.Data.Should().Contain(x => x.Name.Equals("Yukana"));
            people.Data.First().Name.Should().Be("Kana Hanazawa");
        }
        
        [Fact]
        public async Task SearchPersonAsync_KanaQueryByReversePopularity_ShouldReturnKanaWithoutHanazawa()
        {
            // Given
            var config = new PersonSearchConfig{Query = "Kana", OrderBy = PersonSearchOrderBy.Favorites, SortDirection = SortDirection.Ascending};
            
            // When
            var people = await _jikan.SearchPersonAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().NotContain(x => x.Name.Equals("Kana Hanazawa"));
            people.Data.Should().NotContain(x => x.Name.Equals("Kana Ueda"));
            people.Data.Should().NotContain(x => x.Name.Equals("Yukana"));
            people.Data.Should().OnlyContain(x => x.Name.Contains("kana") || x.Name.Contains("Kana"));
            people.Data.First().MemberFavorites.Should().Be(0);
        }
        
        [Fact]
        public async Task SearchPersonAsync_KanaQueryByMalIdWithLimit2_ShouldReturnMikaKanaiAndKanakoSakai()
        {
            // Given
            var config = new PersonSearchConfig{Query = "Kana", OrderBy = PersonSearchOrderBy.MalId, PageSize = 2};
            
            // When
            var people = await _jikan.SearchPersonAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().HaveCount(config.PageSize.Value);
            people.Data.Should().Contain(x => x.Name.Equals("Mika Kanai"));
            people.Data.Should().Contain(x => x.Name.Equals("Kanako Sakai"));
        }
        
        [Fact]
        public async Task SearchPersonAsync_MiyukiSawaQuery_ShouldReturnSingleSawashiro()
        {
            // Given
            var config = new PersonSearchConfig{Query = "miyuki sawa"};
            
            // When
            var people = await _jikan.SearchPersonAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().ContainSingle();

            var result = people.Data.First();
            result.Name.Should().Be("Miyuki Sawashiro");
            result.GivenName.Should().Be("みゆき");
            result.FamilyName.Should().Be("沢城");
            result.Birthday.Should().BeSameDateAs(new DateTime(1985,6,2));
            result.MalId.Should().Be(99);
            result.WebsiteUrl.Should().BeNull();
        }
    }
}