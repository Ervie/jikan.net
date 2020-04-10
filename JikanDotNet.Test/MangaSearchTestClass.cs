using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MangaSearchTestClass
	{
		private readonly IJikan jikan;

		public MangaSearchTestClass()
		{
			jikan = new Jikan();
		}

		[Theory]
		[InlineData("berserk")]
		[InlineData("monster")]
		[InlineData("death")]
		public async Task SearchManga_NonEmptyQuery_ShouldReturnNotNullSearchManga(string query)
		{
			MangaSearchResult returnedManga = await jikan.SearchManga(query);

			Assert.NotNull(returnedManga);
		}

		[Fact]
		public async Task SearchManga_DanganronpaQuery_ShouldReturnDanganronpaManga()
		{
			MangaSearchResult danganronpaManga = await jikan.SearchManga("danganronpa");

			Assert.Equal(20, danganronpaManga.ResultLastPage);
		}

		[Fact]
		public async Task SearchManga_YotsubatoQuery_ShouldReturnYotsubatoManga()
		{
			MangaSearchResult yotsubato = await jikan.SearchManga("yotsubato");

			Assert.Equal("Yotsuba to!", yotsubato.Results.First().Title);
			Assert.Equal("Manga", yotsubato.Results.First().Type);
			Assert.Equal(0, yotsubato.Results.First().Volumes);
			Assert.Equal(104, yotsubato.Results.First().MalId);
		}

		[Fact]
		public async Task SearchManga_YotsubatoPublishingQuery_ShouldReturnPublishedYotsubatoManga()
		{
			MangaSearchConfig searchConfig = new MangaSearchConfig()
			{
				Status = AiringStatus.Airing
			};

			MangaSearchResult yotsubato = await jikan.SearchManga("yotsubato", searchConfig);

			Assert.Equal("Yotsuba to!", yotsubato.Results.First().Title);
			Assert.Equal("Manga", yotsubato.Results.First().Type);
			Assert.Equal(0, yotsubato.Results.First().Volumes);
			Assert.Equal(104, yotsubato.Results.First().MalId);
		}

		[Fact]
		public async Task SearchManga_GirlQuerySecondPage_ShouldFindGirlManga()
		{
			MangaSearchResult returnedAnime = await jikan.SearchManga("girl", 2);

			Assert.Contains("Neo Girl", returnedAnime.Results.Select(x => x.Title));
			Assert.Equal(20, returnedAnime.ResultLastPage);
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

			Assert.Contains("Dangan", danganronpaManga.Results.First().Title);
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
				OrderBy = MangaSearchSortable.Members,
				SortDirection = SortDirection.Descending
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

		[Fact]
		public async Task SearchManga_EmptyQueryActionManga_ShouldFindCrowAnd007()
		{
			var searchConfig = new MangaSearchConfig
			{
				Genres = new List<GenreSearch> { GenreSearch.Action },
				Type = MangaType.Manga
			};

			MangaSearchResult returnedManga = await jikan.SearchManga(searchConfig);

			Assert.Contains("-Crow-", returnedManga.Results.Select(x => x.Title));
			Assert.Contains("007 Series", returnedManga.Results.Select(x => x.Title));
			Assert.True(returnedManga.Results.Count > 30);
		}

		[Fact]
		public async Task SearchManga_EmptyQueryActionMangaFirstPage_ShouldFindCrowAnd007()
		{
			var searchConfig = new MangaSearchConfig
			{
				Genres = new List<GenreSearch> { GenreSearch.Action },
				Type = MangaType.Manga
			};

			MangaSearchResult returnedManga = await jikan.SearchManga(searchConfig, 1);

			Assert.Contains("-Crow-", returnedManga.Results.Select(x => x.Title));
			Assert.Contains("007 Series", returnedManga.Results.Select(x => x.Title));
			Assert.True(returnedManga.Results.Count > 30);
		}

		[Fact]
		public async Task SearchManga_EmptyQueryActionMangaSecondPage_ShouldFind888AndAccelWorld()
		{
			var searchConfig = new MangaSearchConfig
			{
				Genres = new List<GenreSearch> { GenreSearch.Action },
				Type = MangaType.Manga
			};

			MangaSearchResult returnedManga = await jikan.SearchManga(searchConfig, 2);

			Assert.Contains("888", returnedManga.Results.Select(x => x.Title));
			Assert.Contains("Accel World", returnedManga.Results.Select(x => x.Title));
			Assert.True(returnedManga.Results.Count > 30);
		}
	}
}
