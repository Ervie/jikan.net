using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class CharacterExtendedTestClass
	{
		private readonly IJikan _jikan;

		public CharacterExtendedTestClass()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetCharacterPictures_SharoId_ShouldParseKirimaSharoImages()
		{
			CharacterPictures kirimaSharo = await _jikan.GetCharacterPictures(94947);

			Assert.Equal(8, kirimaSharo.Pictures.Count);
		}
	}
}