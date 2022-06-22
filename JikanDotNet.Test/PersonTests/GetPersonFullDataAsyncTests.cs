using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.PersonTests
{
	public class GetPersonFullDataAsyncTests
	{
		private readonly IJikan _jikan;

		public GetPersonFullDataAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetPersonFullDataAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetPersonFullDataAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}
		
		[Fact]
		public async Task GetPersonFullDataAsync_YuasaId_ShouldParseMasaakiYuasa()
		{
			// Given
			var yuasa = await _jikan.GetPersonFullDataAsync(5068);

			// Then
			using var _ = new AssertionScope();
			yuasa.Data.Mangaography.Should().BeEmpty();
			yuasa.Data.Animeography.Should().HaveCountGreaterThan(70);
			yuasa.Data.Animeography.Should().Contain(x => x.Anime.Title.Equals("Ping Pong the Animation"));
			yuasa.Data.Animeography.Should().Contain(x => x.Anime.Title.Equals("Yojouhan Shinwa Taikei"));
			yuasa.Data.VoiceActingRoles.Should().BeEmpty();
		}
		
		[Fact]
		public async Task GetPersonFullDataAsync_OdaId_ShouldParseEiichiroOda()
		{
			// Given
			var oda = await _jikan.GetPersonFullDataAsync(1881);

			// Then
			using var _ = new AssertionScope();
			oda.Data.Mangaography.Should().HaveCount(15);
			oda.Data.Mangaography.Should().Contain(x => x.Manga.Title.Equals("One Piece") && x.Position.Equals("Story & Art"));
			oda.Data.Mangaography.Should().Contain(x => x.Manga.Title.Equals("Cross Epoch") && x.Position.Equals("Story & Art"));
			oda.Data.Mangaography.Should().Contain(x => x.Manga.Title.Equals("One Piece Novel: Mugiwara Stories") && x.Position.Equals("Art"));
			oda.Data.Animeography.Should().HaveCountGreaterThan(30);
			oda.Data.Animeography.Should().Contain(x => x.Anime.Title.Equals("One Piece") && x.Position.Equals("add Original Creator Airing"));
			oda.Data.VoiceActingRoles.Should().ContainSingle();
		}
	}
}