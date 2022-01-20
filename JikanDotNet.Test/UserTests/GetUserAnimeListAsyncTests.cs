using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
	public class GetUserAnimeListAsyncTests
	{
		private readonly IJikan _jikan;

		public GetUserAnimeListAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserAnimeListAsync_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserAnimeListAsync(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserAnimeListAsync_Ervelan_ShouldParseErvelanAnimeList()
		{
			// When
			var animeList = await _jikan.GetUserAnimeListAsync("Ervelan");

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Data.Count.Should().Be(300);
				animeList.Data.Select(x => x.Anime.Title).Should().Contain("Akira");
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserAnimeListAsync_InvalidUsernameSecondPage_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserAnimeListAsync(username, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetUserAnimeListAsync_ValidUsernameInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserAnimeListAsync("Ervelan", page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserAnimeListAsync_ErvelanSecondPage_ShouldParseErvelanAnimeListSecondPage()
		{
			// When
			var animeList = await _jikan.GetUserAnimeListAsync("Ervelan", 2);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Data.Count.Should().Be(300);
			}
		}

		[Fact]
		public async Task GetUserAnimeListAsync_onrix_ShouldParseOnrixAnimeList()
		{
			// When
			var animeList = await _jikan.GetUserAnimeListAsync("onrix");

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Data.Count.Should().Be(122);
			}
		}
	}
}