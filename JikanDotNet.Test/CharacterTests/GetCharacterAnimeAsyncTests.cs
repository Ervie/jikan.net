using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.CharacterTests
{
	public class GetCharacterAnimeAsyncTests
	{
		private readonly IJikan _jikan;

		public GetCharacterAnimeAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetCharacterAnimeAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetCharacterAnimeAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetCharacterAnimeAsync_SpikeSpiegelId_ShouldParseSpikeSpiegelAnime()
		{
			// When
			var spike = await _jikan.GetCharacterAnimeAsync(1);

			// Then
			using (new AssertionScope())
			{
				spike.Data.Should().HaveCount(3);
				spike.Data.Should().OnlyContain(x => x.Role.Equals("Main"));
				spike.Data.Should().OnlyContain(
					x => !string.IsNullOrWhiteSpace(x.Anime.Images.JPG.ImageUrl)
					&& !string.IsNullOrWhiteSpace(x.Anime.Images.JPG.SmallImageUrl)
					&& !string.IsNullOrWhiteSpace(x.Anime.Images.JPG.LargeImageUrl)
				);
			}
		}

		[Fact]
		public async Task GetCharacterAnimeAsync_IchigoKurosakiId_ShouldParseIchigoKurosakiAnime()
		{
			// When
			var ichigo = await _jikan.GetCharacterAnimeAsync(5);

			// Then
			using (new AssertionScope())
			{
				ichigo.Data.Should().HaveCount(9);
				ichigo.Data.Select(x => x.Anime.Title).Should().Contain("Bleach");
			}
		}
	}
}