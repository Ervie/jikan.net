using FluentAssertions;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.GenreTests
{
	public class GetAnimeGenresAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeGenresAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetAnimeGenresAsync_NoParameters_ShouldParseAllAvailableGenres()
		{
			// Given
			const int expectedGenreCount = 114;

			// When
			var result = await _jikan.GetAnimeGenresAsync();

			// Then
			result.Data.Should().HaveCount(expectedGenreCount);
		}

		[Theory]
		[InlineData(GenresFilter.Genres, 55)]
		[InlineData(GenresFilter.ExplicitGenres, 6)]
		[InlineData(GenresFilter.Themes, 41)]
		[InlineData(GenresFilter.Demographics, 12)]
		public async Task GetAnimeGenresAsync_WithFilter_ShouldParseFilteredGenres(GenresFilter filter, int expectedGenreCount)
		{
			// When
			var result = await _jikan.GetAnimeGenresAsync(filter);

			// Then
			result.Data.Should().HaveCount(expectedGenreCount);
		}

		[Theory]
		[InlineData((GenresFilter)int.MaxValue)]
		[InlineData((GenresFilter)int.MinValue)]
		public async Task GetAnimeGenresAsync_InvalidFilter_ShouldThrowValidationException(GenresFilter filter)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeGenresAsync(filter));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}
	}
}