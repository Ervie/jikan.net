using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
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
			var func = _jikan.Awaiting(x => x.GetAnimeEpisodeAsync(malId, 1));

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
			var func = _jikan.Awaiting(x => x.GetAnimeEpisodeAsync(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeEpisodeAsync_CardcaptorFirstEpisode_ShouldParseCardcaptorFirstEpisodeTitles()
		{
			// When
			var cardcaptorFirstEpisode = await _jikan.GetAnimeEpisodeAsync(232, 1);

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
			var cardcaptorFirstEpisode = await _jikan.GetAnimeEpisodeAsync(232, 1);

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
			var cardcaptorTenthEpisode = await _jikan.GetAnimeEpisodeAsync(232, 10);

			// Then
			cardcaptorTenthEpisode.Data.Synopsis.Should().StartWith("It's Sports Day at Sakura's school");
		}
	}
}