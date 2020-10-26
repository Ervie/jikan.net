using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class UserTests
	{
		private readonly IJikan _jikan;

		public UserTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserProfile_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			Func<Task<UserProfile>> func = _jikan.Awaiting(x => x.GetUserProfile(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserProfile_Ervelan_ShouldParseErvelanProfile()
		{
			// When
			var user = await _jikan.GetUserProfile("Ervelan");

			// Then
			using (new AssertionScope())
			{
				user.Should().NotBeNull();
				user.Username.Should().Be("Ervelan");
				user.UserId.Should().Be(289183);
				user.Joined.Value.Year.Should().Be(2010);
				user.AnimeStatistics.Completed.Should().BeGreaterThan(500);
				user.Favorites.Anime.Select(x => x.Name).Should().Contain("Haibane Renmei");
				user.Favorites.Characters.Select(x => x.Name).Should().Contain("Oshino, Shinobu");
			}
		}

		[Fact]
		public async Task GetUserProfile_Nekomata1037_ShouldParseNekomataProfile()
		{
			// When
			var user = await _jikan.GetUserProfile("Nekomata1037");

			// Then
			using (new AssertionScope())
			{
				user.Should().NotBeNull();
				user.Username.Should().Be("Nekomata1037");
				user.UserId.Should().Be(4901676);
				user.Joined.Value.Year.Should().Be(2015);
				user.AnimeStatistics.TotalEntries.Should().BeGreaterThan(700);
				user.Favorites.Anime.Select(x => x.Name).Should().Contain("Steins;Gate");
			}
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