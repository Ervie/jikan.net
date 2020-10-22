using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class PersonSearchTests
	{
		private readonly IJikan _jikan;

		public PersonSearchTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData("hirohiko")]
		[InlineData("oda")]
		[InlineData("sawashiro")]
		public async Task SearchPerson_NonEmptyQuery_ShouldReturnNotNullSearchPerson(string query)
		{
			// When
			var returnedPerson = await _jikan.SearchPerson(query);

			// Then
			returnedPerson.Should().NotBeNull();
		}

		[Fact]
		public async Task SearchPerson_MaayaQuery_ShouldReturnSakamoto()
		{
			// When
			var returnedPerson = await _jikan.SearchPerson("maaya");

			// Then
			returnedPerson.Results.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task SearchPerson_MaayaQuery_ShouldReturnMaayaName()
		{
			// When
			var returnedPerson = await _jikan.SearchPerson("maaya");

			// Then
			returnedPerson.Results.First().Name.Should().Be("Maaya");
		}

		[Fact]
		public async Task SearchPerson_MaayaQuery_ShouldReturnMaayaMalId()
		{
			// When
			var returnedPerson = await _jikan.SearchPerson("maaya");

			// Then
			returnedPerson.Results.First().MalId.Should().Be(39860);
		}

		[Fact]
		public async Task SearchPerson_DaisukeQuerySecondPage_ShouldReturnDaisuke()
		{
			// When
			var returnedPerson = await _jikan.SearchPerson("daisuke", 2);

			// Then
			returnedPerson.Results.Select(x => x.Name).Should().Contain("Ishikawa, Daisuke");
		}
	}
}