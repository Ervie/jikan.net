using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class UserMangaListTestClass
	{
		private readonly IJikan jikan;

		public UserMangaListTestClass()
		{
			jikan = new Jikan();
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