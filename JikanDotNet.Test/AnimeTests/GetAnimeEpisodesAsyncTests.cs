using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeEpisodesAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeEpisodesAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeEpisodesAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeEpisodesAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeEpisodesAsync_BebopId_ShouldParseCowboyBebopEpisodeWithAiredDate()
		{
			// Given
			var expectedDate = new DateTime(1998, 10, 24);

			// When
			var bebop = await _jikan.GetAnimeEpisodesAsync(1);

			// Then
			using (new AssertionScope())
			{
				bebop.Data.Should().HaveCount(26);
				bebop.Data.First().Aired.Should().BeSameDateAs(expectedDate);
			}
		}

		[Fact]
		public async Task GetAnimeEpisodesAsync_BebopId_ShouldParseCowboyBebopFirstEpisodeTitles()
		{
			// When
			var bebop = await _jikan.GetAnimeEpisodesAsync(1);

			// Then
			var firstEpisodeTitle = bebop.Data.First();
			using (new AssertionScope())
			{
				firstEpisodeTitle.Title.Should().Be("Asteroid Blues");
				firstEpisodeTitle.TitleRomanji.Should().StartWith("Asteroid Blues");
				firstEpisodeTitle.TitleJapanese.Should().Be("アステロイド・ブルース");
			}
		}

		[Fact]
		public async Task GetAnimeEpisodesAsync_BebopId_ShouldParseCowboyBebopLastEpisodeTitles()
		{
			// When
			var bebop = await _jikan.GetAnimeEpisodesAsync(1);

			// Then
			var firstEpisodeTitle = bebop.Data.Last();
			using (new AssertionScope())
			{
				firstEpisodeTitle.Title.Should().Be("The Real Folk Blues (Part 2)");
				firstEpisodeTitle.TitleRomanji.Should().StartWith("The Real Folk Blues (Kouhen)");
				firstEpisodeTitle.TitleJapanese.Should().Be("ザ・リアル・フォークブルース（後編）");
			}
		}

		[Fact]
		public async Task GetAnimeEpisodesAsync_CardcaptorId_ShouldParseCardcaptorSakuraFirstEpisodeForumTopic()
		{
			// When
			var cardcaptor = await _jikan.GetAnimeEpisodesAsync(232);

			// Then
			var firstEpisode = cardcaptor.Data.First();
			using (new AssertionScope())
			{
				firstEpisode.Url.Should().Be("https://myanimelist.net/anime/232/Cardcaptor_Sakura/episode/1");
				firstEpisode.MalId.Should().Be(1);
			}
		}
	}
}