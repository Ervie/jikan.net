using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class ExtensionsTestClass
	{
		private readonly IJikan jikan;

		public ExtensionsTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public async Task GetPersonPictures_WakamotoId_ShouldParseNorioWakamotoImages()
		{
			PersonPictures norioWakamoto = await jikan.GetPersonPictures(84);

			Assert.Equal(4, norioWakamoto.Pictures.Count);
		}

		[Fact]
		public async Task GetCharacterPictures_SharoId_ShouldParseKirimaSharoImages()
		{
			CharacterPictures kirimaSharo = await jikan.GetCharacterPictures(94947);

			Assert.Equal(8, kirimaSharo.Pictures.Count);
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
			Assert.Equal(DateTime.Now.Day, firstUpdate.Date.Value.Day);
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