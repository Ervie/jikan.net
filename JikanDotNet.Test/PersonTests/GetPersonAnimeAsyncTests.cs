using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.PersonTests
{
	public class GetPersonAnimeAsyncTests
	{
		private readonly IJikan _jikan;

		public GetPersonAnimeAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetPersonAnimeAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetPersonAnimeAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetPersonAnimeAsync_YuasaId_ShouldParseMasaakiYuasaAnime()
		{
			// Given
			var yuasa = await _jikan.GetPersonAnimeAsync(5068);

			// Then
			using (new AssertionScope())
			{
				yuasa.Data.Should().HaveCountGreaterThan(70);
				yuasa.Data.Should().Contain(x => x.Anime.Title.Equals("Ping Pong the Animation"));
				yuasa.Data.Should().Contain(x => x.Anime.Title.Equals("Yojouhan Shinwa Taikei"));
			}
		}

		[Fact]
		public async Task GetPersonAnimeAsync_SekiTomokazuId_ShouldParseSekiTomokazuAnime()
		{
			// Given
			var seki = await _jikan.GetPersonAnimeAsync(1);

			// Then
			using (new AssertionScope())
			{
				seki.Data.Should().HaveCountLessThan(20);
				seki.Data.Should().Contain(x => x.Anime.Title.Equals("Anime Tenchou") && x.Position.Equals("add Theme Song Performance"));
			}
		}
	}
}
