using FluentAssertions;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.GenreTests;

[Collection("TenraiTests")]
public class GetAnimeGenresAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetAnimeGenresAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Fact]
	public async Task GetAnimeGenresAsync_NoParameters_ShouldParseAllAvailableGenres()
	{
		// Given
		const int expectedGenreCount = 78;

		// When
		var result = await _tenrai.GetAnimeGenresAsync();

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
		var result = await _tenrai.GetAnimeGenresAsync(filter);

		// Then
		result.Data.Should().HaveCount(expectedGenreCount);
	}

	[Fact]
	public async Task GetAnimeGenresAsync_Sfw_NoHentai()
	{
		// When
		var result = await _tenrai.GetAnimeGenresAsync(new GenresConfig { Sfw = true });

		// Then
		result.Data.Should().NotBeEmpty();
		result.Data.Should().NotContain(genre => genre.Name == "Hentai");
	}

	[Theory]
	[InlineData((GenresFilter)int.MaxValue)]
	[InlineData((GenresFilter)int.MinValue)]
	public async Task GetAnimeGenresAsync_InvalidFilter_ShouldThrowValidationException(GenresFilter filter)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeGenresAsync(filter));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}
}