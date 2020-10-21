using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MangaExtendedTestClass
	{
		private readonly IJikan _jikan;

		public MangaExtendedTestClass()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetMangaPictures_MonsterId_ShouldParseMonsterImages()
		{
			// When
			var monster = await _jikan.GetMangaPictures(1);

			// Then
			monster.Pictures.Should().HaveCount(8);
		}

		[Fact]
		public async Task GetMangaCharacters_MonsterId_ShouldParseMonsterCharacters()
		{
			// When
			var monster = await _jikan.GetMangaCharacters(1);

			// Then
			monster.Characters.Should().HaveCount(33);
		}

		[Fact]
		public async Task GetMangaPictures_MonsterId_ShouldParseMonsterCharactersJohan()
		{
			// When
			var monster = await _jikan.GetMangaCharacters(1);

			// Then
			monster.Characters.Select(x => x.Name).Should().Contain("Liebert, Johan");
		}

		[Fact]
		public async Task GetMangaStatistics_MonsterId_ShouldParseMonsterStats()
		{
			// When
			var monster = await _jikan.GetMangaStatistics(1);

			// Then
			using (new AssertionScope())
			{
				monster.ScoreStats.Should().NotBeNull();
				monster.Completed.Should().BeGreaterThan(25000);
				monster.Dropped.Should().BeGreaterThan(500);
			}
		}

		[Fact]
		public async Task GetMangaNews_MonsterId_ShouldParseMonsterNews()
		{
			// When
			var monster = await _jikan.GetMangaNews(1);

			// Then
			using (new AssertionScope())
			{
				monster.News.Should().HaveCount(11);
				monster.News.Select(x => x.Author).Should().Contain("Xinil");
			}
		}

		[Fact]
		public async Task GetMangaForumTopics_MonsterId_ShouldParseMonsterTopics()
		{
			// When
			var monster = await _jikan.GetMangaForumTopics(1);

			// Then
			var topics = monster.Topics.Select(x => x.TopicId);
			using (new AssertionScope())
			{
				topics.Should().Contain(395611);
				topics.Should().Contain(57668);
			}
		}

		[Fact]
		public async Task GetMangaMoreInfo_BerserkId_ShouldParseBerserkMoreInfo()
		{
			// When
			var berserk = await _jikan.GetMangaMoreInfo(2);

			berserk.Info.Should().Contain("The Prototype (1988)");
		}

		[Fact]
		public async Task GetMangaRecommendation_BerserkId_ShouldParseBerserkRecommendations()
		{
			// When
			var berserk = await _jikan.GetMangaRecommendations(2);

			// Then
			using (new AssertionScope())
			{
				//Claymore
				berserk.RecommendationCollection.First().MalId.Should().Be(583);
				berserk.RecommendationCollection.First().RecommendationCount.Should().BeGreaterThan(25);
				berserk.RecommendationCollection.Count.Should().BeGreaterThan(90);
			}
		}

		[Fact]
		public async Task GetMangaReviews_BerserkId_ShouldParseBerserkReviews()
		{
			// When
			var berserk = await _jikan.GetMangaReviews(2);

			// Then
			using (new AssertionScope())
			{
				berserk.Reviews.First().Reviewer.Username.Should().Be("TheCriticsClub");
				berserk.Reviews.First().MalId.Should().Be(4403);
				berserk.Reviews.First().HelpfulCount.Should().BeGreaterThan(1200);

				berserk.Reviews.First().Reviewer.Scores.Overall.Should().Be(10);
				berserk.Reviews.First().Reviewer.Scores.Story.Should().Be(9);
			}
		}

		[Fact]
		public async Task GetMangaReviews_BerserkIdSecondPage_ShouldParseBerserkReviewsPaged()
		{
			// When
			var berserk = await _jikan.GetMangaReviews(2, 2);

			// Then
			using (new AssertionScope())
			{
				berserk.Reviews.First().Reviewer.Username.Should().Be("Sibi_Gowtham");
				berserk.Reviews.First().MalId.Should().Be(261738);
				berserk.Reviews.First().Reviewer.ChaptersRead.Should().Be(351);
				berserk.Reviews.First().HelpfulCount.Should().BeGreaterThan(15);

				berserk.Reviews.First().Reviewer.Scores.Overall.Should().Be(4);
				berserk.Reviews.First().Reviewer.Scores.Story.Should().Be(5);
			}
		}

		[Fact]
		public async Task GetMangaUserUpdates_MonsterId_ShouldParseMonsterUserUpdates()
		{
			// When
			var monster = await _jikan.GetMangaUserUpdates(1);

			// Then
			var firstUpdate = monster.Updates.First();
			using (new AssertionScope())
			{
				monster.Updates.Should().HaveCount(75);
				firstUpdate.Date.Value.Should().BeBefore(DateTime.Now);
				firstUpdate.ChaptersTotal.Should().Be(162);
			}
		}

		[Fact]
		public async Task GetMangaUserUpdates_MonsterIdSecondPage_ShouldParseMonsterUserUpdatesPaged()
		{
			// When
			var monster = await _jikan.GetMangaUserUpdates(1, 2);

			// Then
			var firstUpdate = monster.Updates.First();
			using (new AssertionScope())
			{
				monster.Updates.Should().HaveCount(75);
				firstUpdate.Date.Value.Should().BeBefore(DateTime.Now);
				firstUpdate.ChaptersTotal.Should().Be(162);
			}
		}
	}
}