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
		public async Task GetUserProfile_Ervelan_ShouldParseErvelanProfile()
		{
			UserProfile user = await jikan.GetUserProfile("Ervelan");

			Assert.NotNull(user);
			Assert.Equal("Ervelan", user.Username);
			Assert.Equal(2010, user.Joined.Value.Year);
			Assert.True(user.AnimeStatistics.Completed > 500);
			Assert.Contains("Haibane Renmei", user.Favorites.Anime.Select(x => x.Name));
			Assert.Contains("Oshino, Shinobu", user.Favorites.Characters.Select(x => x.Name));
		}

		[Fact]
		public async Task GetUserProfile_Nekomata1037_ShouldParseNekomataProfile()
		{
			UserProfile user = await jikan.GetUserProfile("Nekomata1037");

			Assert.NotNull(user);
			Assert.Equal("Nekomata1037", user.Username);
			Assert.Equal(2015, user.Joined.Value.Year);
			Assert.True(user.AnimeStatistics.TotalEntries > 700);
			Assert.Contains("Steins;Gate", user.Favorites.Anime.Select(x => x.Name));
		}

		[Fact]
		public async Task GetUserHistory_Nekomata_ShouldParseNekomataHistory()
		{
			UserHistory userHistory = await jikan.GetUserHistory("Nekomata1037");

			Assert.NotNull(userHistory);
			Assert.True(userHistory.History.Count >= 0);
		}

		[Fact]
		public async Task GetUserHistory_NekomataMangaHistory_ShouldParseNekomataMangaHistory()
		{
			UserHistory userHistory = await jikan.GetUserHistory("Nekomata1037", UserHistoryExtension.Manga);

			Assert.NotNull(userHistory);
			Assert.True(userHistory.History.Count >= 0);
		}

		[Fact]
		public async Task GetUserFriends_Ervelan_ShouldParseErvelanFriends()
		{
			UserFriends friends = await jikan.GetUserFriends("Ervelan");

			Assert.NotNull(friends);
			Assert.True(friends.Friends.Count > 20);
			Assert.Contains("SonMati", friends.Friends.Select(x => x.Username));
			Assert.Contains("Progeusz", friends.Friends.Select(x => x.Username));
		}

		[Fact]
		public async Task GetUserFriends_ErvelanTenthPage_ShouldReturnNoFriends()
		{
			UserFriends friends = await jikan.GetUserFriends("Ervelan", 10);

			Assert.Null(friends);
		}

		[Fact]
		public async Task GetUserAnimeList_Ervelan_ShouldParseErvelanAnimeList()
		{
			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan");

			Assert.NotNull(animeList);
			Assert.Equal(300, animeList.Anime.Count);
			Assert.Contains("Akira", animeList.Anime.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanWatching_ShouldParseErvelanAnimeWatchingList()
		{
			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", UserAnimeListExtension.Watching);

			Assert.NotNull(animeList);
			Assert.Equal(UserAnimeListExtension.Watching, animeList.Anime.First().WatchingStatus);
			Assert.Equal(AiringStatus.Airing, animeList.Anime.First().AiringStatus);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanDropped_ShouldParseErvelanDroppedList()
		{
			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", UserAnimeListExtension.Dropped);

			Assert.NotNull(animeList);
			Assert.True(animeList.Anime.Count > 5);
			Assert.Contains("Coppelion", animeList.Anime.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSecondPage_ShouldParseErvelanAnimeListSecondPage()
		{
			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", 2);

			Assert.NotNull(animeList);
			Assert.Equal(300, animeList.Anime.Count);
		}

		[Fact]
		public async Task GetUserAnimeList_onrix_ShouldParseOnrixAnimeList()
		{
			UserAnimeList animeList = await jikan.GetUserAnimeList("onrix");

			Assert.NotNull(animeList);
			Assert.Equal(122, animeList.Anime.Count);
		}

		[Fact]
		public async Task GetUserMangaList_Ervelan_ShouldParseErvelanMangaList()
		{
			UserMangaList mangaList = await jikan.GetUserMangaList("Ervelan");

			Assert.NotNull(mangaList);
			Assert.True(mangaList.Manga.Count > 90);
			Assert.Contains("Dr. Stone", mangaList.Manga.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserMangaList_ErvelanReading_ShouldParseErvelanMangaReadingList()
		{
			UserMangaList mangaList = await jikan.GetUserMangaList("Ervelan", UserMangaListExtension.Reading);

			Assert.NotNull(mangaList);
			Assert.Contains("One Piece", mangaList.Manga.Select(x => x.Title));
			Assert.Equal(UserMangaListExtension.Reading, mangaList.Manga.First().ReadingStatus);
			Assert.Equal(AiringStatus.Airing, mangaList.Manga.First().PublishingStatus);
		}

		[Fact]
		public async Task GetUserMangaList_ErvelanDropped_ShouldParseErvelanMangaDroppedList()
		{
			UserMangaList mangaList = await jikan.GetUserMangaList("Ervelan", UserMangaListExtension.Dropped);

			Assert.NotNull(mangaList);
			Assert.Equal(3, mangaList.Manga.Count);
			Assert.Contains("D.Gray-man", mangaList.Manga.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserMangaList_onrix_ShouldParseOnrixMangaList()
		{
			UserMangaList mangaList = await jikan.GetUserMangaList("onrix");

			Assert.Null(mangaList);
		}

		[Fact]
		public async Task GetUserMangaList_Mithogawa_ShouldParseMithogawaMangaList()
		{
			UserMangaList mangaList = await jikan.GetUserMangaList("Mithogawa");

			Assert.NotNull(mangaList);
			Assert.Equal(300, mangaList.Manga.Count);
		}
	}
}