using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class PersonExtendedTestClass
	{
		private readonly IJikan _jikan;

		public PersonExtendedTestClass()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetPersonPictures_WakamotoId_ShouldParseNorioWakamotoImages()
		{
			PersonPictures norioWakamoto = await _jikan.GetPersonPictures(84);

			Assert.Equal(4, norioWakamoto.Pictures.Count);
		}
	}
}