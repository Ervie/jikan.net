using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Tenrai.Consts;
using Xunit;

namespace Tenrai.Tests.AnimeTests;

[Collection("TenraiTests")]
public class SearchAnimeTestsAsync
{
	private readonly ITenrai _tenrai;

	public SearchAnimeTestsAsync(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task SearchAnimeAsync_InvalidPage_ShouldThrowValidationException(int page)
	{
		// Given
		var config = new AnimeSearchConfig {Page = page};

		// When
		var func = _tenrai.Awaiting(x => x.SearchAnimeAsync(config));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(101)]
	[InlineData(int.MaxValue)]
	public async Task SearchAnimeAsync_InvalidPageSize_ShouldThrowValidationException(int pageSize)
	{
		// Given
		var config = new AnimeSearchConfig {Limit = pageSize};

		// When
		var func = _tenrai.Awaiting(x => x.SearchAnimeAsync(config));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task SearchAnimeAsync_GivenSecondPage_ShouldReturnSecondPage()
	{
		// Given
		var config = new AnimeSearchConfig {Page = 2};

		// When
		var anime = await _tenrai.SearchAnimeAsync(config);

		// Then
		using var _ = new AssertionScope();
		anime.Data.Should().HaveCount(ParameterConsts.DefaultPageSize);
		anime.Data.First().Title.Should().Be("Rurouni Kenshin: Meiji Kenkaku Romantan - Tsuioku-hen");
		anime.Pagination.LastVisiblePage.Should().BeGreaterThan(780);
		anime.Pagination.CurrentPage.Should().Be(2);
		anime.Pagination.Items.Count.Should().Be(25);
		anime.Pagination.Items.PerPage.Should().Be(25);
	}

	[Fact]
	public async Task SearchAnimeAsync_GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
	{
		// Given
		const int pageSize = 5;
		var config = new AnimeSearchConfig {Limit = pageSize};

		// When
		var anime = await _tenrai.SearchAnimeAsync(config);

		// Then
		using var _ = new AssertionScope();
		anime.Data.Should().HaveCount(pageSize);
		anime.Data.First().Title.Should().Be("Cowboy Bebop");
		anime.Pagination.CurrentPage.Should().Be(1);
		anime.Pagination.Items.Count.Should().Be(pageSize);
		anime.Pagination.Items.PerPage.Should().Be(pageSize);
	}

	[Fact]
	public async Task SearchAnimeAsync_GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
	{
		// Given
		const int pageSize = 5;
		var config = new AnimeSearchConfig {Page = 2, Limit = pageSize};

		// When
		var anime = await _tenrai.SearchAnimeAsync(config);

		// Then
		using var _ = new AssertionScope();
		anime.Data.Should().HaveCount(pageSize);
		anime.Data.First().Title.Should().Be("Eyeshield 21");
		anime.Pagination.CurrentPage.Should().Be(2);
		anime.Pagination.Items.Count.Should().Be(pageSize);
		anime.Pagination.Items.PerPage.Should().Be(pageSize);
	}

	[Theory]
	[InlineData("berserk")]
	[InlineData("danganronpa")]
	[InlineData("death")]
	public async Task SearchAnimeAsync_NonEmptyQuery_ShouldReturnNotNullSearchAnime(string query)
	{
		// Given
		var config = new AnimeSearchConfig {Query = query};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(query);

		// Then
		returnedAnime.Should().NotBeNull();
	}

	[Fact]
	public async Task SearchAnimeAsync_OnePieceAiringQuery_ShouldReturnAiringOnePieceAnime()
	{
		// Given
		var config = new AnimeSearchConfig()
		{
			Query = "one p",
			Statuses = new[] { AiringStatus.Airing },
			Types = new[] { AnimeType.TV }
		};

		// When
		var onePieceAnime = await _tenrai.SearchAnimeAsync(config);

		// Then
		onePieceAnime.Data.First().Title.Should().Be("One Piece");
	}

	[Fact]
	public async Task SearchAnimeAsync_HaibaneQuery_ShouldReturnHaibaneRenmeiAnime()
	{
		// When
		var result = await _tenrai.SearchAnimeAsync("haibane");

		// Then
		var firstResult = result.Data.First();
		using var _ = new AssertionScope();
		firstResult.Title.Should().Be("Haibane Renmei");
		firstResult.Type.Should().Be("TV");
		firstResult.Episodes.Should().Be(13);
		firstResult.MalId.Should().Be(387);
	}

	[Theory]
	[InlineData("berserk")]
	[InlineData("danganronpa")]
	[InlineData("death")]
	public async Task SearchAnimeAsync_TVConfig_ShouldReturnNotNullSearchAnime(string query)
	{
		// Given
		var config = new AnimeSearchConfig
		{
			Query = query,
			Types = new[] { AnimeType.TV }
		};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(config);

		// Then
		returnedAnime.Should().NotBeNull();
	}

	[Fact]
	public async Task SearchAnimeAsync_DanganronpaTVConfig_ShouldReturnDanganronpaAnime()
	{
		// Given
		var config = new AnimeSearchConfig
		{
			Query = "danganronpa",
			Types = new[] { AnimeType.TV }
		};

		// When
		var anime = await _tenrai.SearchAnimeAsync(config);

		// Then
		anime.Pagination.LastVisiblePage.Should().Be(1);
	}

	[Fact]
	public async Task SearchAnimeAsync_FairyTailTVAbove7Config_ShouldFilterFairyTailAnimeScore()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			Query = "Fairy Tail",
			Types = new[] { AnimeType.TV },
			MinimumScore = 7
		};

		// When
		var anime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		using var _ = new AssertionScope();
		anime.Data.Should().Contain(x => x.Title.Equals("Fairy Tail (2014)"));
		anime.Data.Should().Contain(x => x.Title.Equals("Fairy Tail: Final Series"));
		anime.Data.Should().Contain(x => x.Title.Equals("Fairy Tail"));
	}

	[Fact]
	public async Task SearchAnimeAsync_BlameSciFiConfig_ShouldFilterBleachSciFi()
	{
		// Given
		var searchConfig = new AnimeSearchConfig { Query = "Blame" } ;
		searchConfig.Genres.Add(AnimeGenreSearch.SciFi);

		// When
		var anime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		anime.Data.Select(x => x.Title).Should().Contain("Blame! Movie");
	}

	[Fact]
	public async Task SearchAnimeAsync_BlameSciFiMovieConfig_ShouldFilterBleachMechaMovie()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			Query = "Blame",
			Types = new[] { AnimeType.Movie }
		};
		searchConfig.Genres.Add(AnimeGenreSearch.SciFi);

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		returnedAnime.Data.First().Title.Should().Be("Blame! Movie");
	}

	[Fact]
	public async Task SearchAnimeAsync_OneSortByMembersConfig_ShouldSortByPopularity()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			Query = "one",
			OrderBy = AnimeSearchOrderBy.Members,
			SortDirection = SortDirection.Descending
		};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		var titles = returnedAnime.Data.Select(x => x.Title);
		using var _ = new AssertionScope();
		titles.Should().Contain("One Piece");
		titles.First().Should().Be("One");
	}

	[Fact]
	public async Task SearchAnimeAsync_OneSortByIdConfig_ShouldSortByIdHachimitsuFirst()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			Query = "one",
			OrderBy = AnimeSearchOrderBy.Id,
			SortDirection = SortDirection.Ascending
		};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		returnedAnime.Data.First().Title.Should().Be("One");
	}

	[Fact]
	public async Task SearchAnimeAsync_ProducerKyotoAnimationConfig_ShouldReturnFMPAndLuckyStar()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			ProducerIds = { 2 }
		};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		returnedAnime.Data.Should().Contain(x => x.Title.Contains("Full Metal Panic? Fumoffu"));
		returnedAnime.Data.Should().Contain(x => x.Title.Contains("Lucky☆Star"));
	}

	[Fact]
	public async Task SearchAnimeAsync_IncorrectProducerConfig_ShouldReturnEmpty()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			ProducerIds = { -1 }
		};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		returnedAnime.Data.Should().BeEmpty();
	}

	[Fact]
	public async Task SearchAnimeAsync_EmptyQueryNullConfig_ShouldThrowValidationException()
	{
		// When
		var func = _tenrai.Awaiting(x => x.SearchAnimeAsync((AnimeSearchConfig)null));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Theory]
	[InlineData((AiringStatus)int.MaxValue, null, null, null, null, null)]
	[InlineData((AiringStatus)int.MinValue, null, null, null, null, null)]
	[InlineData(null, (AnimeAgeRating)int.MaxValue, null, null, null, null)]
	[InlineData(null, (AnimeAgeRating)int.MinValue, null, null, null, null)]
	[InlineData(null, null, (AnimeType)int.MaxValue, null, null, null)]
	[InlineData(null, null, (AnimeType)int.MinValue, null, null, null)]
	[InlineData(null, null, null, (AnimeSearchOrderBy)int.MaxValue, null, null)]
	[InlineData(null, null, null, (AnimeSearchOrderBy)int.MinValue, null, null)]
	[InlineData(null, null, null, AnimeSearchOrderBy.Episodes, (SortDirection)int.MaxValue, null)]
	[InlineData(null, null, null, AnimeSearchOrderBy.Episodes, (SortDirection)int.MinValue, null)]
	[InlineData(null, null, null, null, null, (AnimeGenreSearch)int.MaxValue)]
	[InlineData(null, null, null, null, null, (AnimeGenreSearch)int.MinValue)]
	public async Task SearchAnimeAsync_EmptyQueryWithConfigWithInvalidEnums_ShouldThrowValidationException(
		AiringStatus? airingStatus,
		AnimeAgeRating? rating,
		AnimeType? mangaType,
		AnimeSearchOrderBy? orderBy,
		SortDirection? sortDirection,
		AnimeGenreSearch? genreSearch
	)
	{
		// Given
		var searchConfig = new AnimeSearchConfig()
		{
			Statuses = new[] { airingStatus.GetValueOrDefault() },
			Ratings = new[] { rating.GetValueOrDefault() },
			Types = new[] { mangaType.GetValueOrDefault() },
			OrderBy = orderBy.GetValueOrDefault(),
			SortDirection = sortDirection.GetValueOrDefault(),
			Genres = genreSearch.HasValue ? new[] { genreSearch.Value } : Array.Empty<AnimeGenreSearch>()
		};

		// When
		var func = _tenrai.Awaiting(x => x.SearchAnimeAsync(searchConfig));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task SearchAnimeAsync_EmptyQueryActionTvAnime_ShouldFindCowboyBebopAndOnPiece()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			Types = new[] { AnimeType.TV },
			Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
		};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		var titles = returnedAnime.Data.Select(x => x.Title);
		using var _ = new AssertionScope();
		titles.Should().Contain("Cowboy Bebop");
		titles.Should().Contain("One Piece");
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task SearchAnimeAsync_EmptyQueryActionTvAnimeInvalidPage_ShouldThrowValidationException(int page)
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			Page = page,
			Types = new[] { AnimeType.TV },
			Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
		};

		// When
		var func = _tenrai.Awaiting(x => x.SearchAnimeAsync(searchConfig));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task SearchAnimeAsync_EmptyQueryActionTvAnimeFirstPage_ShouldFindCowboyBebopAndOnPiece()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			Page = 1,
			Types = new[] { AnimeType.TV },
			Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
		};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		var titles = returnedAnime.Data.Select(x => x.Title);
		using var _ = new AssertionScope();
		titles.Should().Contain("Cowboy Bebop");
		titles.Should().Contain("One Piece");
	}

	[Fact]
	public async Task SearchAnimeAsync_EmptyQueryActionTvAnimeThirdPage_ShouldFind()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			Page = 3,
			Types = new[] { AnimeType.TV },
			Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
		};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		var titles = returnedAnime.Data.Select(x => x.Title);
		using var _ = new AssertionScope();
		titles.Should().Contain("Elfen Lied");
		titles.Should().Contain("Samurai Champloo");
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task SearchAnimeAsync_GirlQueryActionCompletedAnimeInvalidPage_ShouldThrowValidationException(int page)
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			Page = page,
			Query = "girl",
			Statuses = new[] { AiringStatus.Complete },
			Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
		};

		// When
		var func = _tenrai.Awaiting(x => x.SearchAnimeAsync(searchConfig));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task SearchAnimeAsync_OneQueryActionCompletedAnimeSecondPage_ShouldReturnNotEmptyCollection()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			Query = "one",
			Page = 2,
			Statuses = new[] { AiringStatus.Complete },
			Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
		};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		using (new AssertionScope())
		{
			returnedAnime.Should().NotBeNull();
			returnedAnime.Data.Should().NotBeEmpty();
		}
	}

	[Fact]
	public async Task SearchAnimeAsync_GenreInclusion_ShouldReturnNotEmptyCollection()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action, AnimeGenreSearch.Comedy },
			OrderBy = AnimeSearchOrderBy.Score,
			SortDirection = SortDirection.Descending
		};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		returnedAnime.Data.Should().NotBeEmpty();
	}

	[Fact]
	public async Task SearchAnimeAsync_GenreExclusion_ShouldNotContainExcludedGenres()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			ExcludedGenres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
		};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		returnedAnime.Data.Should().NotBeEmpty();
		returnedAnime.Data.Should().OnlyContain(a => a.Genres.All(g => g.MalId != 1));
	}

	[Fact]
	public async Task SearchAnimeAsync_WithStartDateAndEndDate_ShouldReturnNotEmpty()
	{
		// Given
		var searchConfig = new AnimeSearchConfig
		{
			Query = "cowboy",
			StartDate = new DateTime(1998, 1, 1),
			EndDate = new DateTime(1999, 12, 31)
		};

		// When
		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		// Then
		using var _ = new AssertionScope();
		returnedAnime.Data.Should().NotBeEmpty();
		returnedAnime.Data.Should().Contain(a => a.MalId == 1L);
	}

	[Fact]
	public async Task SearchAnimeAsync_ExcludedTypes_LeavesThemOut()
	{
		var searchConfig = new AnimeSearchConfig
		{
			ExcludedTypes = new[] { AnimeType.Music, AnimeType.Cm, AnimeType.Pv },
			Limit = 100
		};

		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		using var _ = new AssertionScope();
		returnedAnime.Data.Should().HaveCount(100);
		returnedAnime.Data.Should().NotContain(a => a.Type == "Music" || a.Type == "CM" || a.Type == "PV");
	}

	[Fact]
	public async Task SearchAnimeAsync_OrderByAdded_IsAccepted()
	{
		var searchConfig = new AnimeSearchConfig { OrderBy = AnimeSearchOrderBy.Added, SortDirection = SortDirection.Descending };

		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		returnedAnime.Data.Should().NotBeEmpty();
	}

	[Fact]
	public async Task SearchAnimeAsync_EnglishDubOnCrunchyroll_HasBebop()
	{
		var searchConfig = new AnimeSearchConfig
		{
			VoiceLanguages = new[] { VoiceLanguage.English },
			StreamingServices = new[] { "Crunchyroll" }
		};

		var returnedAnime = await _tenrai.SearchAnimeAsync(searchConfig);

		using var _ = new AssertionScope();
		returnedAnime.Pagination.Items.Total.Should().BeGreaterThan(100);
		returnedAnime.Data.Should().Contain(a => a.MalId == 1L);
	}
}