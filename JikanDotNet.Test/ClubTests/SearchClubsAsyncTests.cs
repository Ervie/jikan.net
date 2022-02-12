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
    public class SearchClubAsyncTests
    {
        private readonly IJikan _jikan;

        public SearchClubAsyncTests()
        {
            _jikan = new Jikan();
        }

        [Fact]
        public async Task SearchClubAsync_EmptyConfig_ShouldReturnFirst25People()
        {
            // Given
            var config = new ClubSearchConfig();
            
            // When
            var clubs = await _jikan.SearchClubAsync(config);

            // Then
            using var _ = new AssertionScope();
            clubs.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            clubs.Data.First().Name.Should().Be("Cowboy Bebop");
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task SearchClubAsync_InvalidPage_ShouldThrowValidationException(int page)
        {
            // Given
            var config = new ClubSearchConfig{Page = page};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchClubAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(26)]
        [InlineData(int.MaxValue)]
        public async Task SearchClubAsync_InvalidPageSize_ShouldThrowValidationException(int pageSize)
        {
            // Given
            var config = new ClubSearchConfig{PageSize = pageSize};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchClubAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task SearchClubAsync_GivenSecondPage_ShouldReturnSecondPage()
        {
            // Given
            var config = new ClubSearchConfig{Page = 2};
            
            // When
            var characters = await _jikan.SearchClubAsync(config);

            // Then
            characters.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
        }
        
        [Fact]
        public async Task SearchClubAsync_GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
        {
            // Given
            const int pageSize = 5;
            var config = new ClubSearchConfig{PageSize = pageSize};
            
            // When
            var characters = await _jikan.SearchClubAsync(config);

            // Then
            using var _ = new AssertionScope();
            characters.Data.Should().HaveCount(pageSize);
            characters.Data.Skip(1).First().Name.Should().Be("Cowboy Bebop");
        }
        
        [Fact]
        public async Task SearchCharacterAsync_GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
        {
            // Given
            const int pageSize = 5;
            var config = new ClubSearchConfig{PageSize = pageSize, Page = 2};
            
            // When
            var characters = await _jikan.SearchClubAsync(config);

            // Then
            using var _ = new AssertionScope();
            characters.Data.Should().HaveCount(pageSize);
            characters.Data.First().Name.Should().Be("Anime Cafe");
        }
        
        [Theory]
        [InlineData('1')]
        [InlineData('0')]
        [InlineData('[')]
        [InlineData('\n')]
        [InlineData('_')]
        public async Task SearchClubAsync_InvalidLetter_ShouldThrowValidationException(char notLetter)
        {
            // Given
            var config = new ClubSearchConfig{Letter = notLetter};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchClubAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData('A')]
        [InlineData('L')]
        [InlineData('S')]
        public async Task SearchClubAsync_ValidLetter_ShouldReturnRecordsOnlyStartingOnLetter(char letter)
        {
            // Given
            var config = new ClubSearchConfig{Letter = letter};
            
            // When
            var people = await _jikan.SearchClubAsync(config);

            // Then
            people.Data.Should().OnlyContain(x => x.Name.StartsWith(letter));
        }
    }
}