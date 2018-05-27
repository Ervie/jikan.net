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

			Assert.Contains("Boogiepop wa Warawanai: Boogiepop Phantom", winter2000.SeasonEntries.Select(x => x.Title));
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

			SeasonEntry youjoSenki = winter2017.SeasonEntries.FirstOrDefault(x => x.Title.Equals("Youjo Senki"));

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
	}
}