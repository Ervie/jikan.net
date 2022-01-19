using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
	public class GetUserStatisticsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetUserStatisticsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserStatisticsAsync_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserStatisticsAsync(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserStatisticsAsync_Ervelan_ShouldParseErvelanStatistics()
		{
			// When
			var user = await _jikan.GetUserStatisticsAsync("Ervelan");

			// Then
			using (new AssertionScope())
			{
				user.Data.AnimeStatistics.Completed.Should().BeGreaterThan(600);
				user.Data.AnimeStatistics.MeanScore.Should().BeLessThan(6);
				user.Data.AnimeStatistics.EpisodesWatched.Should().BeGreaterThan(8500);
				user.Data.AnimeStatistics.TotalEntries.Should().BeGreaterThan(700);
				user.Data.AnimeStatistics.Dropped.Should().Be(6);
				user.Data.MangaStatistics.DaysRead.Should().BeGreaterThan(75);
				user.Data.MangaStatistics.ChaptersRead.Should().BeGreaterThan(12000);
				user.Data.MangaStatistics.VolumesRead.Should().BeGreaterThan(300);
			}
		}

		[Fact]
		public async Task GetUserStatisticsAsync_Nekomata1037_ShouldParseNekomataStatistics()
		{
			// When
			var user = await _jikan.GetUserStatisticsAsync("Nekomata1037");

			// Then
			using (new AssertionScope())
			{
				user.Data.AnimeStatistics.Completed.Should().BeGreaterThan(440);
				user.Data.AnimeStatistics.MeanScore.Should().BeGreaterThan(8);
				user.Data.AnimeStatistics.EpisodesWatched.Should().BeGreaterThan(6000);
				user.Data.AnimeStatistics.TotalEntries.Should().BeGreaterThan(900);
				user.Data.AnimeStatistics.Dropped.Should().Be(3);
				user.Data.MangaStatistics.DaysRead.Should().BeGreaterThan(85);
				user.Data.MangaStatistics.ChaptersRead.Should().BeGreaterThan(15000);
				user.Data.MangaStatistics.VolumesRead.Should().BeGreaterThan(700);
			}
		}
	}
}
