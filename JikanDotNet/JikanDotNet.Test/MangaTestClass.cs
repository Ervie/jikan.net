using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MangaTestClass
	{
		private readonly IJikan jikan;

		public MangaTestClass()
		{
			jikan = new Jikan(true);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public async Task GetManga_CorrectId_ShouldReturnNotNullManga(long malId)
		{
			Manga returedManga = await jikan.GetManga(malId);

			Assert.NotNull(returedManga);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(5)]
		[InlineData(6)]
		public async Task GetManga_WrongId_ShouldReturnNullMana(long malId)
		{
			Manga returedManga = await jikan.GetManga(malId);

			Assert.Null(returedManga);
		}

		[Fact]
		public async Task GetManga_BerserkId_ShouldParseBerserk()
		{
			Manga berserkManga = await jikan.GetManga(2);

			Assert.Equal("Berserk", berserkManga.Title);
		}

		[Fact]
		public async Task GetManga_MonsterId_ShouldParseMonster()
		{
			Manga monsterManga = await jikan.GetManga(1);

			Assert.Equal("Monster", monsterManga.Title);
		}

		[Fact]
		public async Task GetManga_MonsterId_ShouldParseMonsterRelated()
		{
			Manga monsterManga = await jikan.GetManga(1);

			Assert.Single(monsterManga.Related.Adaptations);
			Assert.Single(monsterManga.Related.SideStories);
		}

		[Fact]
		public async Task GetManga_YotsubatoId_ShouldParseYotsubatoInformation()
		{
			Manga yotsubatoManga = await jikan.GetManga(104);

			Assert.Equal("Publishing", yotsubatoManga.Status);
			Assert.Equal(2003, yotsubatoManga.Published.From.Value.Year);
			Assert.Null(yotsubatoManga.Chapters);
			Assert.Null(yotsubatoManga.Volumes);
			Assert.Equal("Manga", yotsubatoManga.Type);
		}

		[Fact]
		public async Task GetManga_OnePieceId_ShouldParseOnePieceCollections()
		{
			Manga onePieceManga = await jikan.GetManga(13);

			Assert.Equal(1, onePieceManga.Authors.Count);
			Assert.Equal(1, onePieceManga.Serializations.Count);
			Assert.Equal(6, onePieceManga.Genres.Count);
			Assert.Equal("Oda, Eiichiro", onePieceManga.Authors.First().ToString());
			Assert.Equal("Shounen Jump (Weekly)", onePieceManga.Serializations.First().ToString());
			Assert.Equal("Action", onePieceManga.Genres.First().ToString());
		}

		[Fact]
		public async Task GetManga_MetallicaMetallucaId_ShouldParseMangaWithNoRelatedAdaptations()
		{
			Manga returnedManga = await jikan.GetManga(19983);

			Assert.Null(returnedManga.Related.Adaptations);
		}
	}
}