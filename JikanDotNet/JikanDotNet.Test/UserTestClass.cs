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
		public void ShouldReturnNullHistory()
		{
			UserFriends friends = Task.Run(() => jikan.GetUserFriends("Ervelan", 10)).Result;

			Assert.Null(friends);
		}

		[Fact]
		public void ShouldParseErvelanAnimeList()
		{
			UserAnimeList animeList = Task.Run(() => jikan.GetUserAnimeList("Ervelan")).Result;

			Assert.NotNull(animeList);
			Assert.Equal(300, animeList.Anime.Count);
			Assert.Contains("Akira", animeList.Anime.Select(x => x.Title));
		}

		[Fact]
		public void ShouldParseErvelanAnimeWatchingList()
		{
			UserAnimeList animeList = Task.Run(() => jikan.GetUserAnimeList("Ervelan", UserAnimeListExtension.Watching)).Result;

			Assert.NotNull(animeList);
			Assert.Equal(UserAnimeListExtension.Watching, animeList.Anime.First().WatchingStatus);
			Assert.Equal(AiringStatus.Airing, animeList.Anime.First().AiringStatus);
		}

		[Fact]
		public void ShouldParseErvelanDroppedList()
		{
			UserAnimeList animeList = Task.Run(() => jikan.GetUserAnimeList("Ervelan", UserAnimeListExtension.Dropped)).Result;

			Assert.NotNull(animeList);
			Assert.True(animeList.Anime.Count > 5);
			Assert.Contains("Coppelion", animeList.Anime.Select(x => x.Title));
		}

		[Fact]
		public void ShouldParseErvelanAnimeListSecondPage()
		{
			UserAnimeList animeList = Task.Run(() => jikan.GetUserAnimeList("Ervelan", 2)).Result;

			Assert.NotNull(animeList);
			Assert.Equal(300, animeList.Anime.Count);
		}

		[Fact]
		public void ShouldParseOnrixAnimeList()
		{
			UserAnimeList animeList = Task.Run(() => jikan.GetUserAnimeList("onrix")).Result;

			Assert.NotNull(animeList);
			Assert.Equal(122, animeList.Anime.Count);
		}

		[Fact]
		public void ShouldParseErvelanMangaList()
		{
			UserMangaList mangaList = Task.Run(() => jikan.GetUserMangaList("Ervelan")).Result;

			Assert.NotNull(mangaList);
			Assert.True(mangaList.Manga.Count > 90);
			Assert.Contains("Dr. Stone", mangaList.Manga.Select(x => x.Title));
		}

		[Fact]
		public void ShouldParseErvelanMangaReadingList()
		{
			UserMangaList mangaList = Task.Run(() => jikan.GetUserMangaList("Ervelan", UserMangaListExtension.Reading)).Result;

			Assert.NotNull(mangaList);
			Assert.Contains("One Piece", mangaList.Manga.Select(x => x.Title));
			Assert.Equal(UserMangaListExtension.Reading, mangaList.Manga.First().ReadingStatus);
			Assert.Equal(AiringStatus.Airing, mangaList.Manga.First().PublishingStatus);
		}

		[Fact]
		public void ShouldParseErvelanMangaDroppedList()
		{
			UserMangaList mangaList = Task.Run(() => jikan.GetUserMangaList("Ervelan", UserMangaListExtension.Dropped)).Result;

			Assert.NotNull(mangaList);
			Assert.Equal(3, mangaList.Manga.Count);
			Assert.Contains("D.Gray-man", mangaList.Manga.Select(x => x.Title));
		}

		[Fact]
		public void ShouldParseOnrixMangaList()
		{
			UserMangaList mangaList = Task.Run(() => jikan.GetUserMangaList("onrix")).Result;

			Assert.Null(mangaList);
		}

		[Fact]
		public void ShouldParseMithogawaMangaList()
		{
			UserMangaList mangaList = Task.Run(() => jikan.GetUserMangaList("Mithogawa")).Result;

			Assert.NotNull(mangaList);
			Assert.Equal(300, mangaList.Manga.Count);
		}
	}
}