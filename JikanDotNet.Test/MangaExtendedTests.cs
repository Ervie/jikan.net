using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MangaExtendedTests
	{
		private readonly IJikan _jikan;

		public MangaExtendedTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaPictures_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<MangaPictures>> func = _jikan.Awaiting(x => x.GetMangaPictures(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaPictures_MonsterId_ShouldParseMonsterImages()
		{
			// When
			var monster = await _jikan.GetMangaPictures(1);

			// Then
			monster.Pictures.Should().HaveCount(8);
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaCharacters_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<MangaCharacters>> func = _jikan.Awaiting(x => x.GetMangaCharacters(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaStatistics_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<MangaStats>> func = _jikan.Awaiting(x => x.GetMangaStatistics(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaNews_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<MangaNews>> func = _jikan.Awaiting(x => x.GetMangaNews(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaForumTopics_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaForumTopics(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaForumTopics_MonsterId_ShouldParseMonsterTopics()
		{
			// When
			var monster = await _jikan.GetMangaForumTopics(1);

			// Then
			var topics = monster.Data.Select(x => x.MalId);
			using (new AssertionScope())
			{
				topics.Should().Contain(395611);
				topics.Should().Contain(57668);
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaMoreInfo_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<MoreInfo>> func = _jikan.Awaiting(x => x.GetMangaMoreInfo(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaMoreInfo_BerserkId_ShouldParseBerserkMoreInfo()
		{
			// When
			var berserk = await _jikan.GetMangaMoreInfo(2);

			berserk.Info.Should().Contain("The Prototype (1988)");
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaRecommendations_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<Recommendations>> func = _jikan.Awaiting(x => x.GetMangaRecommendations(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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
				//berserk.RecommendationCollection.First().MalId.Should().Be(583);
				//berserk.RecommendationCollection.First().RecommendationCount.Should().BeGreaterThan(25);
				berserk.RecommendationCollection.Count.Should().BeGreaterThan(90);
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaReviews_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<MangaReviews>> func = _jikan.Awaiting(x => x.GetMangaReviews(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaReviews_SecondPageInvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<MangaReviews>> func = _jikan.Awaiting(x => x.GetMangaReviews(id, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaReviews_VvalidIdIndalidPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<MangaReviews>> func = _jikan.Awaiting(x => x.GetMangaReviews(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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