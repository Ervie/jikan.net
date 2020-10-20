using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class AnimeExtendedTestClass
	{
		private readonly IJikan _jikan;

		public AnimeExtendedTestClass()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetAnimeEpisodes_BebopId_ShouldParseCowboyBebopEpisode()
		{
			// When
			AnimeEpisodes bebop = await _jikan.GetAnimeEpisodes(1);

			// Then
			using (new AssertionScope())
			{
				bebop.EpisodeCollection.Should().HaveCount(26);
				bebop.EpisodeCollection.First().Title.Should().Be("Asteroid Blues");
			}
		}

		[Fact]
		public async Task GetAnimeCharactersStaff_BebopId_ShouldParseCowboyBebopCharactersAndStaff()
		{
			// When
			AnimeCharactersStaff bebop = await _jikan.GetAnimeCharactersStaff(1);

			// Then
			using (new AssertionScope())
			{
				bebop.Characters.Should().Contain(x => x.Name.Equals("Black, Jet"));
				bebop.Staff.Where(x => x.Role.Contains("Director") && x.Role.Contains("Script")).Select(x => x.Name).Should().Contain("Watanabe, Shinichiro");
			}
		}

		[Fact]
		public async Task GetAnimePictures_BebopId_ShouldParseCowboyBebopImages()
		{
			// When
			AnimePictures bebop = await _jikan.GetAnimePictures(1);

			// Then
			bebop.Pictures.Should().HaveCount(13);
		}

		[Fact]
		public async Task GetAnimeVideos_BebopId_ShouldParseCowboyBebopVideos()
		{
			// When
			AnimeVideos bebop = await _jikan.GetAnimeVideos(1);

			// Then
			using (new AssertionScope())
			{
				bebop.PromoVideos.Should().HaveCount(3);
				bebop.PromoVideos.Select(x => x.Title).Should().Contain("PV 2");
				bebop.EpisodeVideos.Should().HaveCount(26);
				bebop.EpisodeVideos.Select(x => x.Title).Should().Contain("Pierrot Le Fou");
			}
		}

		[Fact]
		public async Task GetAnimeStatistics_BebopId_ShouldParseCowboyBebopStats()
		{
			// When
			AnimeStats bebop = await _jikan.GetAnimeStatistics(1);

			// Then
			using (new AssertionScope())
			{
				bebop.ScoreStats.Should().NotBeNull();
				bebop.Completed.Should().BeGreaterThan(450000);
				bebop.PlanToWatch.Should().BeGreaterThan(50000);
				bebop.ScoreStats._5.Votes.Should().BeGreaterThan(5000);
			}
		}

		[Fact]
		public async Task GetAnimeNews_BebopId_ShouldParseCowboyBebopNews()
		{
			// When
			AnimeNews bebop = await _jikan.GetAnimeNews(1);

			// Then
			using (new AssertionScope())
			{
				bebop.News.Should().HaveCount(7);
				bebop.News.Select(x => x.Author).Should().Contain("Snow");
			}
		}

		[Fact]
		public async Task GetAnimeForumTopics_BebopId_ShouldParseCowboyBebopTopics()
		{
			// When
			ForumTopics bebop = await _jikan.GetAnimeForumTopics(1);

			// Then
			bebop.Topics.Should().HaveCount(15);
		}

		[Fact]
		public async Task GetAnimeMoreInfo_BebopId_ShouldParseCowboyBebopMoreInfo()
		{
			// When
			MoreInfo bebop = await _jikan.GetAnimeMoreInfo(1);

			// Then
			bebop.Info.Should().Contain("Suggested Order of Viewing");
		}

		[Fact]
		public async Task GetAnimeRecommendation_BebopId_ShouldParseCowboyBebopRecommendations()
		{
			// When
			Recommendations bebop = await _jikan.GetAnimeRecommendations(1);

			// Then
			using (new AssertionScope())
			{
				bebop.RecommendationCollection.First().MalId.Should().Be(205); // Samurai Champloo
				bebop.RecommendationCollection.First().RecommendationCount.Should().BeGreaterThan(70);
				bebop.RecommendationCollection.Count.Should().BeGreaterThan(100);
			}
		}

		[Fact]
		public async Task GetAnimeReviews_BebopId_ShouldParseCowboyBebopReviews()
		{
			// When
			AnimeReviews bebop = await _jikan.GetAnimeReviews(1);

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

		[Fact]
		public async Task GetAnimeReviews_BebopIdSecondPage_ShouldParseCowboyBebopReviewsPaged()
		{
			// When
			AnimeReviews bebop = await _jikan.GetAnimeReviews(1, 2);

			// Then
			var firstReview = bebop.Reviews.First();
			using (new AssertionScope())
			{
				firstReview.Reviewer.EpisodesSeen.Should().Be(26);
				firstReview.HelpfulCount.Should().BeGreaterThan(5);
			}
		}

		[Fact]
		public async Task GetAnimeUserUpdates_BebopId_ShouldParseCowboyBebopUserUpdates()
		{
			// When
			AnimeUserUpdates bebop = await _jikan.GetAnimeUserUpdates(1);

			// Then
			var firstUpdate = bebop.Updates.First();
			using (new AssertionScope())
			{
				bebop.Updates.Should().HaveCount(75);
				firstUpdate.Date.Value.Should().BeBefore(DateTime.Now);
			}
		}

		[Fact]
		public async Task GetAnimeUserUpdates_BebopIdSecondPage_ShouldParseCowboyBebopUserUpdatesPaged()
		{
			// When
			AnimeUserUpdates bebop = await _jikan.GetAnimeUserUpdates(1, 2);

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