using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class UserTestss
	{
		private readonly IJikan _jikan;

		public UserTestss()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserHistory_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			Func<Task<UserHistory>> func = _jikan.Awaiting(x => x.GetUserHistory(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserHistory_Nekomata_ShouldParseNekomataHistory()
		{
			// When
			var userHistory = await _jikan.GetUserHistory("Nekomata1037");

			// Then
			using (new AssertionScope())
			{
				userHistory.Should().NotBeNull();
				userHistory.History.Count.Should().BeGreaterOrEqualTo(0);
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserHistory_InvalidUsernameWithExtension_ShouldThrowValidationException(string username)
		{
			// When
			Func<Task<UserHistory>> func = _jikan.Awaiting(x => x.GetUserHistory(username, UserHistoryExtension.Manga));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData((UserHistoryExtension)int.MaxValue)]
		[InlineData((UserHistoryExtension)int.MinValue)]
		public async Task GetUserHistory_ErvelanWithInvalidExtension_ShouldThrowValidationException(UserHistoryExtension userHistoryExtension)
		{
			// When
			Func<Task<UserHistory>> func = _jikan.Awaiting(x => x.GetUserHistory("Ervelan", userHistoryExtension));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserHistory_ErvelanMangaHistory_ShouldParseErvelanMangaHistory()
		{
			// When
			var userHistory = await _jikan.GetUserHistory("Ervelan", UserHistoryExtension.Manga);

			// Then
			using (new AssertionScope())
			{
				userHistory.Should().NotBeNull();
				userHistory.History.Count.Should().BeGreaterOrEqualTo(0);
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserFriends_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			Func<Task<UserFriends>> func = _jikan.Awaiting(x => x.GetUserFriends(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserFriends_Ervelan_ShouldParseErvelanFriends()
		{
			// When
			var friends = await _jikan.GetUserFriends("Ervelan");

			// Then
			var friendUsernames = friends.Friends.Select(x => x.Username);
			using (new AssertionScope())
			{
				friends.Should().NotBeNull();
				friends.Friends.Count.Should().BeGreaterThan(20);
				friendUsernames.Should().Contain("SonMati");
				friendUsernames.Should().Contain("Progeusz");
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserFriends_InvalidUsernameWithPage_ShouldThrowValidationException(string username)
		{
			// When
			Func<Task<UserFriends>> func = _jikan.Awaiting(x => x.GetUserFriends(username, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetUserFriends_ValidUsernameWithInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<UserFriends>> func = _jikan.Awaiting(x => x.GetUserFriends("Ervelan", page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public void GetUserFriends_ErvelanTenthPage_ShouldReturnNoFriends()
		{
			// When
			var action = _jikan.Awaiting(x => x.GetUserFriends("Ervelan", 10));

			action.Should().ThrowAsync<JikanRequestException>();
		}
	}
}