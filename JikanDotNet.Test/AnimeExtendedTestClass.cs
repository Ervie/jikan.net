using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class AnimeExtendedTestClass
	{
		private readonly IJikan jikan;

		public AnimeExtendedTestClass()
		{
			jikan = new Jikan();
		}

		[Fact]
		public async Task GetAnimeEpisodes_BebopId_ShouldParseCowboyBebopEpisode()
		{
			AnimeEpisodes bebop = await jikan.GetAnimeEpisodes(1);

			Assert.Equal(26, bebop.EpisodeCollection.Count);
			Assert.Equal("Asteroid Blues", bebop.EpisodeCollection.First().Title);
		}

		[Fact]
		public async Task GetAnimeCharactersStaff_BebopId_ShouldParseCowboyBebopCharactersAndStaff()
		{
			AnimeCharactersStaff bebop = await jikan.GetAnimeCharactersStaff(1);

			Assert.Contains("Black, Jet", bebop.Characters.Select(x => x.Name));
			Assert.Contains("Watanabe, Shinichiro", bebop.Staff.Where(x => x.Role.Contains("Director") && x.Role.Contains("Script")).Select(x => x.Name));
		}

		[Fact]
		public async Task GetAnimePictures_BebopId_ShouldParseCowboyBebopImages()
		{
			AnimePictures bebop = await jikan.GetAnimePictures(1);

			Assert.Equal(11, bebop.Pictures.Count);
		}

		[Fact]
		public async Task GetAnimeVideos_BebopId_ShouldParseCowboyBebopVideos()
		{
			AnimeVideos bebop = await jikan.GetAnimeVideos(1);

			Assert.Equal(3, bebop.PromoVideos.Count);
			Assert.Contains("PV 2", bebop.PromoVideos.Select(x => x.Title));
			Assert.Equal(26, bebop.EpisodeVideos.Count);
			Assert.Contains("Pierrot Le Fou", bebop.EpisodeVideos.Select(x => x.Title));
		}

		[Fact]
		public async Task GetAnimeStatistics_BebopId_ShouldParseCowboyBebopStats()
		{
			AnimeStats bebop = await jikan.GetAnimeStatistics(1);

			Assert.NotNull(bebop.ScoreStats);
			Assert.True(bebop.Completed > 450000);
			Assert.True(bebop.PlanToWatch > 50000);
			Assert.True(bebop.ScoreStats._5.Votes > 5000);
		}

		[Fact]
		public async Task GetAnimeNews_BebopId_ShouldParseCowboyBebopNews()
		{
			AnimeNews bebop = await jikan.GetAnimeNews(1);

			Assert.Equal(6, bebop.News.Count);
			Assert.Contains("Snow", bebop.News.Select(x => x.Author));
		}

		[Fact]
		public async Task GetAnimeForumTopics_BebopId_ShouldParseCowboyBebopTopics()
		{
			ForumTopics bebop = await jikan.GetAnimeForumTopics(1);

			Assert.Equal(15, bebop.Topics.Count);
		}

		[Fact]
		public async Task GetAnimeMoreInfo_BebopId_ShouldParseCowboyBebopMoreInfo()
		{
			MoreInfo bebop = await jikan.GetAnimeMoreInfo(1);

			Assert.Contains("Suggested Order of Viewing", bebop.Info);
		}

		[Fact]
		public async Task GetAnimeRecommendation_BebopId_ShouldParseCowboyBebopRecommendations()
		{
			Recommendations bebop = await jikan.GetAnimeRecommendations(1);

			//Samurai Champloo
			Assert.Equal(205, bebop.RecommendationCollection.First().MalId);
			Assert.True(bebop.RecommendationCollection.First().RecommendationCount > 70);
			Assert.True(bebop.RecommendationCollection.Count > 100);
		}

		[Fact]
		public async Task GetAnimeReviews_BebopId_ShouldParseCowboyBebopReviews()
		{
			AnimeReviews bebop = await jikan.GetAnimeReviews(1);

			Assert.Equal("TheLlama", bebop.Reviews.First().Reviewer.Username);
			Assert.Equal(7406, bebop.Reviews.First().MalId);
			Assert.Equal(26, bebop.Reviews.First().Reviewer.EpisodesSeen);
			Assert.True(bebop.Reviews.First().HelpfulCount > 1400);

			Assert.Equal(10, bebop.Reviews.First().Reviewer.Scores.Overall);
			Assert.Equal(9, bebop.Reviews.First().Reviewer.Scores.Animation);

		}

		[Fact]
		public async Task GetAnimeReviews_BebopIdSecondPage_ShouldParseCowboyBebopReviewsPaged()
		{
			AnimeReviews bebop = await jikan.GetAnimeReviews(1, 2);

			Assert.Equal(26, bebop.Reviews.First().Reviewer.EpisodesSeen);
			Assert.True(bebop.Reviews.First().HelpfulCount > 5);
		}

		[Fact]
		public async Task GetAnimeUserUpdates_BebopId_ShouldParseCowboyBebopUserUpdates()
		{
			AnimeUserUpdates bebop = await jikan.GetAnimeUserUpdates(1);

			var firstUpdate = bebop.Updates.First();

			Assert.Equal(75, bebop.Updates.Count);
			Assert.True(DateTime.Now >= firstUpdate.Date.Value);
			Assert.True(!firstUpdate.EpisodesTotal.HasValue || firstUpdate.EpisodesTotal == 26);
		}

		[Fact]
		public async Task GetAnimeUserUpdates_BebopIdSecondPage_ShouldParseCowboyBebopUserUpdatesPaged()
		{
			AnimeUserUpdates bebop = await jikan.GetAnimeUserUpdates(1, 2);

			var firstUpdate = bebop.Updates.First();

			Assert.Equal(75, bebop.Updates.Count);
			Assert.True(!firstUpdate.EpisodesTotal.HasValue || firstUpdate.EpisodesTotal == 26);
		}
	}
}
