using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class SeasonTestClass
	{
		private readonly IJikan jikan;

		public SeasonTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public void ShouldParseWinter2000()
		{
			Season winter2000 = Task.Run(() => jikan.GetSeason(2000, Seasons.Winter)).Result;

			Assert.Contains("Boogiepop wa Warawanai", winter2000.SeasonEntries.Select(x => x.Title));
			Assert.Contains("Ojamajo Doremi Sharp", winter2000.SeasonEntries.Select(x => x.Title));
		}

		[Fact]
		public void ShouldParseSpring1970()
		{
			Season spring1970 = Task.Run(() => jikan.GetSeason(1970, Seasons.Spring)).Result;

			Assert.Equal(17, spring1970.SeasonEntries.Count);
		}

		[Fact]
		public void ShouldParseYoujoSenki()
		{
			Season winter2017 = Task.Run(() => jikan.GetSeason(2017, Seasons.Winter)).Result;

			AnimeSubEntry youjoSenki = winter2017.SeasonEntries.FirstOrDefault(x => x.Title.Equals("Youjo Senki"));

			Assert.Equal("TV", youjoSenki.Type);
			Assert.False(youjoSenki.R18);
			Assert.False(youjoSenki.Kids);
			Assert.False(youjoSenki.Continued);
		}

		[Fact]
		public void ShouldParseCurrentSeason()
		{
			Season currentSesaon = Task.Run(() => jikan.GetSeason()).Result;

			Assert.NotNull(currentSesaon);
			Assert.InRange(currentSesaon.SeasonEntries.Count, 20, 500);
		}

		[Fact]
		public void ShouldParseSpring1970ExtraInfo()
		{
			Season spring1970 = Task.Run(() => jikan.GetSeason(1970, Seasons.Spring)).Result;

			Assert.Equal("Spring", spring1970.SeasonName);
			Assert.Equal(1970, spring1970.SeasonYear);
		}

		[Fact]
		public void ShouldParseFirstQueryableYear()
		{
			SeasonArchives seasonArchives = Task.Run(() => jikan.GetSeasonArchive()).Result;

			Assert.Equal(1917, seasonArchives.Archives.Last().Year);
			Assert.Equal(4,  seasonArchives.Archives.Last().Season.Count);
		}

		[Fact]
		public void ShouldParseLatestQueryableYear()
		{
			SeasonArchives seasonArchives = Task.Run(() => jikan.GetSeasonArchive()).Result;

			Assert.True(seasonArchives.Archives.First().Year > 2018);
			Assert.InRange(seasonArchives.Archives.Last().Season.Count, 1, 4);
		}

		[Fact]
		public void ShouldParseLaterSeason()
		{
			Season season = Task.Run(() => jikan.GetSeasonLater()).Result;

			Assert.Null(season.SeasonYear);
			Assert.Equal("Later", season.SeasonName);
			Assert.Null(season.SeasonEntries.First().AiringStart);
		}
	}
}