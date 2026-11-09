using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.SeasonTests
{
	[Collection("TenraiTests")]
	public class GetPreviousSeasonAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetPreviousSeasonAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Fact]
		public async Task GetPreviousSeasonAsync_IsSeasonBeforeCurrent()
		{
			var current = MostCommonSeason((await _tenrai.GetCurrentSeasonAsync()).Data);
			var previous = MostCommonSeason((await _tenrai.GetPreviousSeasonAsync()).Data);

			previous.Should().Be(SeasonBefore(current));
		}

		// Both lists carry shows continuing from earlier seasons, so the bulk of the page names the season
		private static (Season Season, int Year) MostCommonSeason(ICollection<Anime> anime)
		{
			return anime
				.Where(x => x.Season.HasValue && x.Year.HasValue)
				.GroupBy(x => (x.Season.Value, x.Year.Value))
				.OrderByDescending(x => x.Count())
				.First()
				.Key;
		}

		private static (Season Season, int Year) SeasonBefore((Season Season, int Year) season)
		{
			return season.Season switch
			{
				Season.Winter => (Season.Fall, season.Year - 1),
				Season.Spring => (Season.Winter, season.Year),
				Season.Summer => (Season.Spring, season.Year),
				_ => (Season.Summer, season.Year)
			};
		}
	}
}
