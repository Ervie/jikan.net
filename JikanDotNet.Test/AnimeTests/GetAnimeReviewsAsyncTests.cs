using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeReviewsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeReviewsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeReviewsAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeReviewsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeReviewsAsync_BebopId_ShouldParseCowboyBebopReviews()
		{
			// When
			var bebop = await _jikan.GetAnimeReviewsAsync(1);

			// Then
			var firstReview = bebop.Data.First();
			using (new AssertionScope())
			{
				firstReview.User.Username.Should().Be("TheLlama");
				firstReview.MalId.Should().Be(7406);
				firstReview.Score.Should().BeGreaterThan(8);

				firstReview.Reactions.TotalReactions.Should().BeGreaterThan(2000);
				firstReview.Reactions.Confusing.Should().BeGreaterThan(0);
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeReviewsAsync_SecondPageWithInvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeReviewsAsync(malId, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeReviewsAsync_CorrectIdWrongPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeReviewsAsync(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeReviewsAsync_BebopIdSecondPage_ShouldParseCowboyBebopReviewsPaged()
		{
			// When
			var bebop = await _jikan.GetAnimeReviewsAsync(1, 2);

			// Then
			var firstReview = bebop.Data.First();
			using (new AssertionScope())
			{
				firstReview.EpisodesWatched.Should().Be(null);
				firstReview.Score.Should().BeGreaterOrEqualTo(8);
			}
		}
	}
}