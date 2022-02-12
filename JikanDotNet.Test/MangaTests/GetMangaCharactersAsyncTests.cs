using FluentAssertions;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
	public class GetMangaCharactersAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMangaCharactersAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaCharactersAsync_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaCharactersAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaCharactersAsync_MonsterId_ShouldParseMonsterCharacters()
		{
			// When
			var monster = await _jikan.GetMangaCharactersAsync(1);

			// Then
			monster.Data.Should().HaveCount(34);
		}

		[Fact]
		public async Task GetMangaCharactersAsync_MonsterId_ShouldParseMonsterCharactersJohan()
		{
			// When
			var monster = await _jikan.GetMangaCharactersAsync(1);

			// Then
			monster.Data.Select(x => x.Character.Name).Should().Contain("Liebert, Johan");
		}
	}
}