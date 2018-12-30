using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MagazineTestClass
	{
		private readonly IJikan jikan;

		public MagazineTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public void ShouldParseBigComicOriginal()
		{
			Magazine magazine = Task.Run(() => jikan.GetMagazine(1)).Result;

			Assert.NotNull(magazine);
			Assert.Equal("Big Comic Original", magazine.Metadata.Name);
			Assert.Equal(1, magazine.Metadata.MalId);
			Assert.Equal(1, magazine.MalId);
			Assert.Equal("Monster", magazine.Manga.First().Title);
			Assert.Contains("Pluto", magazine.Manga.Select(x => x.Title));
		}

		[Fact]
		public void ShouldParseYoungAnimal()
		{
			Magazine magazine = Task.Run(() => jikan.GetMagazine(2)).Result;

			Assert.NotNull(magazine);
			Assert.Equal("Young Animal", magazine.Metadata.Name);
			Assert.Equal("Berserk", magazine.Manga.First().Title);
			Assert.True(magazine.Manga.First().Members > 200000);
			Assert.Contains("3-gatsu no Lion", magazine.Manga.Select(x => x.Title));
		}

		[Fact]
		public void ShouldParseYoungAnimalSecondPage()
		{
			Magazine magazine = Task.Run(() => jikan.GetMagazine(2, 2)).Result;

			Assert.NotNull(magazine);
			Assert.True(magazine.Manga.Count > 10);
		}

		[Fact]
		public void ShouldParseShonenJump()
		{
			Magazine magazine = Task.Run(() => jikan.GetMagazine(83)).Result;

			Assert.NotNull(magazine);
			Assert.Equal("Shounen Jump (Weekly)", magazine.Metadata.Name);
			Assert.Equal(83, magazine.Metadata.MalId);
			Assert.Equal("Naruto", magazine.Manga.First().Title);
			Assert.True(magazine.Manga.First().Members > 200000);
			Assert.Equal("One Piece", magazine.Manga.Skip(1).First().Title);
			Assert.Contains("Shaman King", magazine.Manga.Select(x => x.Title));
		}
	}
}