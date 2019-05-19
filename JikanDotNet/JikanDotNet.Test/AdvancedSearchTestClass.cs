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
		public async Task SearchAnime_TVConfig_ShouldReturnNotNullSearchAnime(string query)
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV
			};

			AnimeSearchResult returnedAnime = await jikan.SearchAnime(query, searchConfig);

			Assert.NotNull(returnedAnime);
		}

		[Fact]
		public async Task SearchAnime_DanganronpaTVConfig_ShouldReturnDanganronpaAnime()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV
			};

			AnimeSearchResult danganronpaAnime = await jikan.SearchAnime("danganronpa", searchConfig);

			Assert.Equal(1, danganronpaAnime.ResultLastPage);
		}

		[Fact]
		public async Task SearchAnime_FairyTailTVAbove8Config_ShouldFilterFairyTailAnimeScore()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Score = 8
			};

			AnimeSearchResult fairyTailAnime = await jikan.SearchAnime("Fairy Tail", searchConfig);

			Assert.Equal("Fairy Tail (2014)", fairyTailAnime.Results.First().Title);
			Assert.Equal("Fairy Tail: Final Series", fairyTailAnime.Results.Skip(1).First().Title);
		}

		[Fact]
		public async Task SearchAnime_BleachMechaConfig_ShouldFilterBleachMecha()
		{
			var searchConfig = new AnimeSearchConfig();
			searchConfig.Genres.Add(GenreSearch.Mecha);

			AnimeSearchResult returnedAnime = await jikan.SearchAnime("Bleach", searchConfig);

			Assert.Contains("Blame! Movie", returnedAnime.Results.Select(x => x.Title));
			Assert.Contains("Bubblegum Crisis", returnedAnime.Results.Select(x => x.Title));
		}

		[Fact]
		public async Task SearchAnime_BleachMechaMovieConfig_ShouldFilterBleachMechaMovie()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.Movie
			};
			searchConfig.Genres.Add(GenreSearch.Mecha);

			AnimeSearchResult returnedAnime = await jikan.SearchAnime("Bleach", searchConfig);

			Assert.Equal("Blame! Movie", returnedAnime.Results.First().Title);
		}

		[Fact]
		public async Task SearchAnime_BleachAfter2017Config_ShouldFilterBleachAfter2017()
		{
			System.DateTime configDate = new System.DateTime(2018, 1, 1);
			var searchConfig = new AnimeSearchConfig
			{
				StartDate = configDate
			};

			AnimeSearchResult returnedAnime = await jikan.SearchAnime("Bleach", searchConfig);

			Assert.Contains("Full Metal Panic! Invisible Victory", returnedAnime.Results.Select(x => x.Title));
			Assert.Contains("Beatless", returnedAnime.Results.Select(x => x.Title));
		}

		[Fact]
		public async Task SearchAnime_OneSortByMembersConfig_ShouldSortByPopularityOPMFirst()
		{
			var searchConfig = new AnimeSearchConfig
			{
				OrderBy = AnimeSearchSortable.Members
			};

			AnimeSearchResult returnedAnime = await jikan.SearchAnime("one", searchConfig);

			Assert.Contains("One Piece", returnedAnime.Results.Select(x => x.Title));
			Assert.Contains("One Punch Man", returnedAnime.Results.Select(x => x.Title));
			Assert.Equal("One Punch Man", returnedAnime.Results.First().Title);
		}

		[Fact]
		public async Task SearchAnime_OneSortByIdConfig_ShouldSortByIdHachimitsuFirst()
		{
			var searchConfig = new AnimeSearchConfig
			{
				OrderBy = AnimeSearchSortable.Id,
				SortDirection = SortDirection.Ascending
			};

			AnimeSearchResult returnedAnime = await jikan.SearchAnime("one", searchConfig);

			Assert.Equal("Hachimitsu to Clover", returnedAnime.Results.First().Title);
		}

		[Fact]
		public async Task SearchAnime_VioletProducerKyotoAnimationConfig_ShouldReturnVEGAsTop3()
		{
			var searchConfig = new AnimeSearchConfig
			{
				ProducerId = 2
			};

			AnimeSearchResult returnedAnime = await jikan.SearchAnime("violet", searchConfig);

			Assert.Contains("Evergarden", returnedAnime.Results.First().Title);
			Assert.Contains("Evergarden", returnedAnime.Results.Skip(1).First().Title);
			Assert.Contains("Evergarden", returnedAnime.Results.Skip(2).First().Title);
		}

		[Fact]
		public async Task SearchAnime_VioletIncorrectProducerConfig_ShouldNotFilter()
		{
			var searchConfig = new AnimeSearchConfig
			{
				ProducerId = -1
			};

			AnimeSearchResult returnedAnime = await jikan.SearchAnime("violet", searchConfig);

			Assert.Contains("Violence Jack: Jigoku Gai-hen", returnedAnime.Results.Select(x => x.Title));
			Assert.Contains("Violet Evergarden", returnedAnime.Results.Select(x => x.Title));
		}

		[Theory]
		[InlineData("berserk")]
		[InlineData("monster")]
		[InlineData("death")]
		public async Task SearchManga_MangaConfig_ShouldReturnNotNullSearchManga(string query)
		{
			var searchConfig = new MangaSearchConfig
			{
				Type = MangaType.Manga
			};

			MangaSearchResult returnedManga = await jikan.SearchManga(query, searchConfig);

			Assert.NotNull(returnedManga);
		}

		[Fact]
		public async Task SearchManga_DanganronpaMangaConfig_ShouldReturnDanganronpaManga()
		{
			var searchConfig = new MangaSearchConfig
			{
				Type = MangaType.Manga
			};
			MangaSearchResult danganronpaManga = await jikan.SearchManga("danganronpa", searchConfig);

			Assert.Equal(1, danganronpaManga.ResultLastPage);
		}

		[Fact]
		public async Task SearchManga_DanganronpaMangaAbove8Config_ShouldReturnDanganronpaMangaScore()
		{
			var searchConfig = new MangaSearchConfig
			{
				Type = MangaType.Manga,
				Score = 8
			};
			MangaSearchResult danganronpaManga = await jikan.SearchManga("danganronpa", searchConfig);

			Assert.Equal("New Danganronpa V3: Minna no Koroshiai Shingakki Comic Anthology", danganronpaManga.Results.First().Title);
		}

		[Fact]
		public async Task SearchManga_MangaGameGenreConfig_ShouldFilterMetalFightBeyblade()
		{
			var searchConfig = new MangaSearchConfig
			{
				Type = MangaType.Manga,
			};
			searchConfig.Genres.Add(GenreSearch.Game);

			MangaSearchResult returnedManga = await jikan.SearchManga("metal", searchConfig);

			Assert.Equal("Metal Fight Beyblade", returnedManga.Results.First().Title);
		}

		[Fact]
		public async Task SearchManga_MetalAfter2014Config_ShouldFilterMetallicaMettallucaAndFMPEndDate()
		{
			var searchConfig = new MangaSearchConfig
			{
				EndDate = new System.DateTime(2015, 1, 1)
			};

			MangaSearchResult returnedManga = await jikan.SearchManga("metal", searchConfig);

			Assert.Contains("Metallica Metalluca", returnedManga.Results.Select(x => x.Title));
			Assert.Contains("Full Metal Panic! Another", returnedManga.Results.Select(x => x.Title));
		}

		[Fact]
		public async Task SearchManga_MetalSortByMembersConfig_ShouldSortByPopularityFairyTailFirst()
		{
			var searchConfig = new MangaSearchConfig
			{
				OrderBy = MangaSearchSortable.Members
			};

			MangaSearchResult returnedManga = await jikan.SearchManga("metal", searchConfig);

			Assert.Contains("Fairy Tail", returnedManga.Results.Select(x => x.Title));
			Assert.Contains("Fullmetal Alchemist", returnedManga.Results.Select(x => x.Title));
			Assert.Equal("Fairy Tail", returnedManga.Results.First().Title);
		}

		[Fact]
		public async Task SearchManga_OneSortByIdConfig_ShouldSortByIdOnePieceFirst()
		{
			var searchConfig = new MangaSearchConfig
			{
				OrderBy = MangaSearchSortable.Id,
				SortDirection = SortDirection.Ascending
			};

			MangaSearchResult returnedManga = await jikan.SearchManga("one", searchConfig);

			Assert.Equal("One Piece", returnedManga.Results.First().Title);
		}

		[Fact]
		public async Task SearchManga_TorikoShonenJumpMagazineConfig_ShouldReturnTwoEntries()
		{
			var searchConfig = new MangaSearchConfig
			{
				MagazineId = 83
			};

			MangaSearchResult returnedManga = await jikan.SearchManga("toriko", searchConfig);

			Assert.Contains("Toriko", returnedManga.Results.First().Title);
			Assert.Equal(2, returnedManga.Results.Count);
		}

		[Fact]
		public async Task SearchManga_TorikoIncorrectMagazineConfig_ShouldNotFilter()
		{
			var searchConfig = new MangaSearchConfig
			{
				MagazineId = -1
			};

			MangaSearchResult returnedManga = await jikan.SearchManga("toriko", searchConfig);

			Assert.Contains("Toriko", returnedManga.Results.First().Title);
			Assert.True(returnedManga.Results.Count > 30);
		}
	}
}