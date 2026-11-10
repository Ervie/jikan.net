using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.CharacterTests;

[Collection("TenraiTests")]
public class GetCharacterAnimeAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetCharacterAnimeAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetCharacterAnimeAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetCharacterAnimeAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetCharacterAnimeAsync_SpikeSpiegelId_ShouldParseSpikeSpiegelAnime()
	{
		// When
		var spike = await _tenrai.GetCharacterAnimeAsync(1);

		// Then
		using (new AssertionScope())
		{
			spike.Data.Should().HaveCount(3);
			spike.Data.Should().OnlyContain(x => x.Role.Equals("Main"));
			spike.Data.Should().OnlyContain(
				x => !string.IsNullOrWhiteSpace(x.Anime.Images.JPG.ImageUrl)
				&& !string.IsNullOrWhiteSpace(x.Anime.Images.JPG.SmallImageUrl)
				&& !string.IsNullOrWhiteSpace(x.Anime.Images.JPG.LargeImageUrl)
			);
		}
	}

	[Fact]
	public async Task GetCharacterAnimeAsync_IchigoKurosakiId_ShouldParseIchigoKurosakiAnime()
	{
		// When
		var ichigo = await _tenrai.GetCharacterAnimeAsync(5);

		// Then
		using (new AssertionScope())
		{
			ichigo.Data.Should().HaveCountGreaterOrEqualTo(10);
			ichigo.Data.Select(x => x.Anime.Title).Should().Contain("Bleach");
		}
	}
}