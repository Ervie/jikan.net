using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.AnimeTests
{
	[Collection("TenraiTests")]
	public class GetAnimeReviewsAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetAnimeReviewsAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeReviewsAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetAnimeReviewsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Fact]
		public async Task GetAnimeReviewsAsync_BebopId_ShouldParseCowboyBebopReviews()
		{
			// When
			var bebop = await _tenrai.GetAnimeReviewsAsync(1);

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
			var func = _tenrai.Awaiting(x => x.GetAnimeReviewsAsync(malId, new ReviewsSearchConfig { Page = 2 }));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeReviewsAsync_CorrectIdWrongPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetAnimeReviewsAsync(1, new ReviewsSearchConfig { Page = page }));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Fact]
		public async Task GetAnimeReviewsAsync_BebopIdSecondPage_ShouldParseCowboyBebopReviewsPaged()
		{
			// When
			var bebop = await _tenrai.GetAnimeReviewsAsync(1, new ReviewsSearchConfig { Page = 2 });

			// Then
			var firstReview = bebop.Data.First();
			using (new AssertionScope())
			{
				firstReview.EpisodesWatched.Should().Be(null);
				firstReview.Score.Should().BeGreaterOrEqualTo(8);
			}
		}

		[Fact]
		public async Task GetAnimeReviewsAsync_Username_AllByUser()
		{
			// Given
			var username = (await _tenrai.GetAnimeReviewsAsync(1)).Data.First().User.Username;

			// When
			var reviews = await _tenrai.GetAnimeReviewsAsync(1, new ReviewsSearchConfig { Username = username });

			// Then
			reviews.Data.Should().NotBeEmpty();
			reviews.Data.Should().OnlyContain(review => review.User.Username == username);
		}
	}
}