using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.AnimeTests;

[Collection("TenraiTests")]
public class GetAnimeEpisodeAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetAnimeEpisodeAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetAnimeEpisodeAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeEpisodeAsync(malId, 1));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetAnimeEpisodeAsync_ValidIdInvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeEpisodeAsync(1, page));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetAnimeEpisodeAsync_CardcaptorFirstEpisode_ShouldParseCardcaptorFirstEpisodeTitles()
	{
		// When
		var cardcaptorFirstEpisode = await _tenrai.GetAnimeEpisodeAsync(232, 1);

		// Then
		using (new AssertionScope())
		{
			cardcaptorFirstEpisode.Data.Title.Should().Be("Sakura and the Strange Magical Book");
			cardcaptorFirstEpisode.Data.TitleRomanji.Should().Be("Sakura to Fushigi na Mahou no Hon");
			cardcaptorFirstEpisode.Data.TitleJapanese.Should().Be("さくらと不思議な魔法の本");
		}
	}

	[Fact]
	public async Task GetAnimeEpisodeAsync_CardcaptorSakuraIdFirstEpisode_ShouldParseCardcaptorFirstEpisodeBasicData()
	{
		// When
		var cardcaptorFirstEpisode = await _tenrai.GetAnimeEpisodeAsync(232, 1);

		// Then
		using (new AssertionScope())
		{
			cardcaptorFirstEpisode.Data.Duration.Should().Be(1500);
			cardcaptorFirstEpisode.Data.Filler.Should().BeFalse();
			cardcaptorFirstEpisode.Data.Recap.Should().BeFalse();
		}
	}

	[Fact]
	public async Task GetAnimeEpisodeAsync_CardcaptorSakuraIdTenthEpisode_ShouldParseSynopsis()
	{
		// When
		var cardcaptorTenthEpisode = await _tenrai.GetAnimeEpisodeAsync(232, 10);

		// Then
		cardcaptorTenthEpisode.Data.Synopsis.Should().StartWith("It's Sports Day at Sakura's school");
	}
}