using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
	public class GetUserClubsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetUserClubsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserClubsAsync_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserClubsAsync(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserClubsAsync_Progeusz_ShouldParseProgeuszClubs()
		{
			// When
			var Clubs = await _jikan.GetUserClubsAsync("Progeusz");

			// Then
			using (new AssertionScope())
			{
				Clubs.Data.Should().HaveCount(19);
				Clubs.Data.Should().Contain(x => x.Name.Equals("Hatsune Miku - the Goddess ~FanClub~"));
				Clubs.Data.Should().Contain(x => x.MalId.Equals(65525)); // Manga Sales Rankings
			}
		}

		[Fact]
		public async Task GetUserClubsAsync_Ervelan_ShouldParseErvelanClubs()
		{
			// When
			var Clubs = await _jikan.GetUserClubsAsync("Ervelan");

			// Then
			using (new AssertionScope())
			{
				Clubs.Data.Should().HaveCount(15);
				Clubs.Data.Should().Contain(x => x.Name.Equals("JoJo's Bizarre Adventure Club"));
				Clubs.Data.Should().Contain(x => x.MalId.Equals(14689)); // Sakuya fanclub
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserClubsAsync_InvalidUsernameSecondPage_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserClubsAsync(username, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetUserClubsAsync_ValidUsernameInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserClubsAsync("Ervelan", page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		// This one does not work, most likely on jikan side
		[Fact]
		public async Task GetUserClubsAsync_ArchaeonSecondPage_ShouldParseArchaeonClubs()
		{
			// When
			var Clubs = await _jikan.GetUserClubsAsync("Archaeon", 2);

			// Then
			Clubs.Data.Should().BeEmpty();
		}
	}
}