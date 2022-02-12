using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
	public class GetUserRecommendationsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetUserRecommendationsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserRecommendationsAsync_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserRecommendationsAsync(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserRecommendationsAsync_Ervelan_ShouldParseErvelanRecommendations()
		{
			// When
			var Recommendations = await _jikan.GetUserRecommendationsAsync("Ervelan");

			// Then
			using (new AssertionScope())
			{
				Recommendations.Should().NotBeNull();
				Recommendations.Data.Should().BeEmpty();
			}
		}

		[Fact]
		public async Task GetUserRecommendationsAsync_Progeusz_ShouldParseProgeuszRecommendations()
		{
			// When
			var Recommendations = await _jikan.GetUserRecommendationsAsync("Progeusz");

			// Then
			using (new AssertionScope())
			{
				Recommendations.Data.Should().ContainSingle().Which.Content.StartsWith("Both anime are survival death games.");
				Recommendations.Pagination.HasNextPage.Should().BeFalse();
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserRecommendationsAsync_InvalidUsernameSecondPage_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserRecommendationsAsync(username, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetUserRecommendationsAsync_ValidUsernameInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserRecommendationsAsync("Ervelan", page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserRecommendationsAsync_IchiyonyuuzlotySecondPage_ShouldParseIchiyonjuuzlotyRecommendations()
		{
			// When
			var Recommendations = await _jikan.GetUserRecommendationsAsync("Ichiyonjuuzloty", 2);

			// Then
			Recommendations.Data.Should().BeEmpty();
		}
	}
}
