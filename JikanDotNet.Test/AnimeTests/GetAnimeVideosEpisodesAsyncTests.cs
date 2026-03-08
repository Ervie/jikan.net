using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	[Collection("JikanTests")]
	public class GetAnimeVideosEpisodesAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeVideosEpisodesAsyncTests(JikanFixture jikanFixture)
		{
			_jikan = jikanFixture.Jikan;
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeVideosEpisodesAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeVideosEpisodesAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeVideosEpisodesAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeVideosEpisodesAsync(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeVideosEpisodesAsync_BebopId_ShouldParseCowboyBebopEpisodes()
		{
			// When
			var bebop = await _jikan.GetAnimeVideosEpisodesAsync(1);

			// Then
			using var _ = new AssertionScope();
			bebop.Data.Should().NotBeEmpty();
			bebop.Data.Should().OnlyContain(e => !string.IsNullOrWhiteSpace(e.Title) && !string.IsNullOrWhiteSpace(e.Url));
			bebop.Data.Select(e => e.Title).Should().Contain("Asteroid Blues");
			bebop.Pagination.Should().NotBeNull();
		}

		[Fact]
		public async Task GetAnimeVideosEpisodesAsync_BebopIdWithPage_ShouldParseCowboyBebopEpisodes()
		{
			// When
			var bebop = await _jikan.GetAnimeVideosEpisodesAsync(1, 1);

			// Then
			using var _ = new AssertionScope();
			bebop.Data.Should().NotBeEmpty();
			bebop.Data.Select(e => e.Title).Should().Contain("Asteroid Blues");
			bebop.Pagination.Should().NotBeNull();
		}
	}
}
