using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet.Consts;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
    public class SearchUserAsyncTests
    {
        private readonly IJikan _jikan;

        public SearchUserAsyncTests()
        {
            _jikan = new Jikan();
        }

        [Fact]
        public async Task SearchUserAsync_EmptyConfig_ShouldReturnFirst25People()
        {
            // Given
            var config = new UserSearchConfig();
            
            // When
            var users = await _jikan.SearchUserAsync(config);

            // Then
            using var _ = new AssertionScope();
            users.Data.Should().HaveCount(20);
            users.Data.First().Username.Should().Be("ErickGabriel555");
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task SearchUserAsync_InvalidPage_ShouldThrowValidationException(int page)
        {
            // Given
            var config = new UserSearchConfig{Page = page};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchUserAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task SearchUserAsync_GivenSecondPage_ShouldReturnSecondPage()
        {
            // Given
            var config = new UserSearchConfig{Page = 2};
            
            // When
            var users = await _jikan.SearchUserAsync(config);

            // Then
            using var _ = new AssertionScope();
            users.Data.Should().HaveCount(20);
            users.Data.First().Username.Should().Be("EnricoA128");
            users.Data.First().Images.Should().NotBeNull();
        }
        
        [Theory]
        [InlineData((UserGender)int.MinValue)]
        [InlineData((UserGender)int.MaxValue)]
        public async Task SearchUserAsync_InvalidGenderEnumValue_ShouldThrowValidationException(UserGender gender)
        {
            // Given
            var config = new UserSearchConfig{Gender = gender};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchUserAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData(UserGender.Any, "ErickGabriel555")]
        [InlineData(UserGender.Male, "ErickGabriel555")]
        [InlineData(UserGender.Female, "ErickGabriel555")]
        [InlineData(UserGender.NonBinary, "ErickGabriel555")]
        public async Task SearchUserAsync_ValidGenderEnumValue_ShouldReturnUsers(UserGender gender, string expectedFirstUser)
        {
            // Given
            var config = new UserSearchConfig{Gender = gender};
            
            // When
            var users = await _jikan.SearchUserAsync(config);

            // Then
            using var _ = new AssertionScope();
            users.Data.Should().HaveCount(20);
            users.Data.First().Username.Should().Be(expectedFirstUser);
        }
        
        [Fact]
        public async Task SearchUserAsync_SonMatiQuery_ShouldReturnSonMati()
        {
            // Given
            var config = new UserSearchConfig{Query = "SonMati"};
            
            // When
            var users = await _jikan.SearchUserAsync(config);

            // Then
            users.Data.Should().Contain(x => x.Username.Equals("SonMati") && x.Url.Equals("https://myanimelist.net/profile/SonMati"));
        }
        
        [Fact]
        public async Task SearchUserAsync_WithLocation_ShouldReturnFilteredByLocation()
        {
            // Given
            var config = new UserSearchConfig{Location = "mysłowice"};
            
            // When
            var users = await _jikan.SearchUserAsync(config);

            // Then
            using var _ = new AssertionScope();
            users.Data.Should().HaveCount(20);
            users.Data.First().Username.Should().Be("P3gueiA1ds");
        }
        
        [Fact]
        public async Task SearchUserAsync_WithMinAge_ShouldReturnFilteredByMinAge()
        {
            // Given
            var config = new UserSearchConfig{MinAge = 20};
            
            // When
            var users = await _jikan.SearchUserAsync(config);

            // Then
            using var _ = new AssertionScope();
            users.Data.Should().HaveCount(20);
            users.Data.First().Username.Should().Be("DeafExorcist");
        }
        
        [Fact]
        public async Task SearchUserAsync_WithMaxAge_ShouldReturnFilteredByMaxAge()
        {
            // Given
            var config = new UserSearchConfig{MaxAge = 20};
            
            // When
            var users = await _jikan.SearchUserAsync(config);

            // Then
            using var _ = new AssertionScope();
            users.Data.Should().HaveCount(20);
            users.Data.First().Username.Should().Be("KarlaD05");
        }
    }
}