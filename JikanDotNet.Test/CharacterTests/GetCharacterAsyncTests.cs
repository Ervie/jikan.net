using FluentAssertions;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.CharacterTests
{
	public class GetCharacterAsyncTests
	{
		private readonly IJikan _jikan;

		public GetCharacterAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetCharacterAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetCharacterAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public async Task GetCharacterAsync_CorrectId_ShouldReturnNotNullCharacter(long malId)
		{
			// When
			var returnedCharacter = await _jikan.GetCharacterAsync(malId);

			// Then
			returnedCharacter.Should().NotBeNull();
		}

		[Theory]
		[InlineData(8)]
		[InlineData(9)]
		[InlineData(10)]
		public void GetCharacterAsync_WrongId_ShouldReturnNullCharacter(long malId)
		{
			// When & Then
			Assert.ThrowsAnyAsync<JikanRequestException>(() => _jikan.GetCharacterAsync(malId));
		}

		[Fact]
		public async Task GetCharacterAsync_IchigoKurosakiId_ShouldParseIchigoKurosaki()
		{
			// When
			var ichigo = await _jikan.GetCharacterAsync(5);

			// Then
			ichigo.Data.Name.Should().Be("Ichigo Kurosaki");
		}

		[Fact]
		public async Task GetCharacterAsync_IchigoKurosakiId_ShouldParseIchigoKurosakiAboutNotNull()
		{
			// When
			var ichigo = await _jikan.GetCharacterAsync(5);

			// Then
			ichigo.Data.About.Should().NotBeNullOrEmpty();
		}

		[Fact]
		public async Task GetCharacterAsync_GetBlackId_ShouldParseJetBlackNicknames()
		{
			// When
			var jetBlack = await _jikan.GetCharacterAsync(3);

			// Then
			jetBlack.Data.Nicknames.Should().HaveCount(2);
		}
	}
}