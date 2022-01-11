using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
	public class GetMangaStatisticsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMangaStatisticsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaStatisticsAsync_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaStatisticsAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaStatistics_MonsterId_ShouldParseMonsterStats()
		{
			// When
			var monster = await _jikan.GetMangaStatisticsAsync(1);

			// Then
			using (new AssertionScope())
			{
				monster.Data.ScoreStats.Should().NotBeNull();
				monster.Data.Completed.Should().BeGreaterThan(25000);
				monster.Data.Dropped.Should().BeGreaterThan(500);
				monster.Data.Total.Should().BeGreaterThan(160000);
				monster.Data.ScoreStats.Should().Contain(x => x.Score.Equals(8) && x.Votes > 8500);
			}
		}
	}
}
