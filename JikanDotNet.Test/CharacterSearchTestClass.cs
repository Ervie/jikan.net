using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class CharacterSearchTestClass
	{
		private readonly IJikan jikan;

		public CharacterSearchTestClass()
		{
			jikan = new Jikan();
		}

		[Theory]
		[InlineData("edward")]
		[InlineData("mai")]
		[InlineData("takeshi")]
		public async Task SearchCharacter_NonEmptyQuery_ShouldReturnNotNullSearchCharacter(string query)
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter(query);

			Assert.NotNull(returnedCharacter);
		}

		[Fact]
		public async Task SearchCharacter_LupinQuery_ShouldReturnLupin()
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter("lupin");

			Assert.Equal(2, returnedCharacter.ResultLastPage);
		}

		[Fact]
		public async Task SearchCharacter_LupinQuery_ShouldReturnLupinName()
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter("lupin iii");

			Assert.Equal("Lupin III, Arsene", returnedCharacter.Results.First().Name);
		}

		[Fact]
		public async Task SearchCharacter_LupinQuery_ShouldReturnLupinMalId()
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter("lupin iii");

			Assert.Equal(1044, returnedCharacter.Results.First().MalId);
		}

		[Fact]
		public async Task SearchCharacter_LambdadeltaQuery_ShouldReturnLambdadetla()
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter("lambdadelta");

			Assert.Equal(3, returnedCharacter.Results.Count());
			Assert.Equal("Lambdadelta", returnedCharacter.Results.First().Name);
			Assert.Equal("Umineko no Naku Koro ni", returnedCharacter.Results.First().Animeography.First().Name);
			Assert.Single(returnedCharacter.Results.First().Animeography);
		}

		[Fact]
		public async Task SearchCharacter_KirumiQuery_ShouldReturnKirumi()
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter("kirumi");

			Assert.Single(returnedCharacter.Results);
			Assert.Equal("Toujou, Kirumi", returnedCharacter.Results.First().Name);
			Assert.Single(returnedCharacter.Results.First().Mangaography);
		}

		[Fact]
		public async Task SearchCharacter_EdwardQuerySecondPage_ShouldFindEdwards()
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter("edward", 2);

			Assert.Contains("Edward", returnedCharacter.Results.Select(x => x.Name));
			Assert.Equal(50, returnedCharacter.Results.Count);
		}
	}
}