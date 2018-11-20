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
		public void ShouldParseNorioWakamotoImages()
		{
			PersonPictures norioWakamoto = Task.Run(() => jikan.GetPersonPictures(84)).Result;

			Assert.Equal(4, norioWakamoto.Pictures.Count);
		}

		[Fact]
		public void ShouldParseKirimaSharoImages()
		{
			CharacterPictures kirimaSharo = Task.Run(() => jikan.GetCharacterPictures(94947)).Result;

			Assert.Equal(8, kirimaSharo.Pictures.Count);
		}

		[Fact]
		public void ShouldParseMonsterImages()
		{
			MangaPictures monster = Task.Run(() => jikan.GetMangaPictures(1)).Result;

			Assert.Equal(8, monster.Pictures.Count);
		}

		[Fact]
		public void ShouldParseMonsterCharacters()
		{
			MangaCharacters monster = Task.Run(() => jikan.GetMangaCharacters(1)).Result;

			Assert.Equal(33, monster.Characters.Count);
		}

		[Fact]
		public void ShouldParseMonsterCharactersJohan()
		{
			MangaCharacters monster = Task.Run(() => jikan.GetMangaCharacters(1)).Result;

			Assert.Contains("Liebert, Johan", monster.Characters.Select(x => x.Name));
		}

		[Fact]
		public void ShouldParseMonsterStats()
		{
			MangaStats monster = Task.Run(() => jikan.GetMangaStatistics(1)).Result;

			Assert.NotNull(monster.ScoreStats);
			Assert.True(monster.Completed > 25000);
			Assert.True(monster.Dropped > 500);
		}

		[Fact]
		public void ShouldParseMonsterNews()
		{
			MangaNews monster = Task.Run(() => jikan.GetMangaNews(1)).Result;

			Assert.Equal(11, monster.News.Count);
			Assert.Contains("Xinil", monster.News.Select(x => x.Author));
		}

		[Fact]
		public void ShouldParseMonsterTopics()
		{
			ForumTopics monster = Task.Run(() => jikan.GetMangaForumTopics(1)).Result;

			Assert.Contains(1672449, monster.Topics.Select(x => x.TopicId));
			Assert.Contains(155394, monster.Topics.Select(x => x.TopicId));
			Assert.Contains(395621, monster.Topics.Select(x => x.TopicId));
		}

		[Fact]
		public void ShouldParseBerserkMoreInfo()
		{
			MoreInfo berserk = Task.Run(() => jikan.GetMangaMoreInfo(2)).Result;

			Assert.Contains("The Prototype (1988)", berserk.Info);
			}

		[Fact]
		public void ShouldParseCowboyBebopEpisode()
		{
			AnimeEpisodes bebop = Task.Run(() => jikan.GetAnimeEpisodes(1)).Result;

			Assert.Equal(26, bebop.EpisodeCollection.Count);
			Assert.Equal("Asteroid Blues", bebop.EpisodeCollection.First().Title);
		}

		[Fact]
		public void ShouldParseCowboyBebopCharactersAndStaff()
		{
			AnimeCharactersStaff bebop = Task.Run(() => jikan.GetAnimeCharactersStaff(1)).Result;

			Assert.Contains("Black, Jet", bebop.Characters.Select(x => x.Name));
			Assert.Contains("Watanabe, Shinichiro", bebop.Staff.Where(x => x.Role.Contains("Director") && x.Role.Contains("Script")).Select(x => x.Name));
		}

		[Fact]
		public void ShouldParseCowboyBebopImages()
		{
			AnimePictures bebop = Task.Run(() => jikan.GetAnimePictures(1)).Result;

			Assert.Equal(11, bebop.Pictures.Count);
		}

		[Fact]
		public void ShouldParseCowboyBebopVideos()
		{
			AnimeVideos bebop = Task.Run(() => jikan.GetAnimeVideos(1)).Result;
			
			Assert.Equal(3, bebop.PromoVideos.Count);
			Assert.Contains("PV 2", bebop.PromoVideos.Select(x => x.Title));
			Assert.Equal(26, bebop.EpisodeVideos.Count);
			Assert.Contains("Pierrot Le Fou", bebop.EpisodeVideos.Select(x => x.Title));
		}

		[Fact]
		public void ShouldParseCowboyBebopStats()
		{
			AnimeStats bebop = Task.Run(() => jikan.GetAnimeStatistics(1)).Result;

			Assert.NotNull(bebop.ScoreStats);
			Assert.True(bebop.Completed > 450000);
			Assert.True(bebop.PlanToWatch > 50000);
			Assert.True(bebop.ScoreStats._5.Votes > 5000);
		}

		[Fact]
		public void ShouldParseCowboyBebopNews()
		{
			AnimeNews bebop = Task.Run(() => jikan.GetAnimeNews(1)).Result;

			Assert.Equal(6, bebop.News.Count);
			Assert.Contains("Snow", bebop.News.Select(x => x.Author));
		}

		[Fact]
		public void ShouldParseCowboyBebopTopics()
		{
			ForumTopics bebop = Task.Run(() => jikan.GetAnimeForumTopics(1)).Result;

			Assert.Contains(1739374, bebop.Topics.Select(x => x.TopicId));
			Assert.Contains(29334, bebop.Topics.Select(x => x.TopicId));
			Assert.Contains(29323, bebop.Topics.Select(x => x.TopicId));
		}

		[Fact]
		public void ShouldParseCowboyBebopMoreInfo()
		{
			MoreInfo bebop = Task.Run(() => jikan.GetAnimeMoreInfo(1)).Result;

			Assert.Contains("Suggested Order of Viewing", bebop.Info);
		}
	}
}