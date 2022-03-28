using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using JikanDotNet.Consts;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
    public class SearchMangaAsyncTests
    {
        private readonly IJikan _jikan;

        public SearchMangaAsyncTests()
        {
            _jikan = new Jikan();
        }
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task SearchMangaAsync_InvalidPage_ShouldThrowValidationException(int page)
        {
            // Given
            var config = new MangaSearchConfig {Page = page};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchMangaAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(26)]
        [InlineData(int.MaxValue)]
        public async Task SearchMangaAsync_InvalidPageSize_ShouldThrowValidationException(int pageSize)
        {
            // Given
            var config = new MangaSearchConfig {PageSize = pageSize};
            
            // When
            var func = _jikan.Awaiting(x => x.SearchMangaAsync(config));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task SearchMangaAsync_GivenSecondPage_ShouldReturnSecondPage()
        {
            // Given
            var config = new MangaSearchConfig {Page = 2};
            
            // When
            var manga = await _jikan.SearchMangaAsync(config);

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(ParameterConsts.MaximumPageSize);
            manga.Data.First().Title.Should().Be("Nana");
            manga.Pagination.LastVisiblePage.Should().BeGreaterThan(780);
        }
        
        [Fact]
        public async Task SearchMangaAsync_GivenValidPageSize_ShouldReturnPageSizeNumberOfRecords()
        {
            // Given
            const int pageSize = 5;
            var config = new MangaSearchConfig {PageSize = pageSize};
            
            // When
            var manga = await _jikan.SearchMangaAsync(config);

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(pageSize);
            manga.Data.First().Title.Should().Be("Monster");
        }
        
        [Fact]
        public async Task SearchMangaAsync_GivenValidPageAndPageSize_ShouldReturnPageSizeNumberOfRecordsFromNextPage()
        {
            // Given
            const int pageSize = 5;
            var config = new MangaSearchConfig {Page = 2, PageSize = pageSize};
            
            // When
            var manga = await _jikan.SearchMangaAsync(config);

            // Then
            using var _ = new AssertionScope();
            manga.Data.Should().HaveCount(pageSize);
            manga.Data.First().Title.Should().Be("Full Moon wo Sagashite");
        }
        
        [Theory]
        [InlineData("berserk")]
        [InlineData("monster")]
        [InlineData("death")]
        public async Task SearchMangaAsync_NonEmptyQuery_ShouldReturnNotNullSearchManga(string query)
        {
            // When
            var returnedManga = await _jikan.SearchMangaAsync(query);

            // Then
            returnedManga.Data.Should().NotBeEmpty();
        }

        [Fact]
        public async Task SearchManga_DanganronpaQuery_ShouldReturnDanganronpaManga()
        {
            // When
            var danganronpaManga = await _jikan.SearchMangaAsync("danganronpa");

            danganronpaManga.Data.Should().HaveCountGreaterThan(15);
        }
        
        [Fact]
        public async Task SearchMangaAsync_YotsubaQuery_ShouldReturnYotsubatoManga()
        {
            // When
            var yotsubato = await _jikan.SearchMangaAsync("yotsuba");

            // Then
            var firstResult = yotsubato.Data.First();
            using var _ = new AssertionScope();
            firstResult.Title.Should().Be("Yotsuba to!");
            firstResult.Type.Should().Be("Manga");
            firstResult.Volumes.Should().BeNull();
            firstResult.MalId.Should().Be(104);
        }

        [Fact]
        public async Task SearchMangaAsync_YotsubatoQueryNullConfig_ShouldThrowValidationException()
        {
            // When
            var func = _jikan.Awaiting(x => x.SearchMangaAsync((MangaSearchConfig) null));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }
        
        [Fact]
        public async Task SearchMangaAsync_YotsubaPublishingQuery_ShouldReturnPublishedYotsubatoManga()
        {
            // Given
            var searchConfig = new MangaSearchConfig()
            {
                Query = "yotsuba",
                Status = PublishingStatus.Publishing
            };

            // When
            var yotsubato = await _jikan.SearchMangaAsync(searchConfig);

            // Then
            var firstResult = yotsubato.Data.First();
            using var _ = new AssertionScope();
            firstResult.Title.Should().Be("Yotsuba to!");
            firstResult.Type.Should().Be("Manga");
            firstResult.Volumes.Should().BeNull();
            firstResult.MalId.Should().Be(104);
        }
        
        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task SearchMangaAsync_YotsubaQueryInvalidPage_ShouldThrowValidationException(int page)
        {
            // Given
            var searchConfig = new MangaSearchConfig()
            {
                Query = "yotsuba",
                Page = page
            };
            
            // When
            var func = _jikan.Awaiting(x => x.SearchMangaAsync(searchConfig));

            // Then
            await func.Should().ThrowExactlyAsync<JikanValidationException>();
        }

        [Fact]
        public async Task SearchMangaAsync_GirlQuerySecondPage_ShouldFindGirlManga()
        {
            // Given
            var searchConfig = new MangaSearchConfig()
            {
                Query = "girl",
                Page = 2
            };
            
            // When
            var returnedAnime = await _jikan.SearchMangaAsync(searchConfig);

            // Then
            using var _ = new AssertionScope();
            returnedAnime.Data.Select(x => x.Title).Should().Contain("Misty Girl"); 
            returnedAnime.Pagination.LastVisiblePage.Should().BeGreaterThan(15);
        }
        
        [Theory]
        [InlineData("berserk")]
        [InlineData("monster")]
        [InlineData("death")]
        public async Task SearchMangaAsync_MangaConfig_ShouldReturnNotNullSearchManga(string query)
        {
            // Given
            var searchConfig = new MangaSearchConfig
            {
                Type = MangaType.Manga
            };

            // When
            var returnedManga = await _jikan.SearchMangaAsync(searchConfig);

            // Then
            returnedManga.Data.Should().NotBeEmpty();
        }

        [Fact]
        public async Task SearchMangaAsync_DanganronpaMangaConfig_ShouldReturnDanganronpaManga()
        {
            // Given
            var searchConfig = new MangaSearchConfig
            {
                Query = "danganronpa",
                Type = MangaType.Manga
            };

            // When
            var danganronpaManga = await _jikan.SearchMangaAsync(searchConfig);

            // Then
            danganronpaManga.Data.Should().HaveCountGreaterThan(12);
        }
        
        
		[Fact]
		public async Task SearchMangaAsync_DanganronpaMangaAbove7Config_ShouldReturnDanganronpaMangaScore()
		{
			// Given
			var searchConfig = new MangaSearchConfig
			{
				Query = "danganronpa",
				Type = MangaType.Manga,
				MinimumScore = 7
			};

			// When
			var danganronpaManga = await _jikan.SearchMangaAsync(searchConfig);

			danganronpaManga.Data.First().Title.Should().Contain("Dangan");
		}

		[Fact]
		public async Task SearchMangaAsync_MangaGameGenreConfig_ShouldFilterMetalFightBeyblade()
		{
			// Given
			var searchConfig = new MangaSearchConfig
			{
				Query = "metal",
				Type = MangaType.Manga,
			};
			searchConfig.Genres.Add(MangaGenreSearch.Game);

			// When
			var returnedManga = await _jikan.SearchMangaAsync(searchConfig);

			returnedManga.Data.First().Title.Should().Be("Metal Fight Beyblade");
		}

		[Fact]
		public async Task SearchMangaAsync_MetalSortByMembersConfig_ShouldSortByPopularityFMAFirst()
		{
			// Given
			var searchConfig = new MangaSearchConfig
			{
				Query = "metal",
				OrderBy = MangaSearchOrderBy.Members,
				SortDirection = SortDirection.Descending
			};

			// When
			var returnedManga = await _jikan.SearchMangaAsync(searchConfig);

			// Then
			var titles = returnedManga.Data.Select(x => x.Title);
			using (new AssertionScope())
			{
				titles.Should().Contain("Fullmetal Alchemist");
				titles.Should().Contain("Metallica Metalluca");
				titles.Should().Contain("Full Metal Panic!");
				returnedManga.Data.First().Title.Should().Be("Fullmetal Alchemist");
			}
		}

		[Fact]
		public async Task SearchMangaAsync_OneSortByIdConfig_ShouldSortByIdOnePieceFirst()
		{
			// Given
			var searchConfig = new MangaSearchConfig
			{
				Query = "one",
				OrderBy = MangaSearchOrderBy.MalId,
				SortDirection = SortDirection.Ascending
			};

			// When
			var returnedManga = await _jikan.SearchMangaAsync(searchConfig);

			// Then
			returnedManga.Data.First().Title.Should().Be("One Piece");
		}

		[Fact]
		public async Task SearchMangaAsync_ShonenJumpMagazineConfig_ShouldReturnNarutoAndBleach()
		{
			// Given
			var searchConfig = new MangaSearchConfig
			{
				MagazineIds = { 83 }
			};

			// When
			var returnedManga = await _jikan.SearchMangaAsync(searchConfig);

			// Then
			using (new AssertionScope())
			{
				returnedManga.Data.First().Title.Should().Be("Naruto");
				returnedManga.Data.Skip(1).First().Title.Should().Be("Bleach");
				returnedManga.Data.Should().HaveCount(25);
			}
		}

		[Theory]
		[InlineData((PublishingStatus)int.MaxValue, null, null, null, null)]
		[InlineData((PublishingStatus)int.MinValue, null, null, null, null)]
		[InlineData(null, (MangaType)int.MaxValue, null, null, null)]
		[InlineData(null, (MangaType)int.MinValue, null, null, null)]
		[InlineData(null, null, (MangaSearchOrderBy)int.MaxValue, null, null)]
		[InlineData(null, null, (MangaSearchOrderBy)int.MinValue, null, null)]
		[InlineData(null, null, MangaSearchOrderBy.Chapters, (SortDirection)int.MaxValue, null)]
		[InlineData(null, null, MangaSearchOrderBy.Chapters, (SortDirection)int.MinValue, null)]
		[InlineData(null, null, null, null, (MangaGenreSearch)int.MaxValue)]
		[InlineData(null, null, null, null, (MangaGenreSearch)int.MinValue)]
		public async Task SearchMangaAsync_EmptyQueryWithConfigWithInvalidEnums_ShouldThrowValidationException(
			PublishingStatus? airingStatus,
			MangaType? mangaType,
			MangaSearchOrderBy? orderBy,
			SortDirection? sortDirection,
			MangaGenreSearch? genreSearch
		)
		{
			// Given
			var searchConfig = new MangaSearchConfig()
			{
				Status = airingStatus.GetValueOrDefault(),
				Type = mangaType.GetValueOrDefault(),
				OrderBy = orderBy.GetValueOrDefault(),
				SortDirection = sortDirection.GetValueOrDefault(),
				Genres = genreSearch.HasValue ? new[] { genreSearch.Value } : Array.Empty<MangaGenreSearch>()
			};

			// When
			var func = _jikan.Awaiting(x => x.SearchMangaAsync(searchConfig));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task SearchMangaAsync_EmptyQueryActionManga_ShouldFindBerserkAndBlackCat()
		{
			// Given
			var searchConfig = new MangaSearchConfig
			{
				Genres = new List<MangaGenreSearch> { MangaGenreSearch.Action }
			};

			// When
			var returnedManga = await _jikan.SearchMangaAsync(searchConfig);

			// Then
			var titles = returnedManga.Data.Select(x => x.Title);
			using var _ = new AssertionScope();
			titles.Should().Contain("Berserk");
			titles.Should().Contain("Black Cat");
		}

		[Fact]
		public async Task SearchMangaAsync_EmptyQueryActionMangaFirstPage_ShouldFindBerserkAndBlackCat()
		{
			// Given
			var searchConfig = new MangaSearchConfig
			{
				Page = 1,
				Genres = new List<MangaGenreSearch> { MangaGenreSearch.Action }
			};

			// When
			var returnedManga = await _jikan.SearchMangaAsync(searchConfig);

			// Then
			var titles = returnedManga.Data.Select(x => x.Title);
			using var _ = new AssertionScope();
			titles.Should().Contain("Berserk");
			titles.Should().Contain("Black Cat");
		}

		[Fact]
		public async Task SearchMangaAsync_EmptyQueryActionMangaSecondPage_ShouldFindHakushoAndAirGear()
		{
			// Given
			var searchConfig = new MangaSearchConfig
			{
				Page = 2,
				Genres = new List<MangaGenreSearch> { MangaGenreSearch.Action 
			};

			// When
			var returnedManga = await _jikan.SearchMangaAsync(searchConfig);

			// Then
			var titles = returnedManga.Data.Select(x => x.Title);
			using var _ = new AssertionScope();
			titles.Should().Contain("Yuu☆Yuu☆Hakusho");
			titles.Should().Contain("Air Gear");
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task SearchMangaAsync_OreQueryWithConfigInvalidPage_ShouldThrowValidationException(int page)
		{
			// Given
			var searchConfig = new MangaSearchConfig()
			{
				Query = "ore",
				Page = page,
				Status = PublishingStatus.Publishing
			};

			// When
			var func = _jikan.Awaiting(x => x.SearchMangaAsync(searchConfig));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task SearchMangaAsync_OreQueryComedyMangaSecondPage_ShouldReturnNotEmptyCollection()
		{
			// Given
			var searchConfig = new MangaSearchConfig
			{
				Query = "ore",
				Page = 2,
				Genres = new List<MangaGenreSearch> { MangaGenreSearch.Comedy },
				Type = MangaType.Manga
			};

			// When
			var returnedManga = await _jikan.SearchMangaAsync(searchConfig);

			// Then
			returnedManga.Data.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task SearchMangaAsync_GenreInclusion_ShouldReturnNotEmptyCollection()
		{
			// Given
			var searchConfig = new MangaSearchConfig
			{
				Genres = new List<MangaGenreSearch> { MangaGenreSearch.Comedy },
				Type = MangaType.Manga
			};

			// When
			var returnedManga = await _jikan.SearchMangaAsync(searchConfig);

			// Then
			returnedManga.Data.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task SearchMangaAsync_GenreExclusion_ShouldReturnNotEmptyCollection()
		{
			// Given
			var searchConfig = new MangaSearchConfig
			{
				ExcludedGenres = new List<MangaGenreSearch> { MangaGenreSearch.Comedy },
				Type = MangaType.Manga
			};

			// When
			var returnedManga = await _jikan.SearchMangaAsync(searchConfig);

			// Then
			returnedManga.Data.Should().NotBeNullOrEmpty();
		}
    }
}