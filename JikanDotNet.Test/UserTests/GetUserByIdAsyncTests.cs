using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
    [Collection("JikanTests")]
    public class GetUserByIdAsyncTests
    {
        private readonly IJikan _jikan;

        public GetUserByIdAsyncTests(JikanFixture jikanFixture)
        {
            _jikan = jikanFixture.Jikan;
        }

        [Fact]
        public async Task GetUserByIdAsync_ValidId_ReturnsUserProfile()
        {
            // Arrange
            long userId = 1; // Example MAL user id

            // Act
            var result = await _jikan.GetUserByIdAsync(userId);

            // Assert
            using var _ = new AssertionScope();
            result.Should().NotBeNull();
            result.Data.Should().NotBeNull();
            result.Data.Username.Should().NotBeNullOrWhiteSpace();
        }
    }
}
