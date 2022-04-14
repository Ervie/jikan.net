using System;
using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet.Consts;
using Xunit;

namespace JikanDotNet.Tests.CharacterTests
{
    public class SearchCharacterAsyncTests
    {
        private readonly IJikan _jikan;

        public SearchCharacterAsyncTests()
        {
            _jikan = new Jikan();
        }

        [Fact]
        public async Task SearchCharacterAsync_EmptyConfig_ShouldReturnFirst25People()
        {
            // Given
            var config = new CharacterSearchConfig();
            
            // When
            var characters = await _jikan.SearchCharacterAsync(config);

            // Then
            using var _ = new AssertionScope();
            characters.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            characters.Data.First().Name.Should().Be("Spike Spiegel");
            characters.Data.First().NameKanji.Should().Be("スパイク・スピーゲル");
            characters.Pagination.LastVisiblePage.Should().BeGreaterThan(2350);
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task SearchCharacterAsync_InvalidPage_ShouldThrowValidationException(int page)
        {
            // Given
            var config = new CharacterSearchConfig{Page = page};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchCharacterAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(26)]
        [InlineData(int.MaxValue)]
        public async Task SearchCharacterAsync_InvalidPageSize_ShouldThrowValidationException(int pageSize)
        {
            // Given
            var config = new CharacterSearchConfig{PageSize = pageSize};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchCharacterAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task SearchCharacterAsync_GivenSecondPage_ShouldReturnSecondPage()
        {
            // Given
            var config = new CharacterSearchConfig{Page = 2};
            
            // When
            var characters = await _jikan.SearchCharacterAsync(config);

            // Then
            using var _ = new AssertionScope();
            characters.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            characters.Data.First().Name.Should().Be("Leorio Paladiknight");
            characters.Data.First().NameKanji.Should().StartWith("レオリオ=パラディナｲﾄ");
            characters.Pagination.LastVisiblePage.Should().BeGreaterThan(2350);
            characters.Pagination.CurrentPage.Should().Be(2);
            characters.Pagination.Items.Count.Should().Be(25);
            characters.Pagination.Items.PerPage.Should().Be(25);
        }
        
        [Fact]
        public async Task SearchCharacterAsync_GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
        {
            // Given
            const int pageSize = 5;
            var config = new CharacterSearchConfig{PageSize = pageSize};
            
            // When
            var characters = await _jikan.SearchCharacterAsync(config);

            // Then
            using var _ = new AssertionScope();
            characters.Data.Should().HaveCount(pageSize);
            characters.Data.First().Name.Should().Be("Spike Spiegel");
            characters.Data.First().NameKanji.Should().Be("スパイク・スピーゲル");
            characters.Pagination.CurrentPage.Should().Be(1);
            characters.Pagination.Items.Count.Should().Be(pageSize);
            characters.Pagination.Items.PerPage.Should().Be(pageSize);
        }
        
        [Fact]
        public async Task SearchCharacterAsync_GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
        {
            // Given
            const int pageSize = 5;
            var config = new CharacterSearchConfig{PageSize = pageSize, Page = 2};
            
            // When
            var characters = await _jikan.SearchCharacterAsync(config);

            // Then
            using var _ = new AssertionScope();
            characters.Data.Should().HaveCount(pageSize);
            characters.Data.First().Name.Should().Be("Rukia Kuchiki");
            characters.Pagination.CurrentPage.Should().Be(2);
            characters.Pagination.Items.Count.Should().Be(pageSize);
            characters.Pagination.Items.PerPage.Should().Be(pageSize);
        }
        
        [Theory]
        [InlineData('1')]
        [InlineData('0')]
        [InlineData('[')]
        [InlineData('\n')]
        [InlineData('_')]
        public async Task SearchCharacterAsync_InvalidLetter_ShouldThrowValidationException(char notLetter)
        {
            // Given
            var config = new CharacterSearchConfig{Letter = notLetter};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchCharacterAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData('A')]
        [InlineData('L')]
        [InlineData('S')]
        public async Task SearchCharacterAsync_ValidLetter_ShouldReturnRecordsOnlyStartingOnLetter(char letter)
        {
            // Given
            var config = new CharacterSearchConfig{Letter = letter};
            
            // When
            var people = await _jikan.SearchCharacterAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            people.Data.Should().OnlyContain(x => x.Name.StartsWith(letter));
        }
        
        [Fact]
        public async Task SearchCharacterAsync_LupinQuery_ShouldReturnLupins()
        {
            // Given
            var config = new CharacterSearchConfig{Query = "Lupin"};
            
            // When
            var people = await _jikan.SearchCharacterAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().Contain(x => x.Name.Equals("Fake Lupin"));
            people.Data.Should().Contain(x => x.Name.Equals("Arsène Lupin"));
            people.Data.Should().Contain(x => x.Name.Equals("Lupin II"));
            people.Data.First().Name.Should().Be("Lupin");
        }
        
        [Fact]
        public async Task SearchCharacterAsync_LupinQueryByPopularity_ShouldReturnLupinsWithKurobaFirst()
        {
            // Given
            var config = new CharacterSearchConfig{Query = "Lupin", OrderBy = CharacterSearchOrderBy.Favorites, SortDirection = SortDirection.Descending};
            
            // When
            var people = await _jikan.SearchCharacterAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().Contain(x => x.Name.Equals("Fake Lupin"));
            people.Data.Should().Contain(x => x.Name.Equals("Arsène Lupin"));
            people.Data.Should().Contain(x => x.Name.Equals("Lupin II"));
            people.Data.First().Name.Should().Be("Lupin");
        }
        
        [Fact]
        public async Task SearchCharacterAsync_LupinQueryByReversePopularity_ShouldReturnLupinsWithKurobaLast()
        {
            // Given
            var config = new CharacterSearchConfig{Query = "Lupin", OrderBy = CharacterSearchOrderBy.Favorites, SortDirection = SortDirection.Ascending};
            
            // When
            var people = await _jikan.SearchCharacterAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().Contain(x => x.Name.Equals("Fake Lupin"));
            people.Data.Should().Contain(x => x.Name.Equals("Arsène Lupin"));
            people.Data.Should().Contain(x => x.Name.Equals("Lupin II"));
            people.Data.Last().Name.Should().Be("Arsene Lupin III");
            people.Data.First().Favorites.Should().BeGreaterOrEqualTo(0);
        }
        
        [Fact]
        public async Task SearchCharacterAsync_LupinQueryByMalIdWithLimit2_ShouldReturnThirdAndKaitoKuroba()
        {
            // Given
            var config = new CharacterSearchConfig{Query = "Lupin", OrderBy = CharacterSearchOrderBy.MalId, PageSize = 2};
            
            // When
            var people = await _jikan.SearchCharacterAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().HaveCount(config.PageSize.Value);
            people.Data.Should().Contain(x => x.Name.Equals("Lupin"));
        }
        
        [Fact]
        public async Task SearchCharacterAsync_KirumiQuery_ShouldReturnSingleKirumi()
        {
            // Given
            var config = new CharacterSearchConfig{Query = "kirumi to"};
            
            // When
            var people = await _jikan.SearchCharacterAsync(config);

            // Then
            using var _ = new AssertionScope();
            people.Data.Should().ContainSingle();

            var result = people.Data.First();
            result.Name.Should().Be("Kirumi Toujou");
            result.Nicknames.Should().BeEmpty();
            result.About.Should().NotBeEmpty();
            result.MalId.Should().Be(157604);
        }
    }
}