using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeRecommendationsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeRecommendationsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeRecommendationsAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeRecommendationsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeRecommendationsAsync_BebopId_ShouldParseCowboyBebopRecommendations()
		{
			// When
			var bebop = await _jikan.GetAnimeRecommendationsAsync(1);

			// Then
			using (new AssertionScope())
			{
				bebop.Data.First().Entry.MalId.Should().Be(205);
				bebop.Data.First().Entry.Title.Should().Be("Samurai Champloo");
				bebop.Data.First().Votes.Should().BeGreaterThan(70);
				bebop.Data.Count.Should().BeGreaterThan(100);
			}
		}
	}
}