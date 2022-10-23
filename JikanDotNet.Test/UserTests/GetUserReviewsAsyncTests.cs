using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
	public class GetUserReviewsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetUserReviewsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserReviewsAsync_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserReviewsAsync(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserReviewsAsync_Ervelan_ShouldParseErvelanReviews()
		{
			// When
			var reviews = await _jikan.GetUserReviewsAsync("Ervelan");

			// Then
			using (new AssertionScope())
			{
				reviews.Should().NotBeNull();
				reviews.Data.Should().BeEmpty();
			}
		}

		[Fact]
		public async Task GetUserReviewsAsync_Ichiyonyuuzloty_ShouldParseIchiyonjuuzlotyReviews()
		{
			// When
			var reviews = await _jikan.GetUserReviewsAsync("Ichiyonjuuzloty");

			// Then
			using (new AssertionScope())
			{
				reviews.Data.Should().HaveCount(2);
				reviews.Data.Should().Contain(x => x.Url.Equals("https://myanimelist.net/reviews.php?id=200623"));
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserReviewsAsync_InvalidUsernameSecondPage_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserReviewsAsync(username, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetUserReviewsAsync_ValidUsernameInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserReviewsAsync("Ervelan", page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserReviewsAsync_IchiyonyuuzlotySecondPage_ShouldParseIchiyonjuuzlotyReviews()
		{
			// When
			var reviews = await _jikan.GetUserReviewsAsync("Ichiyonjuuzloty", 2);

			// Then
			reviews.Data.Should().BeEmpty();
		}

		[Fact]
		public async Task GetUserReviewsAsync_ArchaeonSecondPage_ShouldParseArchaeonReviews()
		{
			// When
			var reviews = await _jikan.GetUserReviewsAsync("Archaeon", 2);

			// Then
			using var _ = new AssertionScope();
			reviews.Data.Should().NotBeEmpty().And.HaveCount(10);
			reviews.Data.Should().OnlyContain(x => x.Type.Equals("anime") && x.EpisodesWatched != null && x.ReviewScores.Animation != null);
		}
	}
}