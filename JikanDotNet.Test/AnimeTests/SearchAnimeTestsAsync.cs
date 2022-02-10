using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet.Consts;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
    public class SearchAnimeTestsAsync
    {
        private readonly IJikan _jikan;

        public SearchAnimeTestsAsync()
        {
            _jikan = new Jikan();
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
            var func = _jikan.Awaiting(x => x.SearchAnimeAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(26)]
        [InlineData(int.MaxValue)]
        public async Task SearchAnimeAsync_InvalidPageSize_ShouldThrowValidationException(int pageSize)
        {
            // Given
            var config = new AnimeSearchConfig {PageSize = pageSize};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchAnimeAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task SearchAnimeAsync_GivenSecondPage_ShouldReturnSecondPage()
        {
            // Given
            var config = new AnimeSearchConfig {Page = 2};
            
            // When
            var anime = await _jikan.SearchAnimeAsync(config);

            // Then
            using var _ = new AssertionScope();
            anime.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            anime.Data.First().Title.Should().Be("Rurouni Kenshin: Meiji Kenkaku Romantan - Tsuioku-hen");
            anime.Pagination.LastVisiblePage.Should().BeGreaterThan(780);
        }
        
        [Fact]
        public async Task SearchAnimeAsync_GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
        {
            // Given
            const int pageSize = 5;
            var config = new AnimeSearchConfig {PageSize = pageSize};
            
            // When
            var anime = await _jikan.SearchAnimeAsync(config);

            // Then
            using var _ = new AssertionScope();
            anime.Data.Should().HaveCount(pageSize);
            anime.Data.First().Title.Should().Be("Cowboy Bebop");
        }
        
        [Fact]
        public async Task SearchAnimeAsync_GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
        {
            // Given
            const int pageSize = 5;
            var config = new AnimeSearchConfig {Page = 2, PageSize = pageSize};
            
            // When
            var anime = await _jikan.SearchAnimeAsync(config);

            // Then
            using var _ = new AssertionScope();
            anime.Data.Should().HaveCount(pageSize);
            anime.Data.First().Title.Should().Be("Eyeshield 21");
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
            var returnedAnime = await _jikan.SearchAnimeAsync(query);

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
                Status = AiringStatus.Airing,
                Type = AnimeType.TV
            };

            // When
            var onePieceAnime = await _jikan.SearchAnimeAsync(config);

            // Then
            onePieceAnime.Data.First().Title.Should().Be("One Piece");
        }

        [Fact]
        public async Task SearchAnimeAsync_HaibaneQuery_ShouldReturnHaibaneRenmeiAnime()
        {
            // When
            var result = await _jikan.SearchAnimeAsync("haibane");

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
                Type = AnimeType.TV
            };

            // When
            var returnedAnime = await _jikan.SearchAnimeAsync(config);

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
                Type = AnimeType.TV
            };

            // When
            var anime = await _jikan.SearchAnimeAsync(config);

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
                Type = AnimeType.TV,
                Score = 7
            };

            // When
            var anime = await _jikan.SearchAnimeAsync(searchConfig);

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
            var anime = await _jikan.SearchAnimeAsync(searchConfig);

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
                Type = AnimeType.Movie
            };
            searchConfig.Genres.Add(AnimeGenreSearch.SciFi);

            // When
            var returnedAnime = await _jikan.SearchAnimeAsync(searchConfig);

            // Then
            returnedAnime.Data.First().Title.Should().Be("Blame! Movie");
        }

        [Fact]
        public async Task SearchAnimeAsync_OneSortByMembersConfig_ShouldSortByPopularityOPMFirst()
        {
            // Given
            var searchConfig = new AnimeSearchConfig
            {
                Query = "one",
                OrderBy = AnimeSearchSortable.Members,
                SortDirection = SortDirection.Descending
            };

            // When
            var returnedAnime = await _jikan.SearchAnimeAsync(searchConfig);

            // Then
            var titles = returnedAnime.Data.Select(x => x.Title);
            using var _ = new AssertionScope();
            titles.Should().Contain("One Piece");
            titles.Should().Contain("One Punch Man");
            titles.First().Should().Be("One Punch Man");
        }
        
		[Fact]
		public async Task SearchAnimeAsync_OneSortByIdConfig_ShouldSortByIdHachimitsuFirst()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Query = "one",
				OrderBy = AnimeSearchSortable.Id,
				SortDirection = SortDirection.Ascending
			};

			// When
			var returnedAnime = await _jikan.SearchAnimeAsync(searchConfig);

			// Then
			returnedAnime.Data.First().Title.Should().Be("Hachimitsu to Clover");
		}

		[Fact]
		public async Task SearchAnimeAsync_VioletProducerKyotoAnimationConfig_ShouldReturnVEGAsTop3()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Query = "violet",
				ProducerIds = { 2 }
			};

			// When
			var returnedAnime = await _jikan.SearchAnimeAsync(searchConfig);

			// Then
			returnedAnime.Data.Should().OnlyContain(x => x.Title.Contains("Evergarden") || x.Title.StartsWith("Ultra"));
		}

		[Fact]
		public async Task SearchAnimeAsync_VioletIncorrectProducerConfig_ShouldNotFilter()
		{
			Skip.If(true, "Seems like producer filter does not work 100% time yet. Recheck this.");
			
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Query = "violet",
				ProducerIds = { -1 }
			};

			// When
			var returnedAnime = await _jikan.SearchAnimeAsync(searchConfig);

			// Then
			returnedAnime.Data.Should().BeEmpty();
		}

		[Fact]
		public async Task SearchAnimeAsync_EmptyQueryNullConfig_ShouldThrowValidationException()
		{
			// When
			var func = _jikan.Awaiting(x => x.SearchAnimeAsync((AnimeSearchConfig)null));

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
		public async Task SearchAnimeAsync_EmptyQueryWithConfigWithInvalidEnums_ShouldThrowValidationException(
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
			var func = _jikan.Awaiting(x => x.SearchAnimeAsync(searchConfig));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task SearchAnimeAsync_EmptyQueryActionTvAnime_ShouldFindCowboyBebopAndOnPiece()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Type = AnimeType.TV,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			// When
			var returnedAnime = await _jikan.SearchAnimeAsync(searchConfig);

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
				Type = AnimeType.TV,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			// When
			var func = _jikan.Awaiting(x => x.SearchAnimeAsync(searchConfig));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task SearchAnimeAsync_EmptyQueryActionTvAnimeFirstPage_ShouldFindCowboyBebopAndOnPiece()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Page = 1,
				Type = AnimeType.TV,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			// When
			var returnedAnime = await _jikan.SearchAnimeAsync(searchConfig);

			// Then
			var titles = returnedAnime.Data.Select(x => x.Title);
			using var _ = new AssertionScope();
			titles.Should().Contain("Cowboy Bebop");
			titles.Should().Contain("One Piece");
		}

		[Fact]
		public async Task SearchAnimeAsync_EmptyQueryActionTvAnimeSecondPage_ShouldFindNanohaAndRozenMaiden()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Page = 2,
				Type = AnimeType.TV,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			// When
			var returnedAnime = await _jikan.SearchAnimeAsync(searchConfig);

			// Then
			var titles = returnedAnime.Data.Select(x => x.Title);
			using var _ = new AssertionScope();
			titles.Should().Contain("Mahou Shoujo Lyrical Nanoha");
			titles.Should().Contain("Rozen Maiden");
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
				Status = AiringStatus.Completed,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			// When
			var func = _jikan.Awaiting(x => x.SearchAnimeAsync(searchConfig));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task SearchAnimeAsync_OneQueryActionCompletedAnimeSecondPage_ShouldReturnNotEmptyCollection()
		{
			// Given
			var searchConfig = new AnimeSearchConfig
			{
				Query = "one",
				Page = 2,
				Status = AiringStatus.Completed,
				Genres = new List<AnimeGenreSearch> { AnimeGenreSearch.Action }
			};

			// When
			var returnedAnime = await _jikan.SearchAnimeAsync(searchConfig);

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
				OrderBy = AnimeSearchSortable.Score,
				SortDirection = SortDirection.Descending
			};

			// When
			var returnedAnime = await _jikan.SearchAnimeAsync(searchConfig);

			// Then
			returnedAnime.Data.Should().NotBeEmpty();
		}
    }
}