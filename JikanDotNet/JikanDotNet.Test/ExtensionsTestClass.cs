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
			Person norioWakamoto = Task.Run(() => jikan.GetPerson(84, PersonExtension.Pictures)).Result;

			Assert.Equal(4, norioWakamoto.Images.Count);
		}

		[Fact]
		public void ShouldParseKirimaSharoImages()
		{
			Character kirimaSharo = Task.Run(() => jikan.GetCharacter(94947, CharacterExtension.Pictures)).Result;

			Assert.Equal(8, kirimaSharo.Images.Count);
		}

		[Fact]
		public void ShouldParseMonsterImages()
		{
			Manga monster = Task.Run(() => jikan.GetManga(1, MangaExtension.Pictures)).Result;

			Assert.Equal(8, monster.Images.Count);
		}

		[Fact]
		public void ShouldParseMonsterCharacters()
		{
			Manga monster = Task.Run(() => jikan.GetManga(1, MangaExtension.Characters)).Result;

			Assert.Equal(33, monster.Characters.Count);
		}

		[Fact]
		public void ShouldParseMonsterCharactersJohan()
		{
			Manga monster = Task.Run(() => jikan.GetManga(1, MangaExtension.Characters)).Result;

			Assert.Contains("Liebert, Johan", monster.Characters.Select(x => x.Name));
		}

		[Fact]
		public void ShouldParseMonsterStats()
		{
			Manga monster = Task.Run(() => jikan.GetManga(1, MangaExtension.Stats)).Result;

			Assert.NotNull(monster.ScoreStats);
		}

		[Fact]
		public void ShouldParseMonsterNews()
		{
			Manga monster = Task.Run(() => jikan.GetManga(1, MangaExtension.News)).Result;

			Assert.Equal(11, monster.News.Count);
			Assert.Contains("Xinil", monster.News.Select(x => x.Author));
		}

		[Fact]
		public void ShouldParseMonsterTopics()
		{
			Manga monster = Task.Run(() => jikan.GetManga(1, MangaExtension.Forum)).Result;

			Assert.Contains(1672449, monster.Topics.Select(x => x.TopicId));
			Assert.Contains(155394, monster.Topics.Select(x => x.TopicId));
			Assert.Contains(395621, monster.Topics.Select(x => x.TopicId));
		}

		[Fact]
		public void ShouldParseBerserkMoreInfo()
		{
			Manga berserk = Task.Run(() => jikan.GetManga(2, MangaExtension.MoreInfo)).Result;

			Assert.Contains("The Prototype (1988)", berserk.MoreInfo);
		}
	}
}