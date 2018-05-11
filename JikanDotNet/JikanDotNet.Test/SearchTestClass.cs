using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class SearchTestClass
	{
		private readonly IJikan jikan;

		public SearchTestClass()
		{
			jikan = new Jikan(true);
		}

		[Theory]
		[InlineData("berserk")]
		[InlineData("danganronpa")]
		[InlineData("death")]
		public void ShouldReturnNotNullSearchAnime(string query)
		{
			AnimeSearchResult returnedAnime = Task.Run(() => jikan.SearchAnime(query)).Result;

			Assert.NotNull(returnedAnime);
		}

		[Fact]
		public void ShouldReturnDanganronpaAnime()
		{
			AnimeSearchResult danganronpaAnime = Task.Run(() => jikan.SearchAnime("danganronpa")).Result;

			Assert.Equal(20, danganronpaAnime.ResultLastPage);
		}

		[Fact]
		public void ShouldReturnHaibaneRenmeiAnime()
		{
			AnimeSearchResult haibaneRenmei = Task.Run(() => jikan.SearchAnime("haibane")).Result;

			Assert.Equal("Haibane Renmei", haibaneRenmei.Results.First().Title);
			Assert.Equal("TV", haibaneRenmei.Results.First().Type);
			Assert.Equal(13, haibaneRenmei.Results.First().Episodes);
			Assert.Equal(387, haibaneRenmei.Results.First().MalId);
		}

		[Theory]
		[InlineData("berserk")]
		[InlineData("monster")]
		[InlineData("death")]
		public void ShouldReturnNotNullSearchManga(string query)
		{
			MangaSearchResult returnedManga = Task.Run(() => jikan.SearchManga(query)).Result;

			Assert.NotNull(returnedManga);
		}

		[Fact]
		public void ShouldReturnDanganronpaManga()
		{
			MangaSearchResult danganronpaManga = Task.Run(() => jikan.SearchManga("danganronpa")).Result;

			Assert.Equal(20, danganronpaManga.ResultLastPage);
		}

		[Fact]
		public void ShouldReturnYotsubatoManga()
		{
			MangaSearchResult yotsubato = Task.Run(() => jikan.SearchManga("yotsubato")).Result;

			Assert.Equal("Yotsuba to!", yotsubato.Results.First().Title);
			Assert.Equal("Manga", yotsubato.Results.First().Type);
			Assert.Equal(0, yotsubato.Results.First().Volumes);
			Assert.Equal(104, yotsubato.Results.First().MalId);
		}

		[Theory]
		[InlineData("araki")]
		[InlineData("oda")]
		[InlineData("sawashiro")]
		public void ShouldReturnNotNullSearchPerson(string query)
		{
			PersonSearchResult returnedPerson = Task.Run(() => jikan.SearchPerson(query)).Result;

			Assert.NotNull(returnedPerson);
		}

		[Fact]
		public void ShouldReturnSakamoto()
		{
			PersonSearchResult returnedPerson = Task.Run(() => jikan.SearchPerson("maaya sakamoto")).Result;

			Assert.Equal(1, returnedPerson.Results.Count);
		}

		[Fact]
		public void ShouldReturnSakamotoName()
		{
			PersonSearchResult returnedPerson = Task.Run(() => jikan.SearchPerson("maaya sakamoto")).Result;
			
			Assert.Equal("Maaya Sakamoto", returnedPerson.Results.First().Name);
		}
		[Fact]
		public void ShouldReturnSakamotoMalId()
		{
			PersonSearchResult returnedPerson = Task.Run(() => jikan.SearchPerson("maaya sakamoto")).Result;
			
			Assert.Equal(90, returnedPerson.Results.First().MalId);
		}

		[Theory]
		[InlineData("edward")]
		[InlineData("mai")]
		[InlineData("takeshi")]
		public void ShouldReturnNotNullSearchCharacter(string query)
		{
			CharacterSearchResult returnedCharacter = Task.Run(() => jikan.SearchCharacter(query)).Result;

			Assert.NotNull(returnedCharacter);
		}

		[Fact]
		public void ShouldReturnLupin()
		{
			CharacterSearchResult returnedCharacter = Task.Run(() => jikan.SearchCharacter("lupin")).Result;

			Assert.Equal(2, returnedCharacter.ResultLastPage);
		}

		[Fact]
		public void ShouldReturnLupinName()
		{
			CharacterSearchResult returnedCharacter = Task.Run(() => jikan.SearchCharacter("lupin iii")).Result;

			Assert.Equal("Lupin III, Arsene", returnedCharacter.Results.First().Name);
		}

		[Fact]
		public void ShouldReturnLupinMalId()
		{
			CharacterSearchResult returnedCharacter = Task.Run(() => jikan.SearchCharacter("lupin iii")).Result;

			Assert.Equal(1044, returnedCharacter.Results.First().MalId);
		}

		[Fact]
		public void ShouldReturnLambdadetla()
		{
			CharacterSearchResult returnedCharacter = Task.Run(() => jikan.SearchCharacter("lambdadelta")).Result;

			Assert.Equal(3, returnedCharacter.Results.Count());
			Assert.Equal("Lambdadelta", returnedCharacter.Results.First().Name);
			Assert.Equal("Umineko no Naku Koro ni", returnedCharacter.Results.First().Animeography.First().Title);
			Assert.Equal(1, returnedCharacter.Results.First().Animeography.Count);
		}
	}
}