using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class CharacterExtendedTestClass
	{
		private readonly IJikan jikan;

		public CharacterExtendedTestClass()
		{
			jikan = new Jikan();
		}

		[Fact]
		public async Task GetCharacterPictures_SharoId_ShouldParseKirimaSharoImages()
		{
			CharacterPictures kirimaSharo = await jikan.GetCharacterPictures(94947);

			Assert.Equal(8, kirimaSharo.Pictures.Count);
		}
	}
}