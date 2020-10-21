using FluentAssertions;
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
			// Given
			var norioWakamoto = await _jikan.GetPersonPictures(84);

			// Then
			norioWakamoto.Pictures.Should().HaveCount(4);
		}
	}
}