using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class SeasonTestClass
	{
		private readonly IJikan _jikan;

		public SeasonTestClass()
		{
			_jikan = new Jikan(true);
		}

		[Fact]
		public async Task GetSeason_Winter2000_ShouldParseWinter2000()
		{
			Season winter2000 = await _jikan.GetSeason(2000, Seasons.Winter);

			Assert.Contains("Boogiepop wa Warawanai", winter2000.SeasonEntries.Select(x => x.Title));
			Assert.Contains("Ojamajo Doremi Sharp", winter2000.SeasonEntries.Select(x => x.Title));
		}

		[Fact]
		public async Task GetSeason_Spring1970_ShouldParseSpring1970()
		{
			Season spring1970 = await _jikan.GetSeason(1970, Seasons.Spring);

			Assert.Equal(17, spring1970.SeasonEntries.Count);
		}

		[Fact]
		public async Task GetSeason_Winter2017_ShouldParseYoujoSenki()
		{
			Season winter2017 = await _jikan.GetSeason(2017, Seasons.Winter);

			AnimeSubEntry youjoSenki = winter2017.SeasonEntries.FirstOrDefault(x => x.Title.Equals("Youjo Senki"));

			Assert.Equal("TV", youjoSenki.Type);
			Assert.False(youjoSenki.R18);
			Assert.False(youjoSenki.Kids);
			Assert.False(youjoSenki.Continued);
		}

		[Fact]
		public async Task GetSeason_NoParameter_ShouldParseCurrentSeason()
		{
			Season currentSesaon = await _jikan.GetSeason();

			Assert.NotNull(currentSesaon);
			Assert.InRange(currentSesaon.SeasonEntries.Count, 20, 500);
		}

		[Fact]
		public async Task GetSeason_Spring1970_ShouldParseSpring1970ExtraInfo()
		{
			Season spring1970 = await _jikan.GetSeason(1970, Seasons.Spring);

			Assert.Equal("Spring", spring1970.SeasonName);
			Assert.Equal(1970, spring1970.SeasonYear);
		}

		[Fact]
		public async Task GetSeasonArchive_NoParameter_ShouldParseFirstQueryableYear()
		{
			SeasonArchives seasonArchives = await _jikan.GetSeasonArchive();

			Assert.Equal(1917, seasonArchives.Archives.Last().Year);
			Assert.Equal(4,  seasonArchives.Archives.Last().Season.Count);
		}

		[Fact]
		public async Task GetSeasonArchive_NoParameter_ShouldParseLatestQueryableYear()
		{
			SeasonArchives seasonArchives = await _jikan.GetSeasonArchive();

			Assert.True(seasonArchives.Archives.First().Year > 2018);
			Assert.InRange(seasonArchives.Archives.Last().Season.Count, 1, 4);
		}

		[Fact]
		public async Task GetSeasonLater_NoParameter_ShouldParseLaterSeason()
		{
			Season season = await _jikan.GetSeasonLater();

			Assert.Null(season.SeasonYear);
			Assert.Equal("Later", season.SeasonName);
			Assert.Null(season.SeasonEntries.First().AiringStart);
		}
	}
}