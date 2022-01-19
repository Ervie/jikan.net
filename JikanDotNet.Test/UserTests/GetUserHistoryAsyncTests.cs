using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
	public class GetUserHistoryAsyncTests
	{
		private readonly IJikan _jikan;

		public GetUserHistoryAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserHistory_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserHistoryAsync(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserHistory_Nekomata_ShouldParseNekomataHistory()
		{
			// When
			var userHistory = await _jikan.GetUserHistoryAsync("Nekomata1037");

			// Then
			using (new AssertionScope())
			{
				userHistory.Should().NotBeNull();
				userHistory.Data.Count.Should().BeGreaterOrEqualTo(0);
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserHistory_InvalidUsernameWithExtension_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserHistoryAsync(username, UserHistoryExtension.Manga));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData((UserHistoryExtension)int.MaxValue)]
		[InlineData((UserHistoryExtension)int.MinValue)]
		public async Task GetUserHistory_ErvelanWithInvalidExtension_ShouldThrowValidationException(UserHistoryExtension userHistoryExtension)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserHistoryAsync("Ervelan", userHistoryExtension));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(UserHistoryExtension.Anime)]
		[InlineData(UserHistoryExtension.Manga)]
		public async Task GetUserHistory_ErvelanHistoryWithFilter_ShouldParseErvelanMangaHistory(UserHistoryExtension userHistoryExtension)
		{
			// When
			var userHistory = await _jikan.GetUserHistoryAsync("Ervelan", userHistoryExtension);

			// Then
			using (new AssertionScope())
			{
				userHistory.Should().NotBeNull();
				userHistory.Data.Count.Should().BeGreaterOrEqualTo(0);
			}
		}
	}
}