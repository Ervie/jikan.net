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
		public void ShouldParseCurrentSchedule()
		{
			Schedule currentSeason = Task.Run(() => jikan.GetSchedule()).Result;

			Assert.NotNull(currentSeason);
		}

		[Fact]
		public void ShouldParseMondaySchedule()
		{
			Schedule currentSeason = Task.Run(() => jikan.GetSchedule()).Result;

			Assert.Contains("Shokugeki no Souma: San no Sara - Toutsuki Ressha-hen", currentSeason.Monday.Select(x => x.Title));
			Assert.Contains("Golden Kamuy", currentSeason.Monday.Select(x => x.Title));
		}

		[Fact]
		public void ShouldParseFridaySchedule()
		{
			Schedule currentSeason = Task.Run(() => jikan.GetSchedule()).Result;

			Assert.Contains("Megalo Box", currentSeason.Friday.Select(x => x.Title));
			Assert.Contains("Hinamatsuri", currentSeason.Friday.Select(x => x.Title));
		}
	}
}