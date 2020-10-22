using FluentAssertions;
using FluentAssertions.Execution;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class AnimeSearchTests
	{
		private readonly IJikan _jikan;

		public AnimeSearchTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData("berserk")]
		[InlineData("danganronpa")]
		[InlineData("death")]
		public async Task SearchAnime_NonEmptyQuery_ShouldReturnNotNullSearchAnime(string query)
		{
			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime(query);

			// Then
			returnedAnime.Should().NotBeNull();
		}

		[Fact]
		public async Task SearchAnime_DanganronpaQuery_ShouldReturnDanganronpaAnime()
		{
			// When
			AnimeSearchResult danganronpaAnime = await _jikan.SearchAnime("danganronpa");

			// Then
			danganronpaAnime.ResultLastPage.Should().Be(20);
		}

		[Fact]
		public async Task SearchAnime_OnePieceAiringQuery_ShouldReturnAiringOnePieceAnime()
		{
			// Given
			AnimeSearchConfig searchConfig = new AnimeSearchConfig()
			{
				Status = AiringStatus.Airing
			};

			// When
			AnimeSearchResult onePieceAnime = await _jikan.SearchAnime("one p", searchConfig);

			// Then
			onePieceAnime.Results.First().Title.Should().Be("One Piece");
		}

		[Fact]
		public async Task SearchAnime_HaibaneQuery_ShouldReturnHaibaneRenmeiAnime()
		{
			// When
			AnimeSearchResult result = await _jikan.SearchAnime("haibane");

			// Then
			var firstResult = result.Results.First();
			using (new AssertionScope())
			{
				firstResult.Title.Should().Be("Haibane Renmei");
				firstResult.Type.Should().Be("TV");
				firstResult.Episodes.Should().Be(13);
				firstResult.MalId.Should().Be(387);
			}
		}

		[Fact]
		public async Task SearchAnime_GirlQuerySecondPage_ShouldFindGirlAnime()
		{
			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime("girl", 2);

			// Then
			returnedAnime.Results.Select(x => x.Title).Should().Contain("Sakura-sou no Pet na Kanojo");
		}

		[Theory]
		[InlineData("berserk")]
		[InlineData("danganronpa")]
		[InlineData("death")]
		public async Task SearchAnime_TVConfig_ShouldReturnNotNullSearchAnime(string query)
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV
			};

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime(query, searchConfig);

			// Then
			returnedAnime.Should().Be(returnedAnime);
		}

		[Fact]
		public async Task SearchAnime_DanganronpaTVConfig_ShouldReturnDanganronpaAnime()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV
			};

			// When
			AnimeSearchResult danganronpaAnime = await _jikan.SearchAnime("danganronpa", searchConfig);

			// Then
			danganronpaAnime.ResultLastPage.Should().Be(2);
		}

		[Fact]
		public async Task SearchAnime_FairyTailTVAbove7Config_ShouldFilterFairyTailAnimeScore()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Score = 7
			};

			// When
			AnimeSearchResult fairyTailAnime = await _jikan.SearchAnime("Fairy Tail", searchConfig);

			// Then
			using (new AssertionScope())
			{
				fairyTailAnime.Results.First().Title.Should().Be("Fairy Tail (2014)");
				fairyTailAnime.Results.Skip(1).First().Title.Should().Be("Fairy Tail: Final Series");
			}
		}

		[Fact]
		public async Task SearchAnime_BlameMechaConfig_ShouldFilterBleachMecha()
		{
			// Given
			var searchConfig = new AnimeSearchConfig();
			searchConfig.Genres.Add(GenreSearch.Mecha);

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime("Blame", searchConfig);

			// Then
			returnedAnime.Results.Select(x => x.Title).Should().Contain("Blame! Movie");
		}

		[Fact]
		public async Task SearchAnime_BlameMechaMovieConfig_ShouldFilterBleachMechaMovie()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.Movie
			};
			searchConfig.Genres.Add(GenreSearch.Mecha);

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime("Blame", searchConfig);

			// Then
			returnedAnime.Results.First().Title.Should().Be("Blame! Movie");
		}

		[Fact]
		public async Task SearchAnime_BleachAfter2017Config_ShouldFilterBleachAfter2017()
		{
			// Given
			System.DateTime configDate = new System.DateTime(2018, 1, 1);
			var searchConfig = new AnimeSearchConfig
			{
				StartDate = configDate
			};

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime("Bleach", searchConfig);

			// Then
			var titles = returnedAnime.Results.Select(x => x.Title);
			using (new AssertionScope())
			{
				titles.Should().Contain("Full Metal Panic! Invisible Victory");
				titles.Should().Contain("Beatless");
			}
		}

		[Fact]
		public async Task SearchAnime_OneSortByMembersConfig_ShouldSortByPopularityOPMFirst()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				OrderBy = AnimeSearchSortable.Members,
				SortDirection = SortDirection.Descending
			};

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime("one", searchConfig);

			// Then
			var titles = returnedAnime.Results.Select(x => x.Title);
			using (new AssertionScope())
			{
				titles.Should().Contain("One Piece");
				titles.Should().Contain("One Punch Man");
				titles.First().Should().Be("One Punch Man");
			}
		}

		[Fact]
		public async Task SearchAnime_OneSortByIdConfig_ShouldSortByIdHachimitsuFirst()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				OrderBy = AnimeSearchSortable.Id,
				SortDirection = SortDirection.Ascending
			};

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime("one", searchConfig);

			// Then
			returnedAnime.Results.First().Title.Should().Be("Hachimitsu to Clover");
		}

		[Fact]
		public async Task SearchAnime_VioletProducerKyotoAnimationConfig_ShouldReturnVEGAsTop3()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				ProducerId = 2
			};

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime("violet", searchConfig);

			// Then
			using (new AssertionScope())
			{
				returnedAnime.Results.First().Title.Should().Contain("Evergarden");
				returnedAnime.Results.Skip(1).First().Title.Should().Contain("Evergarden");
				returnedAnime.Results.Skip(2).First().Title.Should().Contain("Evergarden");
			}
		}

		[Fact]
		public async Task SearchAnime_VioletIncorrectProducerConfig_ShouldNotFilter()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				ProducerId = -1
			};

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime("violet", searchConfig);

			// Then
			var titles = returnedAnime.Results.Select(x => x.Title);
			using (new AssertionScope())
			{
				titles.Should().Contain("Violence Jack: Jigoku Gai-hen");
				titles.Should().Contain("Violet Evergarden");
			}
		}

		[Fact]
		public async Task SearchAnime_EmptyQueryActionTvAnime_ShouldFindAfroSamuraiAndAjin()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Genres = new List<GenreSearch> { GenreSearch.Action }
			};

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime(searchConfig);

			// Then
			var titles = returnedAnime.Results.Select(x => x.Title);
			using (new AssertionScope())
			{
				titles.Should().Contain("Ajin");
				titles.Should().Contain("Afro Samurai");
			}
		}

		[Fact]
		public async Task SearchAnime_EmptyQueryActionTvAnimeFirstPage_ShouldFindAfroSamuraiAndAjin()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Genres = new List<GenreSearch> { GenreSearch.Action }
			};

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime(searchConfig, 1);

			// Then
			var titles = returnedAnime.Results.Select(x => x.Title);
			using (new AssertionScope())
			{
				titles.Should().Contain("Ajin");
				titles.Should().Contain("Afro Samurai");
			}
		}

		[Fact]
		public async Task SearchAnime_EmptyQueryActionTvAnimeSecondPage_ShouldFindAzurLaneAndBaccano()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Genres = new List<GenreSearch> { GenreSearch.Action }
			};

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime(searchConfig, 2);

			// Then
			var titles = returnedAnime.Results.Select(x => x.Title);
			using (new AssertionScope())
			{
				titles.Should().Contain("Azur Lane");
				titles.Should().Contain("Baccano!");
			}
		}

		[Fact]
		public async Task SearchAnime_OneQueryActionCompletedAnimeSecondPage_ShouldReturnNotEmptyCollection()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Status = AiringStatus.Completed,
				Genres = new List<GenreSearch> { GenreSearch.Action }
			};

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime("one", 2, searchConfig);

			// Then
			using (new AssertionScope())
			{
				returnedAnime.Should().NotBeNull();
				returnedAnime.Results.Should().NotBeEmpty();
			}
		}

		[Fact]
		public async Task SearchAnime_GenreInclusion_ShouldReturnNotEmptyCollection()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Genres = new List<GenreSearch> { GenreSearch.Action, GenreSearch.Comedy },
				GenreIncluded = true,
				OrderBy = AnimeSearchSortable.Score,
				SortDirection = SortDirection.Descending
			};

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime(searchConfig);

			// Then
			using (new AssertionScope())
			{
				returnedAnime.Should().NotBeNull();
				returnedAnime.Results.Should().NotBeEmpty();
			}
		}

		[Fact]
		public async Task SearchAnime_GenreExclusion_ShouldReturnNotEmptyCollection()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Genres = new List<GenreSearch> { GenreSearch.Action, GenreSearch.Adventure },
				GenreIncluded = false,
				OrderBy = AnimeSearchSortable.Score,
				SortDirection = SortDirection.Descending
			};

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime(searchConfig);

			// Then
			using (new AssertionScope())
			{
				returnedAnime.Should().NotBeNull();
				returnedAnime.Results.Should().NotBeEmpty();
			}
		}
	}
}