using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class AnimeSearchTestClass
	{
		private readonly IJikan jikan;

		public AnimeSearchTestClass()
		{
			jikan = new Jikan();
		}

		[Theory]
		[InlineData("berserk")]
		[InlineData("danganronpa")]
		[InlineData("death")]
		public async Task SearchAnime_NonEmptyQuery_ShouldReturnNotNullSearchAnime(string query)
		{
			AnimeSearchResult returnedAnime = await jikan.SearchAnime(query);

			Assert.NotNull(returnedAnime);
		}

		[Fact]
		public async Task SearchAnime_DanganronpaQuery_ShouldReturnDanganronpaAnime()
		{
			AnimeSearchResult danganronpaAnime = await jikan.SearchAnime("danganronpa");

			Assert.Equal(20, danganronpaAnime.ResultLastPage);
		}

		[Fact]
		public async Task SearchAnime_OnePieceAiringQuery_ShouldReturnAiringOnePieceAnime()
		{
			AnimeSearchConfig searchConfig = new AnimeSearchConfig()
			{
				Status = AiringStatus.Airing
			};

			AnimeSearchResult onePieceAnime = await jikan.SearchAnime("one p", searchConfig);

			Assert.Equal("One Piece", onePieceAnime.Results.First().Title);
		}

		[Fact]
		public async Task SearchAnime_HaibaneQuery_ShouldReturnHaibaneRenmeiAnime()
		{
			AnimeSearchResult haibaneRenmei = await jikan.SearchAnime("haibane");

			Assert.Equal("Haibane Renmei", haibaneRenmei.Results.First().Title);
			Assert.Equal("TV", haibaneRenmei.Results.First().Type);
			Assert.Equal(13, haibaneRenmei.Results.First().Episodes);
			Assert.Equal(387, haibaneRenmei.Results.First().MalId);
		}

		[Fact]
		public async Task SearchAnime_GirlQuerySecondPage_ShouldFindGirlAnime()
		{
			AnimeSearchResult returnedAnime = await jikan.SearchAnime("girl", 2);

			Assert.Contains("Frame Arms Girl", returnedAnime.Results.Select(x => x.Title));
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

			Assert.Equal(2, danganronpaAnime.ResultLastPage);
		}

		[Fact]
		public async Task SearchAnime_FairyTailTVAbove7Config_ShouldFilterFairyTailAnimeScore()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Score = 7
			};

			AnimeSearchResult fairyTailAnime = await jikan.SearchAnime("Fairy Tail", searchConfig);

			Assert.Equal("Fairy Tail (2014)", fairyTailAnime.Results.First().Title);
			Assert.Equal("Fairy Tail: Final Series", fairyTailAnime.Results.Skip(1).First().Title);
		}

		[Fact]
		public async Task SearchAnime_BlameMechaConfig_ShouldFilterBleachMecha()
		{
			var searchConfig = new AnimeSearchConfig();
			searchConfig.Genres.Add(GenreSearch.Mecha);

			AnimeSearchResult returnedAnime = await jikan.SearchAnime("Blame", searchConfig);

			Assert.Contains("Blame! Movie", returnedAnime.Results.Select(x => x.Title));
		}

		[Fact]
		public async Task SearchAnime_BlameMechaMovieConfig_ShouldFilterBleachMechaMovie()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.Movie
			};
			searchConfig.Genres.Add(GenreSearch.Mecha);

			AnimeSearchResult returnedAnime = await jikan.SearchAnime("Blame", searchConfig);

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
				OrderBy = AnimeSearchSortable.Members,
				SortDirection = SortDirection.Descending
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

		[Fact]
		public async Task SearchAnime_EmptyQueryActionTvAnime_ShouldFindAfroSamuraiAndAjin()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Genres = new List<GenreSearch> { GenreSearch.Action }
			};

			AnimeSearchResult returnedAnime = await jikan.SearchAnime(searchConfig);

			Assert.Contains("Ajin", returnedAnime.Results.Select(x => x.Title));
			Assert.Contains("Afro Samurai", returnedAnime.Results.Select(x => x.Title));
		}

		[Fact]
		public async Task SearchAnime_EmptyQueryActionTvAnimeFirstPage_ShouldFindAfroSamuraiAndAjin()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Genres = new List<GenreSearch> { GenreSearch.Action }
			};

			AnimeSearchResult returnedAnime = await jikan.SearchAnime(searchConfig, 1);

			Assert.Contains("Ajin", returnedAnime.Results.Select(x => x.Title));
			Assert.Contains("Afro Samurai", returnedAnime.Results.Select(x => x.Title));
		}

		[Fact]
		public async Task SearchAnime_EmptyQueryActionTvAnimeSecondPage_ShouldFindAzurLaneAndBaccano()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Genres = new List<GenreSearch> { GenreSearch.Action }
			};

			AnimeSearchResult returnedAnime = await jikan.SearchAnime(searchConfig, 2);

			Assert.Contains("Azur Lane", returnedAnime.Results.Select(x => x.Title));
			Assert.Contains("Baccano!", returnedAnime.Results.Select(x => x.Title));
		}

		[Fact]
		public async Task SearchAnime_OneQueryActionCompletedAnimeSecondPage_ShouldReturnNotEmptyCollection()
		{
			var searchConfig = new AnimeSearchConfig
			{
				Status = AiringStatus.Completed,
				Genres = new List<GenreSearch> { GenreSearch.Action }
			};

			AnimeSearchResult returnedAnime = await jikan.SearchAnime("one", 2, searchConfig);

			Assert.NotNull(returnedAnime);
			Assert.NotEmpty(returnedAnime.Results);
		}
	}
}