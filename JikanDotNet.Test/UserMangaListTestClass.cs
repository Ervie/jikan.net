using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class UserMangaListTestClass
	{
		private readonly IJikan _jikan;

		public UserMangaListTestClass()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetUserMangaList_Ervelan_ShouldParseErvelanMangaList()
		{
			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan");

			Assert.NotNull(mangaList);
			Assert.True(mangaList.Manga.Count > 90);
			Assert.Contains("Dr. Stone", mangaList.Manga.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserMangaList_ErvelanReading_ShouldParseErvelanMangaReadingList()
		{
			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", UserMangaListExtension.Reading);

			Assert.NotNull(mangaList);
			Assert.Contains("One Piece", mangaList.Manga.Select(x => x.Title));
			Assert.Equal(UserMangaListExtension.Reading, mangaList.Manga.First().ReadingStatus);
			Assert.Equal(AiringStatus.Airing, mangaList.Manga.First().PublishingStatus);
		}

		[Fact]
		public async Task GetUserMangaList_ErvelanDropped_ShouldParseErvelanMangaDroppedList()
		{
			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", UserMangaListExtension.Dropped);

			Assert.NotNull(mangaList);
			Assert.Equal(3, mangaList.Manga.Count);
			Assert.Contains("D.Gray-man", mangaList.Manga.Select(x => x.Title));
		}

		[Fact]
		public void GetUserMangaList_onrix_ShouldParseOnrixMangaList()
		{
			Assert.ThrowsAnyAsync<JikanRequestException>(() => _jikan.GetUserMangaList("onrix"));
		}

		[Fact]
		public async Task GetUserMangaList_Mithogawa_ShouldParseMithogawaMangaList()
		{
			UserMangaList mangaList = await _jikan.GetUserMangaList("Mithogawa");

			Assert.NotNull(mangaList);
			Assert.Equal(300, mangaList.Manga.Count);
		}

		[Fact]
		public async Task GetUserMangaList_ErvelanFilterWithDeath_ShouldFindBothDeathNotes()
		{
			UserListMangaSearchConfig searchConfig = new UserListMangaSearchConfig()
			{
				Query = "death"
			};

			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", searchConfig);

			Assert.NotNull(mangaList);
			Assert.Contains("Death Note", mangaList.Manga.Select(x => x.Title));
			Assert.Contains("Death Note Another Note: Los Angeles BB Renzoku Satsujin Jiken", mangaList.Manga.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserMangaList_ErvelanFilterWithStone_ShouldFindDrStoneAndJoJo()
		{
			UserListMangaSearchConfig searchConfig = new UserListMangaSearchConfig()
			{
				Query = "stone"
			};

			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", searchConfig);

			Assert.NotNull(mangaList);
			Assert.Contains("Dr. Stone", mangaList.Manga.Select(x => x.Title));
			Assert.Contains("JoJo no Kimyou na Bouken Part 6: Stone Ocean", mangaList.Manga.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserMangaList_ErvelanFilterWithHunter_ShouldFindSingleResult()
		{
			UserListMangaSearchConfig searchConfig = new UserListMangaSearchConfig()
			{
				Query = "hunter"
			};

			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", searchConfig);

			Assert.NotNull(mangaList);
			Assert.Single(mangaList.Manga);
		}

		[Fact]
		public async Task GetUserMangaList_ErvelanFilterWithQwerty_ShouldNotFindResults()
		{
			UserListMangaSearchConfig searchConfig = new UserListMangaSearchConfig()
			{
				Query = "qwerty"
			};

			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", searchConfig);

			Assert.NotNull(mangaList);
			Assert.Empty(mangaList.Manga);
		}

		[Fact]
		public async Task GetUserMangaList_ErvelanFilterWithEmptyString_ShouldReturnAllResults()
		{
			UserListMangaSearchConfig searchConfig = new UserListMangaSearchConfig()
			{
				Query = ""
			};

			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", searchConfig);

			Assert.NotNull(mangaList);
			Assert.True(mangaList.Manga.Count > 90);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSortByTitle_ShouldFindYuuyuuHakusho()
		{
			UserListMangaSearchConfig searchConfig = new UserListMangaSearchConfig()
			{
				OrderBy = UserListMangaSearchSortable.Title,
				SortDirection = SortDirection.Ascending
			};

			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", searchConfig);

			Assert.NotNull(mangaList);
			Assert.Equal("Yuu☆Yuu☆Hakusho", mangaList.Manga.First().Title);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSortByScore_ShouldFindBerserk()
		{
			UserListMangaSearchConfig searchConfig = new UserListMangaSearchConfig()
			{
				OrderBy = UserListMangaSearchSortable.Score,
				SortDirection = SortDirection.Descending
			};

			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", searchConfig);

			Assert.NotNull(mangaList);
			Assert.Equal("Berserk", mangaList.Manga.First().Title);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSortByScoreThenTitle_ShouldFindFMAOn4thPlace()
		{
			UserListMangaSearchConfig searchConfig = new UserListMangaSearchConfig()
			{
				OrderBy = UserListMangaSearchSortable.Score,
				OrderBy2 = UserListMangaSearchSortable.Title,
				SortDirection = SortDirection.Descending
			};

			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", searchConfig);

			Assert.NotNull(mangaList);
			Assert.Equal("Berserk", mangaList.Manga.First().Title);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSortYoungAnimal_ShouldFindBerserk()
		{
			UserListMangaSearchConfig searchConfig = new UserListMangaSearchConfig()
			{
				MagazineId = 2
			};

			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", searchConfig);

			Assert.NotNull(mangaList);
			Assert.Single(mangaList.Manga);
			Assert.Equal("Berserk", mangaList.Manga.First().Title);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSortByWrongMagazineId_ShouldReturnAllResults()
		{
			UserListMangaSearchConfig searchConfig = new UserListMangaSearchConfig()
			{
				MagazineId = -1,
			};

			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", searchConfig);

			Assert.NotNull(mangaList);
			Assert.True(mangaList.Manga.Count > 90);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSortByOneWithPublishingStatus_ShouldFindOPMAndOPNotCrossover()
		{
			UserListMangaSearchConfig searchConfig = new UserListMangaSearchConfig()
			{
				Query = "one",
				PublishingStatus = UserListMangaPublishingStatus.Publishing
			};

			UserMangaList mangaList = await _jikan.GetUserMangaList("Ervelan", searchConfig);

			Assert.NotNull(mangaList);
			Assert.Contains("One Piece", mangaList.Manga.Select(x => x.Title));
			Assert.Contains("One Punch-Man", mangaList.Manga.Select(x => x.Title));
			Assert.DoesNotContain("One Piece x Toriko", mangaList.Manga.Select(x => x.Title));
		}
	}
}