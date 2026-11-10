using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.CharacterTests;

[Collection("TenraiTests")]
public class GetCharacterMangaAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetCharacterMangaAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetCharacterMangaAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetCharacterMangaAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetCharacterMangaAsync_SpikeSpiegelId_ShouldParseSpikeSpiegelAnime()
	{
		// When
		var spike = await _tenrai.GetCharacterMangaAsync(1);

		// Then
		using (new AssertionScope())
		{
			spike.Data.Should().HaveCount(2);
			spike.Data.Should().OnlyContain(x => x.Role.Equals("Main"));
			spike.Data.Should().OnlyContain(
				x => !string.IsNullOrWhiteSpace(x.Manga.Images.JPG.ImageUrl)
				&& !string.IsNullOrWhiteSpace(x.Manga.Images.JPG.SmallImageUrl)
				&& !string.IsNullOrWhiteSpace(x.Manga.Images.JPG.LargeImageUrl)
			);
		}
	}
}
