using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class PersonExtendedTestClass
	{
		private readonly IJikan jikan;

		public PersonExtendedTestClass()
		{
			jikan = new Jikan();
		}

		[Fact]
		public async Task GetPersonPictures_WakamotoId_ShouldParseNorioWakamotoImages()
		{
			PersonPictures norioWakamoto = await jikan.GetPersonPictures(84);

			Assert.Equal(4, norioWakamoto.Pictures.Count);
		}
	}
}