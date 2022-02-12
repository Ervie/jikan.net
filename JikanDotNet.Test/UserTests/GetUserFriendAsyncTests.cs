using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
	public class GetUserFriendsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetUserFriendsAsyncTests()
		{
			_jikan = new Jikan();
		}


		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserFriendsAsync_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserFriendsAsync(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserFriendsAsync_Ervelan_ShouldParseErvelanFriends()
		{
			// When
			var friends = await _jikan.GetUserFriendsAsync("Ervelan");

			// Then
			var friendUsernames = friends.Data.Select(x => x.User.Username);
			using (new AssertionScope())
			{
				friends.Should().NotBeNull();
				friends.Data.Count.Should().BeGreaterThan(20);
				friendUsernames.Should().Contain("SonMati");
				friendUsernames.Should().Contain("Progeusz");
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserFriendsAsync_InvalidUsernameWithPage_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserFriendsAsync(username, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetUserFriendsAsync_ValidUsernameWithInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserFriendsAsync("Ervelan", page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public void GetUserFriendsAsync_ErvelanTenthPage_ShouldReturnNoFriends()
		{
			// When
			var action = _jikan.Awaiting(x => x.GetUserFriendsAsync("Ervelan", 10));

			action.Should().ThrowAsync<JikanRequestException>();
		}
	}
}
