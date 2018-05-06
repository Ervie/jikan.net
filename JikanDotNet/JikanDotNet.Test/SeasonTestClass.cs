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
		public void ShouldParseSpring1980()
		{
			Season spring1970 = Task.Run(() => jikan.GetSeason(1970, Seasons.Spring)).Result;

			Assert.Equal(17, spring1970.SeasonEntries.Count);
		}

		[Fact]
		public void ShouldParseCurrentSeason()
		{
			Season currentSesaon = Task.Run(() => jikan.GetSeason()).Result;

			Assert.NotNull(currentSesaon);
			Assert.InRange(currentSesaon.SeasonEntries.Count, 20, 500);
		}
	}
}