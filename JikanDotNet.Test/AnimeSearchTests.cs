using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
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
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		[InlineData("a")]
		[InlineData("bb")]
		public async Task SearchAnime_InvalidQuery_ShouldThrowValidationException(string query)
		{
			// When
			Func<Task<AnimeSearchResult>> func = _jikan.Awaiting(x => x.SearchAnime(query));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		[InlineData("a")]
		[InlineData("bb")]
		public async Task SearchAnime_InvalidQuerySecondPage_ShouldThrowValidationException(string query)
		{
			// When
			Func<Task<AnimeSearchResult>> func = _jikan.Awaiting(x => x.SearchAnime(query, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task SearchAnime_GirlQueryInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<AnimeSearchResult>> func = _jikan.Awaiting(x => x.SearchAnime("girl", page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		[InlineData("a")]
		[InlineData("bb")]
		public async Task SearchAnime_InvalidQueryWithConfig_ShouldThrowValidationException(string query)
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV
			};

			// When
			Func<Task<AnimeSearchResult>> func = _jikan.Awaiting(x => x.SearchAnime(query, searchConfig));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task SearchAnime_DanganronpaQueryNullConfig_ShouldThrowValidationException()
		{
			// When
			Func<Task<AnimeSearchResult>> func = _jikan.Awaiting(x => x.SearchAnime("danganronpa", null));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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
			searchConfig.Genres.Add(AnimeGenreSearch.Mecha);

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
			searchConfig.Genres.Add(AnimeGenreSearch.Mecha);

			// When
			AnimeSearchResult returnedAnime = await _jikan.SearchAnime("Blame", searchConfig);

			// Then
			returnedAnime.Results.First().Title.Should().Be("Blame! Movie");
		}

		[Fact]
		public async Task SearchAnime_BleachAfter2017Config_ShouldFilterBleachAfter2017()
		{
			// Given
			DateTime configDate = new DateTime(2018, 1, 1);
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
		public async Task SearchAnime_EmptyQueryNullConfig_ShouldThrowValidationException()
		{
			// When
			Func<Task<AnimeSearchResult>> func = _jikan.Awaiting(x => x.SearchAnime("danganronpa", null));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData((AiringStatus)int.MaxValue, null, null, null, null, null)]
		[InlineData((AiringStatus)int.MinValue, null, null, null, null, null)]
		[InlineData(null, (AnimeAgeRating)int.MaxValue, null, null, null, null)]
		[InlineData(null, (AnimeAgeRating)int.MinValue, null, null, null, null)]
		[InlineData(null, null, (AnimeType)int.MaxValue, null, null, null)]
		[InlineData(null, null, (AnimeType)int.MinValue, null, null, null)]
		[InlineData(null, null, null, (AnimeSearchSortable)int.MaxValue, null, null)]
		[InlineData(null, null, null, (AnimeSearchSortable)int.MinValue, null, null)]
		[InlineData(null, null, null, AnimeSearchSortable.Episodes, (SortDirection)int.MaxValue, null)]
		[InlineData(null, null, null, AnimeSearchSortable.Episodes, (SortDirection)int.MinValue, null)]
		[InlineData(null, null, null, null, null, (AnimeGenreSearch)int.MaxValue)]
		[InlineData(null, null, null, null, null, (AnimeGenreSearch)int.MinValue)]
		public async Task SearchAnime_EmptyQueryWithConfigWithInvalidEnums_ShouldThrowValidationException(
			AiringStatus? airingStatus,
			AnimeAgeRating? rating,
			AnimeType? mangaType,
			AnimeSearchSortable? orderBy,
			SortDirection? sortDirection,
			AnimeGenreSearch? genreSearch
		)
		{
			// Given
			var searchConfig = new AnimeSearchConfig()
			{
				Status = airingStatus.GetValueOrDefault(),
				Rating = rating.GetValueOrDefault(),
				Type = mangaType.GetValueOrDefault(),
				OrderBy = orderBy.GetValueOrDefault(),
				SortDirection = sortDirection.GetValueOrDefault(),
				Genres = genreSearch.HasValue ? new[] { genreSearch.Value } : Array.Empty<AnimeGenreSearch>()
			};

			// When
			Func<Task<AnimeSearchResult>> func = _jikan.Awaiting(x => x.SearchAnime(searchConfig));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task SearchAnime_EmptyQueryActionTvAnime_ShouldFindAfroSamuraiAndAjin()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
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

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task SearchAnime_EmptyQueryActionTvAnimeInvalidPage_ShouldThrowValidationException(int page)
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			// When
			Func<Task<AnimeSearchResult>> func = _jikan.Awaiting(x => x.SearchAnime(searchConfig, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task SearchAnime_EmptyQueryActionTvAnimeFirstPage_ShouldFindAfroSamuraiAndAjin()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
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
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
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

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		[InlineData("a")]
		[InlineData("bb")]
		public async Task SearchAnime_InvalidQueryActionCompletedAnimeSecondPage_ShouldThrowValidationException(string query)
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Status = AiringStatus.Completed,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			// When
			Func<Task<AnimeSearchResult>> func = _jikan.Awaiting(x => x.SearchAnime(query, 2, searchConfig));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task SearchAnime_GirlQueryActionCompletedAnimeInvalidPage_ShouldThrowValidationException(int page)
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Status = AiringStatus.Completed,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			// When
			Func<Task<AnimeSearchResult>> func = _jikan.Awaiting(x => x.SearchAnime("girl", page, searchConfig));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task SearchAnime_OneQueryActionCompletedAnimeSecondPage_ShouldReturnNotEmptyCollection()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Status = AiringStatus.Completed,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
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
		public async Task SearchAnime_EmptyQuerySecondPageNullConfig_ShouldThrowValidationException()
		{
			// When
			Func<Task<AnimeSearchResult>> func = _jikan.Awaiting(x => x.SearchAnime("danganronpa", 2, null));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task SearchAnime_GenreInclusion_ShouldReturnNotEmptyCollection()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action, AnimeGenreSearch.Comedy },
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
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action, AnimeGenreSearch.Adventure },
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