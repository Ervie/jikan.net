using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class CharacterTests
	{
		private readonly IJikan _jikan;

		public CharacterTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public async Task GetCharacter_CorrectId_ShouldReturnNotNullCharacter(long malId)
		{
			// When
			Character returnedCharacter = await _jikan.GetCharacter(malId);

			// Then
			returnedCharacter.Should().NotBeNull();
		}

		[Theory]
		[InlineData(8)]
		[InlineData(9)]
		[InlineData(10)]
		public void GetCharacter_WrongId_ShouldReturnNullCharacter(long malId)
		{
			// When & Then
			Assert.ThrowsAnyAsync<JikanRequestException>(() => _jikan.GetCharacter(malId));
		}

		[Fact]
		public async Task GetCharacter_IchigoKurosakiId_ShouldParseIchigoKurosaki()
		{
			// When
			Character ichigo = await _jikan.GetCharacter(5);

			// Then
			ichigo.Name.Should().Be("Ichigo Kurosaki");
		}

		[Fact]
		public async Task GetCharacter_IchigoKurosakiId_ShouldParseIchigoKurosakiAboutNotNull()
		{
			// When
			Character ichigo = await _jikan.GetCharacter(5);

			// Then
			ichigo.About.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task GetCharacter_IchigoKurosakiId_ShouldParseIchigoKurosakiBleach()
		{
			// When
			Character ichigo = await _jikan.GetCharacter(5);

			// Then
			using (new AssertionScope())
			{
				ichigo.Animeography.Select(x => x.Name).Should().Contain("Bleach");
				ichigo.Mangaography.Select(x => x.Name).Should().Contain("Bleach");
			}
		}

		[Fact]
		public async Task GetCharacter_EinId_ShouldParseEin()
		{
			// When
			Character ein = await _jikan.GetCharacter(4);

			// Then
			using (new AssertionScope())
			{
				ein.Name.Should().Be("Ein");
				ein.Animeography.First().Role.Should().Be("Supporting");
				ein.Mangaography.First().Role.Should().Be("Supporting");
				ein.Animeography.Last().Role.Should().Be("Main");
			}
		}
	}
}