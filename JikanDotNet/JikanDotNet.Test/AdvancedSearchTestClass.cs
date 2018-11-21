using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class AdvancedSearchTestClass
	{
		private readonly IJikan jikan;

		public AdvancedSearchTestClass()
		{
			jikan = new Jikan(true);
		}

		[Theory]
		[InlineData("berserk")]
		[InlineData("danganronpa")]
		[InlineData("death")]
		public void ShouldReturnNotNullSearchAnime(string query)
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV
			};

			AnimeSearchResult returnedAnime = Task.Run(() => jikan.SearchAnime(query, searchConfig)).Result;

			Assert.NotNull(returnedAnime);
		}

		[Fact]
		public void ShouldReturnDanganronpaAnime()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV
			};

			AnimeSearchResult danganronpaAnime = Task.Run(() => jikan.SearchAnime("danganronpa", searchConfig)).Result;

			Assert.Equal(1, danganronpaAnime.ResultLastPage);
		}

		[Fact]
		public void ShouldFilterFairyTailAnimeScore()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Score = 8
			};

			AnimeSearchResult fairyTailAnime = Task.Run(() => jikan.SearchAnime("Fairy Tail", searchConfig)).Result;

			Assert.Equal("Fairy Tail (2014)", fairyTailAnime.Results.First().Title);
			Assert.Equal("Fairy Tail: Final Series", fairyTailAnime.Results.Skip(1).First().Title);
		}

		[Fact]
		public void ShouldFilterBleachMecha()
		{
			var searchConfig = new AnimeSearchConfig();
			searchConfig.Genres.Add(GenreSearch.Mecha);

			AnimeSearchResult returnedAnime = Task.Run(() => jikan.SearchAnime("Bleach", searchConfig)).Result;

			Assert.Contains("Blame! Movie", returnedAnime.Results.Select(x => x.Title));
			Assert.Contains("Bubblegum Crisis", returnedAnime.Results.Select(x => x.Title));
		}

		[Fact]
		public void ShouldFilterBleachMechaMovie()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.Movie
			};
			searchConfig.Genres.Add(GenreSearch.Mecha);

			AnimeSearchResult returnedAnime = Task.Run(() => jikan.SearchAnime("Bleach", searchConfig)).Result;

			Assert.Equal("Blame! Movie", returnedAnime.Results.First().Title);
		}

		[Fact]
		public void ShouldFilterBleachAfter2018()
		{
			System.DateTime configDate = new System.DateTime(2018, 1, 1);
			var searchConfig = new AnimeSearchConfig
			{
				StartDate = configDate
			};

			AnimeSearchResult returnedAnime = Task.Run(() => jikan.SearchAnime("Bleach", searchConfig)).Result;

			Assert.Contains("Full Metal Panic! Invisible Victory", returnedAnime.Results.Select(x => x.Title));
			Assert.Contains("Beatless", returnedAnime.Results.Select(x => x.Title));
		}

		[Theory]
		[InlineData("berserk")]
		[InlineData("monster")]
		[InlineData("death")]
		public void ShouldReturnNotNullSearchManga(string query)
		{
			var searchConfig = new MangaSearchConfig
			{
				Type = MangaType.Manga
			};

			MangaSearchResult returnedManga = Task.Run(() => jikan.SearchManga(query, searchConfig)).Result;

			Assert.NotNull(returnedManga);
		}

		[Fact]
		public void ShouldReturnDanganronpaManga()
		{
			var searchConfig = new MangaSearchConfig
			{
				Type = MangaType.Manga
			};
			MangaSearchResult danganronpaManga = Task.Run(() => jikan.SearchManga("danganronpa", searchConfig)).Result;

			Assert.Equal(1, danganronpaManga.ResultLastPage);
		}

		[Fact]
		public void ShouldReturnDanganronpaMangaScore()
		{
			var searchConfig = new MangaSearchConfig
			{
				Type = MangaType.Manga,
				Score = 8
			};
			MangaSearchResult danganronpaManga = Task.Run(() => jikan.SearchManga("danganronpa", searchConfig)).Result;

			Assert.Equal("New Danganronpa V3: Minna no Koroshiai Shingakki Comic Anthology", danganronpaManga.Results.First().Title);
		}

		[Fact]
		public void ShouldFilterMetalGame()
		{
			var searchConfig = new MangaSearchConfig
			{
				Type = MangaType.Manga,
			};
			searchConfig.Genres.Add(GenreSearch.Game);

			MangaSearchResult returnedManga = Task.Run(() => jikan.SearchManga("metal", searchConfig)).Result;

			Assert.Equal("Metal Fight Beyblade", returnedManga.Results.First().Title);
		}

		[Fact]
		public void ShouldFilterMetalGameEndDate()
		{
			var searchConfig = new MangaSearchConfig
			{
				EndDate = new System.DateTime(2015, 1, 1)
			};

			MangaSearchResult returnedManga = Task.Run(() => jikan.SearchManga("metal", searchConfig)).Result;

			Assert.Contains("Metallica Metalluca", returnedManga.Results.Select(x => x.Title));
			Assert.Contains("Full Metal Panic! Another", returnedManga.Results.Select(x => x.Title));
		}
	}
}