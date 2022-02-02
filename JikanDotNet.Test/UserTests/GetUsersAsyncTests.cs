using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet.Consts;
using Xunit;


namespace JikanDotNet.Tests.UserTests
{
    public class GetUsersAsyncTests
    {
        private readonly IJikan _jikan;

        public GetUsersAsyncTests()
        {
            _jikan = new Jikan();
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetUsersAsync_InvalidPage_ShouldThrowValidationException(int page)
        {
            // When
            var func = _jikan.Awaiting(x => x.GetUsersAsync(page));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task GetUsersAsync_NoPage_ShouldReturnCollectionOfUsers()
        {
            // When
            var users = await _jikan.GetUsersAsync(1);

            // Then
            users.Data.Should().HaveCount(20);
            users.Data.First().Username.Should().Be("DemonEliot");
            users.Data.First().Images.Should().NotBeNull();
        }
        
        [Fact]
        public async Task GetUsersAsync_FirstPage_ShouldReturnFirstPageOfUsers()
        {
            // When
            var users = await _jikan.GetUsersAsync(1);

            // Then
            users.Data.Should().HaveCount(20);
            users.Data.First().Username.Should().Be("Shaneeyy");
            users.Data.First().Images.Should().NotBeNull();
        }
        
        [Fact]
        public async Task GetUsersAsync_SecondPage_ShouldReturnSecondPageOfUsers()
        {
            // When
            var users = await _jikan.GetUsersAsync(2);

            // Then
            users.Data.Should().HaveCount(20);
            users.Data.First().Username.Should().Be("susanime");
            users.Data.First().Images.Should().NotBeNull();
        }
    }
}