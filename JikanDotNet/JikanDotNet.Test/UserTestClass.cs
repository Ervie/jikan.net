using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class UserTestClass
	{
		private readonly IJikan jikan;

		public UserTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public void ShouldParseErvelanProfile()
		{
			UserProfile user = Task.Run(() => jikan.GetUserProfile("Ervelan")).Result;

			Assert.NotNull(user);
			Assert.Equal("Ervelan", user.Username);
			Assert.Equal(2010, user.Joined.Value.Year);
			Assert.True(user.AnimeStatistics.Completed > 500);
			Assert.Contains("Haibane Renmei", user.Favorites.Anime.Select(x => x.Name));
			Assert.Contains("Oshino, Shinobu", user.Favorites.Characters.Select(x => x.Name));
		}

		[Fact]
		public void ShouldParseNekomataProfile()
		{
			UserProfile user = Task.Run(() => jikan.GetUserProfile("Nekomata1037")).Result;

			Assert.NotNull(user);
			Assert.Equal("Nekomata1037", user.Username);
			Assert.Equal(2015, user.Joined.Value.Year);
			Assert.True(user.AnimeStatistics.TotalEntries > 700);
			Assert.Contains("Steins;Gate", user.Favorites.Anime.Select(x => x.Name));
		}

		[Fact]
		public void ShouldParseErvelanHistory()
		{
			UserHistory userHistory = Task.Run(() => jikan.GetUserHistory("Ervelan")).Result;

			Assert.NotNull(userHistory);
			Assert.True(userHistory.History.Count > 10);
			Assert.True(userHistory.History.First().Date.Value.Year > 2017);
		}

		[Fact]
		public void ShouldParseErvelanMangaHistory()
		{
			UserHistory userHistory = Task.Run(() => jikan.GetUserHistory("Ervelan", UserHistoryExtension.Manga)).Result;

			Assert.NotNull(userHistory);
			Assert.True(userHistory.History.Count > 5);
			Assert.True(userHistory.History.First().Date.Value.Year > 2017);
		}

		[Fact]
		public void ShouldParseErvelanFriends()
		{
			UserFriends friends = Task.Run(() => jikan.GetUserFriends("Ervelan")).Result;

			Assert.NotNull(friends);
			Assert.True(friends.Friends.Count > 20);
			Assert.Contains("SonMati", friends.Friends.Select(x => x.Username));
			Assert.Contains("Progeusz", friends.Friends.Select(x => x.Username));
		}

		[Fact]
		public void ShouldReturnNull()
		{
			UserFriends friends = Task.Run(() => jikan.GetUserFriends("Ervelan", 10)).Result;

			Assert.Null(friends);
		}
	}
}