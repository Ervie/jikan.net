using FluentAssertions;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
	public class GetMangaPicturesAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMangaPicturesAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaPicturesAsync_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaPicturesAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaPicturesAsync_MonsterId_ShouldParseMonsterImages()
		{
			// When
			var monster = await _jikan.GetMangaPicturesAsync(1);

			// Then
			monster.Data.Should().HaveCount(8);
		}
	}
}