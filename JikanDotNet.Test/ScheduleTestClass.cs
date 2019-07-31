using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class ScheduleTestClass
	{
		private readonly IJikan jikan;

		public ScheduleTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public async Task GetSchedule_AllSchedule_ShouldParseCurrentSchedule()
		{
			Schedule currentSeason = await jikan.GetSchedule();

			Assert.NotNull(currentSeason);
		}

		[Fact]
		public async Task GetSchedule_Monday_ShouldParseMondaySchedule()
		{
			Schedule currentSeason = await jikan.GetSchedule(ScheduledDay.Monday);

			Assert.Contains("Otoppe", currentSeason.Monday.Select(x => x.Title));
			Assert.Contains("Yasamura Yasashi no Yasashii Sekai", currentSeason.Monday.Select(x => x.Title));
		}

		[Fact]
		public async Task GetSchedule_Friday_ShouldParseFridaySchedule()
		{
			Schedule currentSeason = await jikan.GetSchedule(ScheduledDay.Friday);

			Assert.Contains("Doraemon (2005)", currentSeason.Friday.Select(x => x.Title));
			Assert.Contains("Crayon Shin-chan", currentSeason.Friday.Select(x => x.Title));
		}
		[Fact]
		public async Task GetSchedule_UnknownSchedule_ShouldParseUnknownSchedule()
		{
			Schedule currentSeason = await jikan.GetSchedule(ScheduledDay.Unknown);

			Assert.Contains("Yodel no Onna", currentSeason.Unknown.Select(x => x.Title));
			Assert.Contains("Jinxiu Shenzhou Zhi Qi You Ji", currentSeason.Unknown.Select(x => x.Title));
		}

	}
}