using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.PersonTests
{
	public class GetPersonMangaAsyncTests
	{
		private readonly IJikan _jikan;

		public GetPersonMangaAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetPersonMangaAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetPersonMangaAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetPersonMangaAsync_YuasaId_ShouldParseMasaakiYuasaAnime()
		{
			// Given
			var yuasa = await _jikan.GetPersonMangaAsync(5068);

			// Then
			yuasa.Data.Should().BeEmpty();
		}

		[Fact]
		public async Task GetPersonMangaAsync_EiichiroOdaId_ShouldParseEiichiroOda()
		{
			// Given
			var oda = await _jikan.GetPersonMangaAsync(1881);

			// Then
			using (new AssertionScope())
			{
				oda.Data.Should().HaveCount(15);
				oda.Data.Should().Contain(x => x.Manga.Title.Equals("One Piece") && x.Position.Equals("Story & Art"));
				oda.Data.Should().Contain(x => x.Manga.Title.Equals("Cross Epoch") && x.Position.Equals("Story & Art"));
				oda.Data.Should().Contain(x => x.Manga.Title.Equals("One Piece Novel: Mugiwara Stories") && x.Position.Equals("Art"));
			}
		}
	}
}
