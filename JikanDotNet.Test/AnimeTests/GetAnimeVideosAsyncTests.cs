using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeVideosAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeVideosAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeVideosAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeVideosAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeVideosAsync_BebopId_ShouldParseCowboyBebopVideos()
		{
			// When
			var bebop = await _jikan.GetAnimeVideosAsync(1);

			// Then
			using (new AssertionScope())
			{
				bebop.Data.PromoVideos.Should().HaveCount(3);
				bebop.Data.PromoVideos.Select(x => x.Title).Should().Contain("PV 2");
				bebop.Data.EpisodeVideos.Should().HaveCount(26);
				bebop.Data.EpisodeVideos.Select(x => x.Title).Should().Contain("Pierrot Le Fou");
				bebop.Data.MusicVideos.Should().BeEmpty();
			}
		}

		[Fact]
		public async Task GetAnimeVideosAsync_NarutoId_ShouldParseNarutoMusicVideos()
		{
			// When
			var naruto = await _jikan.GetAnimeVideosAsync(20);

			// Then
			using (new AssertionScope())
			{
				naruto.Data.MusicVideos.Should().HaveCountGreaterOrEqualTo(5);
				naruto.Data.MusicVideos.Select(x => x.Title).Should().Contain("OP 2 (Artist ver.)");
				naruto.Data.MusicVideos.Select(x => x.Metadata.Author).Should().Contain("Analogfish");
			}
		}

		[Fact]
		public async Task GetAnimeVideosAsync_OnePieceId_ShouldParseOnePieceMusicVideos()
		{
			// When
			var op = await _jikan.GetAnimeVideosAsync(21);

			// Then
			op.Data.MusicVideos.Should().HaveCountGreaterOrEqualTo(20);
		}
	}
}