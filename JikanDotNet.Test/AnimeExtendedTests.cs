using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class AnimeExtendedTests
	{
		private readonly IJikan _jikan;

		public AnimeExtendedTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimePictures_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<AnimePictures>> func = _jikan.Awaiting(x => x.GetAnimePictures(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimePictures_BebopId_ShouldParseCowboyBebopImages()
		{
			// When
			var bebop = await _jikan.GetAnimePictures(1);

			// Then
			bebop.Pictures.Should().HaveCount(13);
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeVideos_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<AnimeVideos>> func = _jikan.Awaiting(x => x.GetAnimeVideos(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeVideos_BebopId_ShouldParseCowboyBebopVideos()
		{
			// When
			var bebop = await _jikan.GetAnimeVideos(1);

			// Then
			using (new AssertionScope())
			{
				bebop.PromoVideos.Should().HaveCount(3);
				bebop.PromoVideos.Select(x => x.Title).Should().Contain("PV 2");
				bebop.EpisodeVideos.Should().HaveCount(26);
				bebop.EpisodeVideos.Select(x => x.Title).Should().Contain("Pierrot Le Fou");
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeStatistics_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<AnimeStats>> func = _jikan.Awaiting(x => x.GetAnimeStatistics(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeStatistics_BebopId_ShouldParseCowboyBebopStats()
		{
			// When
			var bebop = await _jikan.GetAnimeStatistics(1);

			// Then
			using (new AssertionScope())
			{
				bebop.ScoreStats.Should().NotBeNull();
				bebop.Completed.Should().BeGreaterThan(450000);
				bebop.PlanToWatch.Should().BeGreaterThan(50000);
				bebop.ScoreStats._5.Votes.Should().BeGreaterThan(5000);
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeNews_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<AnimeNews>> func = _jikan.Awaiting(x => x.GetAnimeNews(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeNews_BebopId_ShouldParseCowboyBebopNews()
		{
			// When
			var bebop = await _jikan.GetAnimeNews(1);

			// Then
			using (new AssertionScope())
			{
				bebop.News.Should().HaveCount(7);
				bebop.News.Select(x => x.Author).Should().Contain("Snow");
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeForumTopics_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<ForumTopics>> func = _jikan.Awaiting(x => x.GetAnimeForumTopics(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeForumTopics_BebopId_ShouldParseCowboyBebopTopics()
		{
			// When
			var bebop = await _jikan.GetAnimeForumTopics(1);

			// Then
			bebop.Topics.Should().HaveCount(15);
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeMoreInfo_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<MoreInfo>> func = _jikan.Awaiting(x => x.GetAnimeMoreInfo(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeMoreInfo_BebopId_ShouldParseCowboyBebopMoreInfo()
		{
			// When
			var bebop = await _jikan.GetAnimeMoreInfo(1);

			// Then
			bebop.Info.Should().Contain("Suggested Order of Viewing");
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeRecommendations_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<Recommendations>> func = _jikan.Awaiting(x => x.GetAnimeRecommendations(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeRecommendation_BebopId_ShouldParseCowboyBebopRecommendations()
		{
			// When
			var bebop = await _jikan.GetAnimeRecommendations(1);

			// Then
			using (new AssertionScope())
			{
				bebop.RecommendationCollection.First().MalId.Should().Be(205); // Samurai Champloo
				bebop.RecommendationCollection.First().RecommendationCount.Should().BeGreaterThan(70);
				bebop.RecommendationCollection.Count.Should().BeGreaterThan(100);
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeReviews_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<AnimeReviews>> func = _jikan.Awaiting(x => x.GetAnimeReviews(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeReviews_BebopId_ShouldParseCowboyBebopReviews()
		{
			// When
			var bebop = await _jikan.GetAnimeReviews(1);

			// Then
			var firstReview = bebop.Reviews.First();
			using (new AssertionScope())
			{
				firstReview.Reviewer.Username.Should().Be("TheLlama");
				firstReview.MalId.Should().Be(7406);
				firstReview.Reviewer.EpisodesSeen.Should().Be(26);
				firstReview.HelpfulCount.Should().BeGreaterThan(1400);

				firstReview.Reviewer.Scores.Overall.Should().Be(10);
				firstReview.Reviewer.Scores.Animation.Should().Be(9);
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeReviews_SecondPageWithInvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<AnimeReviews>> func = _jikan.Awaiting(x => x.GetAnimeReviews(malId, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeReviews_CorrectIdWrongPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<AnimeReviews>> func = _jikan.Awaiting(x => x.GetAnimeReviews(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeReviews_BebopIdSecondPage_ShouldParseCowboyBebopReviewsPaged()
		{
			// When
			var bebop = await _jikan.GetAnimeReviews(1, 2);

			// Then
			var firstReview = bebop.Reviews.First();
			using (new AssertionScope())
			{
				firstReview.Reviewer.EpisodesSeen.Should().Be(26);
				firstReview.HelpfulCount.Should().BeGreaterThan(5);
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeUserUpdates_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<AnimeUserUpdates>> func = _jikan.Awaiting(x => x.GetAnimeUserUpdates(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeUserUpdates_BebopId_ShouldParseCowboyBebopUserUpdates()
		{
			// When
			var bebop = await _jikan.GetAnimeUserUpdates(1);

			// Then
			var firstUpdate = bebop.Updates.First();
			using (new AssertionScope())
			{
				bebop.Updates.Should().HaveCount(75);
				firstUpdate.Date.Value.Should().BeBefore(DateTime.Now);
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeUserUpdates_SecondPageInvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<AnimeUserUpdates>> func = _jikan.Awaiting(x => x.GetAnimeUserUpdates(malId, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeUserUpdates_ValidIdInvalidpage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<AnimeUserUpdates>> func = _jikan.Awaiting(x => x.GetAnimeUserUpdates(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeUserUpdates_BebopIdSecondPage_ShouldParseCowboyBebopUserUpdatesPaged()
		{
			// When
			var bebop = await _jikan.GetAnimeUserUpdates(1, 2);

			// Then
			var firstUpdate = bebop.Updates.First();
			using (new AssertionScope())
			{
				bebop.Updates.Should().HaveCount(75);
				firstUpdate.EpisodesTotal.Should().HaveValue().And.Be(26);
			}
		}
	}
}