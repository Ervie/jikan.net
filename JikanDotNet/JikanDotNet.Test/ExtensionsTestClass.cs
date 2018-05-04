using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class ExtensionsTestClass
	{
		private readonly IJikan jikan;

		public ExtensionsTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public void ShouldParseNorioWakamotoImages()
		{
			Person norioWakamoto = Task.Run(() => jikan.GetPerson(84, PersonExtension.Pictures)).Result;

			Assert.Equal(4, norioWakamoto.Images.Count);
		}

		[Fact]
		public void ShouldParseKirimaSharoImages()
		{
			Character kirimaSharo = Task.Run(() => jikan.GetCharacter(94947, CharacterExtension.Pictures)).Result;

			Assert.Equal(8, kirimaSharo.Images.Count);
		}
	}
}