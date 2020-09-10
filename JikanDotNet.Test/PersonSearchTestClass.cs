using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class PersonSearchTestClass
	{
		private readonly IJikan _jikan;

		public PersonSearchTestClass()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData("hirohiko")]
		[InlineData("oda")]
		[InlineData("sawashiro")]
		public async Task SearchPerson_NonEmptyQuery_ShouldReturnNotNullSearchPerson(string query)
		{
			PersonSearchResult returnedPerson = await _jikan.SearchPerson(query);

			Assert.NotNull(returnedPerson);
		}

		[Fact]
		public async Task SearchPerson_MaayaQuery_ShouldReturnSakamoto()
		{
			PersonSearchResult returnedPerson = await _jikan.SearchPerson("maaya");

			Assert.NotEmpty(returnedPerson.Results);
		}

		[Fact]
		public async Task SearchPerson_MaayaQuery_ShouldReturnMaayaName()
		{
			PersonSearchResult returnedPerson = await _jikan.SearchPerson("maaya");

			Assert.Equal("Maaya", returnedPerson.Results.First().Name);
		}

		[Fact]
		public async Task SearchPerson_MaayaQuery_ShouldReturnMaayaMalId()
		{
			PersonSearchResult returnedPerson = await _jikan.SearchPerson("maaya");

			Assert.Equal(39860, returnedPerson.Results.First().MalId);
		}

		[Fact]
		public async Task SearchPerson_DaisukeQuerySecondPage_ShouldReturnDaisuke()
		{
			PersonSearchResult returnedPerson = await _jikan.SearchPerson("daisuke", 2);

			Assert.Contains("Ishikawa, Daisuke", returnedPerson.Results.Select(x => x.Name));
		}
	}
}