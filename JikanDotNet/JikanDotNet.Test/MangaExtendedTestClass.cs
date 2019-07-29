using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MangaExtendedTestClass
	{
		private readonly IJikan jikan;

		public MangaExtendedTestClass()
		{
			jikan = new Jikan();
		}

		[Fact]
		public async Task GetMangaPictures_MonsterId_ShouldParseMonsterImages()
		{
			MangaPictures monster = await jikan.GetMangaPictures(1);

			Assert.Equal(8, monster.Pictures.Count);
		}

		[Fact]
		public async Task GetMangaCharacters_MonsterId_ShouldParseMonsterCharacters()
		{
			MangaCharacters monster = await jikan.GetMangaCharacters(1);

			Assert.Equal(33, monster.Characters.Count);
		}

		[Fact]
		public async Task GetMangaPictures_MonsterId_ShouldParseMonsterCharactersJohan()
		{
			MangaCharacters monster = await jikan.GetMangaCharacters(1);

			Assert.Contains("Liebert, Johan", monster.Characters.Select(x => x.Name));
		}

		[Fact]
		public async Task GetMangaStatistics_MonsterId_ShouldParseMonsterStats()
		{
			MangaStats monster = await jikan.GetMangaStatistics(1);

			Assert.NotNull(monster.ScoreStats);
			Assert.True(monster.Completed > 25000);
			Assert.True(monster.Dropped > 500);
		}

		[Fact]
		public async Task GetMangaNews_MonsterId_ShouldParseMonsterNews()
		{
			MangaNews monster = await jikan.GetMangaNews(1);

			Assert.Equal(11, monster.News.Count);
			Assert.Contains("Xinil", monster.News.Select(x => x.Author));
		}

		[Fact]
		public async Task GetMangaForumTopics_MonsterId_ShouldParseMonsterTopics()
		{
			ForumTopics monster = await jikan.GetMangaForumTopics(1);

			Assert.Contains(1672449, monster.Topics.Select(x => x.TopicId));
			Assert.Contains(155394, monster.Topics.Select(x => x.TopicId));
			Assert.Contains(395621, monster.Topics.Select(x => x.TopicId));
		}

		[Fact]
		public async Task GetMangaMoreInfo_BerserkId_ShouldParseBerserkMoreInfo()
		{
			MoreInfo berserk = await jikan.GetMangaMoreInfo(2);

			Assert.Contains("The Prototype (1988)", berserk.Info);
		}

		[Fact]
		public async Task GetMangaRecommendation_BerserkId_ShouldParseBerserkRecommendations()
		{
			Recommendations berserk = await jikan.GetMangaRecommendations(2);

			//Claymore
			Assert.Equal(583, berserk.RecommendationCollection.First().MalId);
			Assert.True(berserk.RecommendationCollection.First().RecommendationCount > 25);
			Assert.True(berserk.RecommendationCollection.Count > 90);
		}

		[Fact]
		public async Task GetMangaReviews_BerserkId_ShouldParseBerserkReviews()
		{
			MangaReviews berserk = await jikan.GetMangaReviews(2);

			Assert.Equal("TheCriticsClub", berserk.Reviews.First().Reviewer.Username);
			Assert.Equal(4403, berserk.Reviews.First().MalId);
			Assert.Equal(0, berserk.Reviews.First().Reviewer.ChaptersRead);
			Assert.True(berserk.Reviews.First().HelpfulCount > 1200);

			Assert.Equal(10, berserk.Reviews.First().Reviewer.Scores.Overall);
			Assert.Equal(9, berserk.Reviews.First().Reviewer.Scores.Story);
		}

		[Fact]
		public async Task GetMangaReviews_BerserkIdSecondPage_ShouldParseBerserkReviewsPaged()
		{
			MangaReviews berserk = await jikan.GetMangaReviews(2, 2);

			Assert.Equal("ChickenSpoon", berserk.Reviews.First().Reviewer.Username);
			Assert.Equal(80128, berserk.Reviews.First().MalId);
			Assert.Equal(0, berserk.Reviews.First().Reviewer.ChaptersRead);
			Assert.True(berserk.Reviews.First().HelpfulCount > 15);

			Assert.Equal(9, berserk.Reviews.First().Reviewer.Scores.Overall);
			Assert.Equal(8, berserk.Reviews.First().Reviewer.Scores.Story);
		}

		[Fact]
		public async Task GetMangaUserUpdates_MonsterId_ShouldParseMonsterUserUpdates()
		{
			MangaUserUpdates monster = await jikan.GetMangaUserUpdates(1);

			var firstUpdate = monster.Updates.First();

			Assert.Equal(75, monster.Updates.Count);
			Assert.Equal(DateTime.Now.Day, firstUpdate.Date.Value.Day);
			Assert.True(!firstUpdate.ChaptersTotal.HasValue || firstUpdate.ChaptersTotal == 162);
		}

		[Fact]
		public async Task GetMangaUserUpdates_MonsterIdSecondPage_ShouldParseMonsterUserUpdatesPaged()
		{
			MangaUserUpdates monster = await jikan.GetMangaUserUpdates(1, 2);

			var firstUpdate = monster.Updates.First();

			Assert.Equal(75, monster.Updates.Count);
			Assert.True(!firstUpdate.ChaptersTotal.HasValue || firstUpdate.ChaptersTotal == 162);
		}
	}
}