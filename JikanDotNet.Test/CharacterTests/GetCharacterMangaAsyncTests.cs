using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.CharacterTests
{
	public class GetCharacterMangaAsyncTests
	{
		private readonly IJikan _jikan;

		public GetCharacterMangaAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetCharacterMangaAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetCharacterMangaAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetCharacterMangaAsync_SpikeSpiegelId_ShouldParseSpikeSpiegelAnime()
		{
			// When
			var spike = await _jikan.GetCharacterMangaAsync(1);

			// Then
			using (new AssertionScope())
			{
				spike.Data.Should().HaveCount(2);
				spike.Data.Should().OnlyContain(x => x.Role.Equals("Main"));
				spike.Data.Should().OnlyContain(
					x => !string.IsNullOrWhiteSpace(x.Manga.Images.JPG.ImageUrl)
					&& !string.IsNullOrWhiteSpace(x.Manga.Images.JPG.SmallImageUrl)
					&& !string.IsNullOrWhiteSpace(x.Manga.Images.JPG.LargeImageUrl)
				);
			}
		}
	}
}
