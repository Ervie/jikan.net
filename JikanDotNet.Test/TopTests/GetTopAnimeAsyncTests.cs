﻿using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.TopTests
{
	public class GetTopAnimeAsyncTests
	{
		private readonly IJikan _jikan;

		public GetTopAnimeAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetTopAnimeAsync_NoParameter_ShouldParseTopAnime()
		{
			// When
			var top = await _jikan.GetTopAnimeAsync();

			top.Should().NotBeNull();
		}

		[Fact]
		public async Task GetTopAnimeAsync_NoParameter_ShouldParseFMA()
		{
			// When
			var top = await _jikan.GetTopAnimeAsync();

			// Then
			top.Data.First().Title.Should().Be("Fullmetal Alchemist: Brotherhood");
			top.Data.First().Episodes.Should().Be(64);
			top.Data.First().Source.Should().Be("Manga");
			top.Data.First().Duration.Should().Be("24 min per ep");
			top.Data.First().Producers.Should().HaveCount(4);
			top.Data.First().Licensors.Should().HaveCount(2);
			top.Data.First().Studios.Should().ContainSingle().Which.Name.Should().Be("Bones");
		}

		[Fact]
		public async Task GetTopAnimeAsync_NoParameter_ShouldParseLOGHType()
		{
			// When
			var top = await _jikan.GetTopAnimeAsync();

			// Then
			var logh = top.Data.Single(x => x.Title == "Ginga Eiyuu Densetsu");
			logh.Type.Should().Be("OVA");
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetTopAnimeAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetTopAnimeAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetTopAnimeAsync_SecondPage_ShouldParseAnimeSecondPage()
		{
			// When
			var top = await _jikan.GetTopAnimeAsync(2);

			// Then
			var titles = top.Data.Select(x => x.Title);
			using (new AssertionScope())
			{
				titles.Should().Contain("Monogatari Series: Second Season");
				titles.Should().Contain("Cowboy Bebop");
			}
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetTopAnimeAsync_ValidFilterInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetTopAnimeAsync(TopAnimeFilter.Airing, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}
		
		[Fact]
		public async Task GetTopAnimeAsync_FilterParameter_ShouldParseOP()
		{
			// When
			var top = await _jikan.GetTopAnimeAsync(TopAnimeFilter.Airing) ;

			// Then
			top.Data.First().Title.Should().Be("One Piece");
		}
		
		[Fact]
		public async Task GetTopAnimeAsync_FilterParameterWithSecondPage_ShouldParseNotOP()
		{
			// When
			var top = await _jikan.GetTopAnimeAsync(TopAnimeFilter.Airing, 2) ;

			// Then
			using var _ = new AssertionScope();
			top.Data.Should().HaveCount(25);
			top.Data.First().Title.Should().NotBe("One Piece");
		}
		
		[Fact]
		public async Task GetTopAnimeAsync_InvalidSearchConfig_ShouldThrowValidationException()
		{
			// When
			var func = _jikan.Awaiting(x => x.GetTopAnimeAsync(null));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetTopAnimeAsync_DefaultSearchConfig_ShouldParseFrieren()
		{
			// Given
			var searchConfig = new AnimeTopSearchConfig();

			// When
			var result = await _jikan.GetTopAnimeAsync(searchConfig);

			// Then
			result.Data.First().Titles.Should().Contain(x => x.Title.Equals("Sousou no Frieren"));
		}
		
		[Fact]
		public async Task GetTopAnimeAsync_SearchConfigWithPopularityFilter_ShouldParseShingeki()
		{
			// Given
			var searchConfig = new AnimeTopSearchConfig
			{
				Filter = TopAnimeFilter.ByPopularity
			};

			// When
			var result = await _jikan.GetTopAnimeAsync(searchConfig);

			// Then
			result.Data.First().Titles.Should().Contain(x => x.Title.Equals("Shingeki no Kyojin"));
		}
		
		[Fact]
		public async Task GetTopAnimeAsync_SearchConfigMovies_ShouldParseGintama()
		{
			// Given
			var searchConfig = new AnimeTopSearchConfig
			{
				Type = AnimeType.Movie
			};

			// When
			var result = await _jikan.GetTopAnimeAsync(searchConfig);

			// Then
			result.Data.First().Titles.Should().Contain(x => x.Title.Equals("Gintama: The Final"));
		}
	}
}