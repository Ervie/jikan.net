using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class ScheduleTestClass
	{
		private readonly IJikan _jikan;

		public ScheduleTestClass()
		{
			_jikan = new Jikan(true);
		}

		[Fact]
		public async Task GetSchedule_AllSchedule_ShouldParseCurrentSchedule()
		{
			Schedule currentSeason = await _jikan.GetSchedule();

			Assert.NotNull(currentSeason);
		}

		[Fact]
		public async Task GetSchedule_Monday_ShouldParseMondaySchedule()
		{
			Schedule currentSeason = await _jikan.GetSchedule(ScheduledDay.Monday);

			Assert.Contains("The God of High School", currentSeason.Monday.Select(x => x.Title));
			Assert.Contains("Kingdom 3rd Season", currentSeason.Monday.Select(x => x.Title));
		}

		[Fact]
		public async Task GetSchedule_Friday_ShouldParseFridaySchedule()
		{
			Schedule currentSeason = await _jikan.GetSchedule(ScheduledDay.Friday);

			Assert.Contains("Zoids Wild Zero", currentSeason.Friday.Select(x => x.Title));
			Assert.Contains("Crayon Shin-chan", currentSeason.Friday.Select(x => x.Title));
		}
		[Fact]
		public async Task GetSchedule_UnknownSchedule_ShouldParseUnknownSchedule()
		{
			Schedule currentSeason = await _jikan.GetSchedule(ScheduledDay.Unknown);

			Assert.Contains("Yodel no Onna", currentSeason.Unknown.Select(x => x.Title));
			Assert.Contains("Jinxiu Shenzhou Zhi Qi You Ji", currentSeason.Unknown.Select(x => x.Title));
		}

	}
}