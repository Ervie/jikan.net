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
		public void ShouldReturnNotNullManga(long malId)
		{
			Manga returedManga = Task.Run(() => jikan.GetManga(malId)).Result;

			Assert.NotNull(returedManga);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(5)]
		[InlineData(6)]
		public void ShouldReturnNullMana(long malId)
		{
			Manga returedManga = Task.Run(() => jikan.GetManga(malId)).Result;

			Assert.Null(returedManga);
		}

		[Fact]
		public void ShouldParseBerserk()
		{
			Manga berserkManga = Task.Run(() => jikan.GetManga(2)).Result;

			Assert.Equal("Berserk", berserkManga.Title);
		}

		[Fact]
		public void ShouldParseMonster()
		{
			Manga monsterManga = Task.Run(() => jikan.GetManga(1)).Result;

			Assert.Equal("Monster", monsterManga.Title);
		}

		[Fact]
		public void ShouldParseYotsubatoInformation()
		{
			Manga yotsubatoManga = Task.Run(() => jikan.GetManga(104)).Result;

			Assert.Equal("Publishing", yotsubatoManga.Status);
			Assert.Equal("Mar  21, 2003 to ?", yotsubatoManga.PublishedString);
			Assert.Equal("Unknown", yotsubatoManga.Chapters);
			Assert.Equal("Unknown", yotsubatoManga.Volumes);
			Assert.Equal("Manga", yotsubatoManga.Type);
		}

		[Fact]
		public void ShouldParseOnePieceCollections()
		{
			Manga onePieceManga = Task.Run(() => jikan.GetManga(13)).Result;

			Assert.Equal(1, onePieceManga.Authors.Count);
			Assert.Equal(1, onePieceManga.Serializations.Count);
			Assert.Equal(6, onePieceManga.Genres.Count);
			Assert.Equal("Oda, Eiichiro", onePieceManga.Authors.First().ToString());
			Assert.Equal("Shounen Jump (Weekly)", onePieceManga.Serializations.First().ToString());
			Assert.Equal("Action", onePieceManga.Genres.First().ToString());
		}
	}
}