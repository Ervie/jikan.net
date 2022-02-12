using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
	public class GetUserFavoritesAsyncTests
	{
		private readonly IJikan _jikan;

		public GetUserFavoritesAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserFavoritesAsync_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserFavoritesAsync(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserFavoritesAsync_Ervelan_ShouldParseErvelanFavorites()
		{
			// When
			var user = await _jikan.GetUserFavoritesAsync("Ervelan");

			// Then
			using (new AssertionScope())
			{
				user.Data.Anime.Select(x => x.Title).Should().Contain("Haibane Renmei");
				user.Data.Manga.Select(x => x.Title).Should().Contain("Berserk");
				user.Data.Characters.Select(x => x.Name).Should().Contain("Oshino, Shinobu");
				user.Data.People.Select(x => x.Name).Should().Contain("Araki, Hirohiko");
			}
		}

		[Fact]
		public async Task GetUserFavoritesAsync_Ervelan_ShouldParseErvelanFavoritesExtraData()
		{
			// When
			var user = await _jikan.GetUserFavoritesAsync("Ervelan");

			// Then
			using (new AssertionScope())
			{
				user.Data.Anime.Should().Contain(x => x.Title.Equals("Haibane Renmei") && x.StartYear == 2002);
				user.Data.Manga.Should().Contain(x => x.Title.Equals("Berserk") && x.StartYear == 1989);
			}
		}
		[Fact]
		public async Task GetUserFavoritesAsync_Nekomata1037_ShouldParseNekomataFavorites()
		{
			// When
			var user = await _jikan.GetUserFavoritesAsync("Nekomata1037");

			// Then
			using (new AssertionScope())
			{
				user.Data.Anime.Select(x => x.Title).Should().Contain("Steins;Gate");
				user.Data.Manga.Select(x => x.Title).Should().Contain("Vinland Saga");
				user.Data.Characters.Select(x => x.Name).Should().Contain("Makise, Kurisu");
				user.Data.People.Select(x => x.Name).Should().Contain("Sayuri");
			}
		}
	}
}