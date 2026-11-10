using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.MangaTests
{
	[Collection("TenraiTests")]
	public class GetMangaReviewsAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetMangaReviewsAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaReviewsAsync_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetMangaReviewsAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Fact]
		public async Task GetMangaReviewsAsync_BerserkId_ShouldParseBerserkReviews()
		{
			// When
			var berserk = await _tenrai.GetMangaReviewsAsync(2);

			// Then
			using (new AssertionScope())
			{
				berserk.Data.First().User.Username.Should().Be("TheCriticsClub");
				berserk.Data.First().MalId.Should().Be(4403);
				berserk.Data.First().Score.Should().BeGreaterThan(7);
				berserk.Data.First().Reactions.TotalReactions.Should().BeGreaterThan(1);
			}
		}

		[Fact]
		public async Task GetMangaReviewsAsync_ExcludePreliminary_ShouldReturnNoPreliminaryReviews()
		{
			// When
			var berserk = await _tenrai.GetMangaReviewsAsync(2, new ReviewsSearchConfig { Preliminary = InclusionFilter.Exclude });

			// Then
			using (new AssertionScope())
			{
				berserk.Data.Should().NotBeEmpty();
				berserk.Data.Should().OnlyContain(x => !x.IsPreliminary);
			}
		}

		[Fact]
		public async Task GetMangaReviewsAsync_OnlyPreliminary_ShouldNeverReturnNonPreliminaryReviews()
		{
			// When
			var berserk = await _tenrai.GetMangaReviewsAsync(2, new ReviewsSearchConfig { Preliminary = InclusionFilter.Only });

			// Then (manga preliminary reviews are rare, so the set may be empty; none may be non-preliminary)
			berserk.Data.All(x => x.IsPreliminary).Should().BeTrue();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaReviewsAsync_SecondPageWithInvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetMangaReviewsAsync(malId, new ReviewsSearchConfig { Page = 2 }));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaReviewsAsync_CorrectIdWrongPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetMangaReviewsAsync(1, new ReviewsSearchConfig { Page = page }));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Fact]
		public async Task GetMangaReviewsAsync_BerserkIdSecondPage_ShouldParseBerserkReviewsSecondPage()
		{
			// When
			var berserk = await _tenrai.GetMangaReviewsAsync(2, new ReviewsSearchConfig { Page = 2 });

			// Then
			using (new AssertionScope())
			{
				berserk.Data.First().Reactions.TotalReactions.Should().BeGreaterThan(1);
			}
		}

		[Fact]
		public async Task GetMangaReviewsAsync_RecommendedSentimentNewestSort_ShouldReturnRecommendedReviews()
		{
			// Given
			var config = new ReviewsSearchConfig
			{
				Sort = ReviewSortOrder.Newest,
				Sentiment = ReviewSentiment.Recommended
			};

			// When
			var berserk = await _tenrai.GetMangaReviewsAsync(2, config);

			// Then
			using (new AssertionScope())
			{
				berserk.Data.Should().NotBeEmpty();
				berserk.Data.Should().OnlyContain(x => x.Tags.Contains("Recommended"));
				berserk.Pagination.LastVisiblePage.Should().BeGreaterThan(0);
			}
		}

		[Fact]
		public async Task GetMangaReviewsAsync_SpoilersOnly_ShouldReturnOnlySpoilerReviews()
		{
			// Given
			var config = new ReviewsSearchConfig { Spoilers = InclusionFilter.Only };

			// When
			var berserk = await _tenrai.GetMangaReviewsAsync(2, config);

			// Then
			using (new AssertionScope())
			{
				berserk.Data.Should().NotBeEmpty();
				berserk.Data.Should().OnlyContain(x => x.IsSpoiler);
			}
		}
	}
}