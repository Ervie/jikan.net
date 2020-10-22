using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class CharacterExtendedTests
	{
		private readonly IJikan _jikan;

		public CharacterExtendedTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetCharacterPictures_SharoId_ShouldParseKirimaSharoImages()
		{
			// When
			CharacterPictures kirimaSharo = await _jikan.GetCharacterPictures(94947);

			// Then
			kirimaSharo.Pictures.Should().HaveCount(8);
		}
	}
}