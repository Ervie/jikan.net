using FluentAssertions;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.GenreTests
{
	[Collection("JikanTests")]
	public class GetAnimeGenresAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeGenresAsyncTests(JikanFixture jikanFixture)
		{
			_jikan = jikanFixture.Jikan;
		}

		[Fact]
		public async Task GetAnimeGenresAsync_NoParameters_ShouldParseAllAvailableGenres()
		{
			// Given
			const int expectedGenreCount = 78;

			// When
			var result = await _jikan.GetAnimeGenresAsync();

			// Then
			result.Data.Should().HaveCount(expectedGenreCount);
		}

		[Theory]
		[InlineData(GenresFilter.Genres, 18)]
		[InlineData(GenresFilter.ExplicitGenres, 3)]
		[InlineData(GenresFilter.Themes, 52)]
		[InlineData(GenresFilter.Demographics, 5)]
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