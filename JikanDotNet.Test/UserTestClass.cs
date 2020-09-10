using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class UserTestClass
	{
		private readonly IJikan _jikan;

		public UserTestClass()
		{
			_jikan = new Jikan(true);
		}

		[Fact]
		public async Task GetUserProfile_Ervelan_ShouldParseErvelanProfile()
		{
			UserProfile user = await _jikan.GetUserProfile("Ervelan");

			Assert.NotNull(user);
			Assert.Equal("Ervelan", user.Username);
			Assert.Equal(289183, user.UserId);
			Assert.Equal(2010, user.Joined.Value.Year);
			Assert.True(user.AnimeStatistics.Completed > 500);
			Assert.Contains("Haibane Renmei", user.Favorites.Anime.Select(x => x.Name));
			Assert.Contains("Oshino, Shinobu", user.Favorites.Characters.Select(x => x.Name));
		}

		[Fact]
		public async Task GetUserProfile_Nekomata1037_ShouldParseNekomataProfile()
		{
			UserProfile user = await _jikan.GetUserProfile("Nekomata1037");

			Assert.NotNull(user);
			Assert.Equal("Nekomata1037", user.Username);
			Assert.Equal(4901676, user.UserId);
			Assert.Equal(2015, user.Joined.Value.Year);
			Assert.True(user.AnimeStatistics.TotalEntries > 700);
			Assert.Contains("Steins;Gate", user.Favorites.Anime.Select(x => x.Name));
		}

		[Fact]
		public async Task GetUserHistory_Nekomata_ShouldParseNekomataHistory()
		{
			UserHistory userHistory = await _jikan.GetUserHistory("Nekomata1037");

			Assert.NotNull(userHistory);
			Assert.True(userHistory.History.Count >= 0);
		}

		[Fact]
		public async Task GetUserHistory_ErvelanMangaHistory_ShouldParseErvelanMangaHistory()
		{
			UserHistory userHistory = await _jikan.GetUserHistory("Ervelan", UserHistoryExtension.Manga);

			Assert.NotNull(userHistory);
			Assert.True(userHistory.History.Count >= 0);
		}

		[Fact]
		public async Task GetUserFriends_Ervelan_ShouldParseErvelanFriends()
		{
			UserFriends friends = await _jikan.GetUserFriends("Ervelan");

			Assert.NotNull(friends);
			Assert.True(friends.Friends.Count > 20);
			Assert.Contains("SonMati", friends.Friends.Select(x => x.Username));
			Assert.Contains("Progeusz", friends.Friends.Select(x => x.Username));
		}

		[Fact]
		public void GetUserFriends_ErvelanTenthPage_ShouldReturnNoFriends()
		{
			Assert.ThrowsAnyAsync<JikanRequestException>(() => _jikan.GetUserFriends("Ervelan", 10));
		}
	}
}