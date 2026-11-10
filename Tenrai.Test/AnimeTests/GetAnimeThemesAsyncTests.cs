using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.AnimeTests;

[Collection("TenraiTests")]
public class GetAnimeThemesAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetAnimeThemesAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetAnimeThemesAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeThemesAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetAnimeThemesAsync_BebopId_ShouldParseCowboyBebopOpeningsAndEndings()
	{
		// When
		var result = await _tenrai.GetAnimeThemesAsync(1);

		// Then
		using var _ = new AssertionScope();
		result.Data.Openings.Should().ContainSingle().Which.Equals("\"Tank!\" by The Seatbelts (eps 1-25)");
		result.Data.Endings.Should().HaveCount(3);
	}
}