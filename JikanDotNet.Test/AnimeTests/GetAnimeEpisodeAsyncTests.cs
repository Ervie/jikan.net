using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeEpisodeAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeEpisodeAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeEpisodeAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<AnimeEpisode>> func = _jikan.Awaiting(x => x.GetAnimeEpisodeAsync(malId, 1));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeEpisodeAsync_ValidIdInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<AnimeEpisode>> func = _jikan.Awaiting(x => x.GetAnimeEpisodeAsync(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeEpisodeAsync_BebopIdFirstEpisode_ShouldParseCowboyBebopFirstEpisodeTitles()
		{
			// When
			var bebopFirstEpisode = await _jikan.GetAnimeEpisodeAsync(1, 1);

			// Then
			using (new AssertionScope())
			{
				bebopFirstEpisode.Title.English.Should().Be("Sakura and the Strange Magical Book");
				bebopFirstEpisode.Title.Romaji.Should().Be("Sakura to Fushigi na Mahou no Hon");
				bebopFirstEpisode.Title.Japanese.Should().Be("さくらと不思議な魔法の本");
			}
		}

		[Fact]
		public async Task GetAnimeEpisodeAsync_CardcaptorSakuraIdFirstEpisode_ShouldParseCardcaptorFirstEpisodeBasicData()
		{
			// When
			var cardcaptorFirstEpisode = await _jikan.GetAnimeEpisodeAsync(232, 1);

			// Then
			using (new AssertionScope())
			{
				cardcaptorFirstEpisode.Duration.Should().Be("00:25:00");
				cardcaptorFirstEpisode.Filler.Should().BeFalse();
				cardcaptorFirstEpisode.Recap.Should().BeFalse();
			}
		}

		[Fact]
		public async Task GetAnimeEpisodeAsync_CardcaptorSakuraIdTenthEpisode_ShouldParseCardcaptorTentEpisodeBasicDataCrunchyroll()
		{
			// When
			var cardcaptorTenthEpisode = await _jikan.GetAnimeEpisodeAsync(232, 1);

			// Then
			using (new AssertionScope())
			{
				cardcaptorTenthEpisode.Crunchyroll.Should().NotBeNull();
				cardcaptorTenthEpisode.Crunchyroll.Url.Should().NotBeNullOrWhiteSpace();
			}
		}
	}
}