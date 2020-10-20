using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class CharacterSearchTestClass
	{
		private readonly IJikan _jikan;

		public CharacterSearchTestClass()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData("edward")]
		[InlineData("mai")]
		[InlineData("takeshi")]
		public async Task SearchCharacter_NonEmptyQuery_ShouldReturnNotNullSearchCharacter(string query)
		{
			// When
			CharacterSearchResult returnedCharacter = await _jikan.SearchCharacter(query);

			// Then
			returnedCharacter.Should().NotBeNull();
		}

		[Fact]
		public async Task SearchCharacter_LupinQuery_ShouldReturnLupin()
		{
			// When
			CharacterSearchResult returnedCharacter = await _jikan.SearchCharacter("lupin");

			// Then
			returnedCharacter.ResultLastPage.Should().Be(2);
		}

		[Fact]
		public async Task SearchCharacter_LupinQuery_ShouldReturnLupinName()
		{
			// When
			CharacterSearchResult returnedCharacter = await _jikan.SearchCharacter("lupin iii");

			// Then
			returnedCharacter.Results.First().Name.Should().Be("Lupin III, Arsene");
		}

		[Fact]
		public async Task SearchCharacter_LupinQuery_ShouldReturnLupinMalId()
		{
			// When
			CharacterSearchResult returnedCharacter = await _jikan.SearchCharacter("lupin iii");

			// Then
			returnedCharacter.Results.First().MalId.Should().Be(1044);
		}

		[Fact]
		public async Task SearchCharacter_LambdadeltaQuery_ShouldReturnLambdadetla()
		{
			// When
			CharacterSearchResult returnedCharacter = await _jikan.SearchCharacter("lambdadelta");

			// Then
			var firstCharacter = returnedCharacter.Results.First();
			using (new AssertionScope())
			{
				returnedCharacter.Results.Should().HaveCount(3);
				firstCharacter.Name.Should().Be("Lambdadelta");
				firstCharacter.Animeography.First().Name.Should().Be("Umineko no Naku Koro ni");
				firstCharacter.Animeography.Should().ContainSingle();
			}
		}

		[Fact]
		public async Task SearchCharacter_KirumiQuery_ShouldReturnKirumi()
		{
			// When
			CharacterSearchResult returnedCharacter = await _jikan.SearchCharacter("kirumi");

			// Then
			var firstCharacter = returnedCharacter.Results.First();
			using (new AssertionScope())
			{
				returnedCharacter.Results.Should().ContainSingle();
				firstCharacter.Name.Should().Be("Toujou, Kirumi");
				firstCharacter.Mangaography.Should().ContainSingle();
			}
		}

		[Fact]
		public async Task SearchCharacter_EdwardQuerySecondPage_ShouldFindEdwards()
		{
			// When
			CharacterSearchResult returnedCharacter = await _jikan.SearchCharacter("edward", 2);

			// Then
			returnedCharacter.Results.Select(x => x.Name).Should().Contain("Elric, Trisha");
		}
	}
}