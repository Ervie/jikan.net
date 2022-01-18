using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.ClubTests
{
	public class GetClubRelationsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetClubRelationsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetClubRelationsAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetClubRelationsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetClubRelationsAsync_BebopId_ShouldParseCowboyBebopClubRelatedEntities()
		{
			// When
			var club = await _jikan.GetClubRelationsAsync(1);

			// Then
			using (new AssertionScope())
			{
				club.Data.Anime.Should().NotBeEmpty().And.HaveCount(3).And.Contain(x => x.Name.Equals("Cowboy Bebop"));
				club.Data.Manga.Should().NotBeEmpty().And.HaveCount(2).And.Contain(x => x.Name.Equals("Shooting Star Bebop: Cowboy Bebop"));
				club.Data.Characters.Should().NotBeEmpty().And.HaveCount(22).And.Contain(x => x.Name.Equals("Black, Jet"));
			}
		}

		[Fact]
		public async Task GetClubRelationsAsync_NamineCafeId_ShouldParseNamineCafeClubRelatedEntities()
		{
			// When
			var club = await _jikan.GetClubRelationsAsync(39921);

			// Then
			using (new AssertionScope())
			{
				club.Data.Anime.Should().Contain(x => x.Name.Equals("Bakemonogatari"));
				club.Data.Anime.Should().Contain(x => x.Name.Equals("Clannad"));
				club.Data.Manga.Should().Contain(x => x.Name.Equals("Fate/Zero"));
				club.Data.Characters.Should().Contain(x => x.Name.Equals("Naegi, Makoto"));
			}
		}
	}
}