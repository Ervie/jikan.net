using FluentAssertions;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.GenreTests
{
	public class GetMangaGenresAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMangaGenresAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetMangaGenresAsync_NoParameters_ShouldParseAllAvailableGenres()
		{
			// Given
			const int expectedGenreCount = 91;

			// When
			var result = await _jikan.GetMangaGenresAsync();

			// Then
			result.Data.Should().HaveCount(expectedGenreCount);
		}

		[Theory]
		[InlineData(GenresFilter.Genres, 64)]
		[InlineData(GenresFilter.ExplicitGenres, 3)]
		[InlineData(GenresFilter.Themes, 19)]
		[InlineData(GenresFilter.Demographics, 5)]
		public async Task GetMangaGenresAsync_WithFilter_ShouldParseFilteredGenres(GenresFilter filter, int expectedGenreCount)
		{
			// When
			var result = await _jikan.GetMangaGenresAsync(filter);

			// Then
			result.Data.Should().HaveCount(expectedGenreCount);
		}

		[Theory]
		[InlineData((GenresFilter)int.MaxValue)]
		[InlineData((GenresFilter)int.MinValue)]
		public async Task GetMangaGenresAsync_InvalidFilter_ShouldThrowValidationException(GenresFilter filter)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaGenresAsync(filter));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}
	}
}