using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeStatisticsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeStatisticsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeStatisticsAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeStatisticsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeStatistics_BebopId_ShouldParseCowboyBebopStats()
		{
			// When
			var bebop = await _jikan.GetAnimeStatisticsAsync(1);

			// Then
			using (new AssertionScope())
			{
				bebop.Data.ScoreStats.Should().NotBeNull();
				bebop.Data.Completed.Should().BeGreaterThan(450000);
				bebop.Data.PlanToWatch.Should().BeGreaterThan(50000);
				bebop.Data.Total.Should().BeGreaterThan(1500000);
				bebop.Data.ScoreStats.Should().HaveCount(10);
				bebop.Data.ScoreStats.Should().Contain(score => score.Score == 5 && score.Votes > 10000);
			}
		}
	}
}