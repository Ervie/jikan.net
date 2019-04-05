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

		[Fact]
		public async Task SearchMethods_EmptyQuery_ShouldReturnNoResult()
		{
			var nullAnime = await jikan.SearchAnime("");
			var nullManga = await jikan.SearchManga("");
			var nullPerson = await jikan.SearchPerson("");
			var nullCharacter = await jikan.SearchCharacter("");
			
			Assert.Empty(nullAnime.Results);
			Assert.Empty(nullManga.Results);
			Assert.Empty(nullPerson.Results);
			Assert.Empty(nullCharacter.Results);
		}

		[Theory]
		[InlineData("berserk")]
		[InlineData("danganronpa")]
		[InlineData("death")]
		public async Task SearchAnime_NonEmptyQuery_ShouldReturnNotNullSearchAnime(string query)
		{
			AnimeSearchResult returnedAnime = await jikan.SearchAnime(query);

			Assert.NotNull(returnedAnime);
		}

		[Fact]
		public async Task SearchAnime_DanganronpaQuery_ShouldReturnDanganronpaAnime()
		{
			AnimeSearchResult danganronpaAnime = await jikan.SearchAnime("danganronpa");

			Assert.Equal(20, danganronpaAnime.ResultLastPage);
		}

		[Fact]
		public async Task SearchAnime_OnePieceAiringQuery_ShouldReturnAiringOnePieceAnime()
		{
			AnimeSearchConfig searchConfig = new AnimeSearchConfig()
			{
				Status = AiringStatus.Airing
			};

			AnimeSearchResult onePieceAnime = await jikan.SearchAnime("one p", searchConfig);

			Assert.Equal("One Piece", onePieceAnime.Results.First().Title);
		}

		[Fact]
		public async Task SearchAnime_HaibaneQuery_ShouldReturnHaibaneRenmeiAnime()
		{
			AnimeSearchResult haibaneRenmei = await jikan.SearchAnime("haibane");

			Assert.Equal("Haibane Renmei", haibaneRenmei.Results.First().Title);
			Assert.Equal("TV", haibaneRenmei.Results.First().Type);
			Assert.Equal(13, haibaneRenmei.Results.First().Episodes);
			Assert.Equal(387, haibaneRenmei.Results.First().MalId);
		}

		[Fact]
		public async Task SearchAnime_GirlQuerySecondPage_ShouldFindGirlAnime()
		{
			AnimeSearchResult returnedAnime = await jikan.SearchAnime("girl", 2);

			Assert.Contains("Jigoku Shoujo Futakomori", returnedAnime.Results.Select(x => x.Title));
		}

		[Theory]
		[InlineData("berserk")]
		[InlineData("monster")]
		[InlineData("death")]
		public async Task SearchManga_NonEmptyQuery_ShouldReturnNotNullSearchManga(string query)
		{
			MangaSearchResult returnedManga = await jikan.SearchManga(query);

			Assert.NotNull(returnedManga);
		}

		[Fact]
		public async Task SearchManga_DanganronpaQuery_ShouldReturnDanganronpaManga()
		{
			MangaSearchResult danganronpaManga = await jikan.SearchManga("danganronpa");

			Assert.Equal(20, danganronpaManga.ResultLastPage);
		}

		[Fact]
		public async Task SearchManga_YotsubatoQuery_ShouldReturnYotsubatoManga()
		{
			MangaSearchResult yotsubato = await jikan.SearchManga("yotsubato");

			Assert.Equal("Yotsuba to!", yotsubato.Results.First().Title);
			Assert.Equal("Manga", yotsubato.Results.First().Type);
			Assert.Equal(0, yotsubato.Results.First().Volumes);
			Assert.Equal(104, yotsubato.Results.First().MalId);
		}

		[Fact]
		public async Task SearchManga_YotsubatoPublishingQuery_ShouldReturnPublishedYotsubatoManga()
		{
			MangaSearchConfig searchConfig = new MangaSearchConfig()
			{
				Status = AiringStatus.Airing
			};

			MangaSearchResult yotsubato = await jikan.SearchManga("yotsubato", searchConfig);

			Assert.Equal("Yotsuba to!", yotsubato.Results.First().Title);
			Assert.Equal("Manga", yotsubato.Results.First().Type);
			Assert.Equal(0, yotsubato.Results.First().Volumes);
			Assert.Equal(104, yotsubato.Results.First().MalId);
		}

		[Fact]
		public async Task SearchManga_GirlQuerySecondPage_ShouldFindGirlManga()
		{
			MangaSearchResult returnedAnime = await jikan.SearchManga("girl", 2);

			Assert.Contains("My Girl", returnedAnime.Results.Select(x => x.Title));
			Assert.Equal(20, returnedAnime.ResultLastPage);
		}

		[Theory]
		[InlineData("araki")]
		[InlineData("oda")]
		[InlineData("sawashiro")]
		public async Task SearchPerson_NonEmptyQuery_ShouldReturnNotNullSearchPerson(string query)
		{
			PersonSearchResult returnedPerson = await jikan.SearchPerson(query);

			Assert.NotNull(returnedPerson);
		}

		[Fact]
		public async Task SearchPerson_MaayaSakamotoQuery_ShouldReturnSakamoto()
		{
			PersonSearchResult returnedPerson = await jikan.SearchPerson("maaya sakamoto");

			Assert.Single(returnedPerson.Results);
		}

		[Fact]
		public async Task SearchPerson_MaayaSakamotoQuery_ShouldReturnSakamotoName()
		{
			PersonSearchResult returnedPerson = await jikan.SearchPerson("maaya sakamoto");
			
			Assert.Equal("Maaya Sakamoto", returnedPerson.Results.First().Name);
		}

		[Fact]
		public async Task SearchPerson_MaayaSakamotoQuery_ShouldReturnSakamotoMalId()
		{
			PersonSearchResult returnedPerson = await jikan.SearchPerson("maaya sakamoto");
			
			Assert.Equal(90, returnedPerson.Results.First().MalId);
		}

		[Fact]
		public async Task SearchPerson_DaisukeQuerySecondPage_ShouldReturnDaisuke()
		{
			PersonSearchResult returnedPerson = await jikan.SearchPerson("daisuke", 2);

			Assert.Equal(50, returnedPerson.Results.Count);
			Assert.Contains("Daisuke", returnedPerson.Results.Select(x => x.Name));
		}

		[Theory]
		[InlineData("edward")]
		[InlineData("mai")]
		[InlineData("takeshi")]
		public async Task SearchCharacter_NonEmptyQuery_ShouldReturnNotNullSearchCharacter(string query)
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter(query);

			Assert.NotNull(returnedCharacter);
		}

		[Fact]
		public async Task SearchCharacter_LupinQuery_ShouldReturnLupin()
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter("lupin");

			Assert.Equal(2, returnedCharacter.ResultLastPage);
		}

		[Fact]
		public async Task SearchCharacter_LupinQuery_ShouldReturnLupinName()
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter("lupin iii");

			Assert.Equal("Lupin III, Arsene", returnedCharacter.Results.First().Name);
		}

		[Fact]
		public async Task SearchCharacter_LupinQuery_ShouldReturnLupinMalId()
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter("lupin iii");

			Assert.Equal(1044, returnedCharacter.Results.First().MalId);
		}

		[Fact]
		public async Task SearchCharacter_LambdadeltaQuery_ShouldReturnLambdadetla()
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter("lambdadelta");

			Assert.Equal(3, returnedCharacter.Results.Count());
			Assert.Equal("Lambdadelta", returnedCharacter.Results.First().Name);
			Assert.Equal("Umineko no Naku Koro ni", returnedCharacter.Results.First().Animeography.First().Name);
			Assert.Single(returnedCharacter.Results.First().Animeography);
		}

		[Fact]
		public async Task SearchCharacter_KirumiQuery_ShouldReturnKirumi()
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter("kirumi");

			Assert.Single(returnedCharacter.Results);
			Assert.Equal("Toujou, Kirumi", returnedCharacter.Results.First().Name);
			Assert.Single(returnedCharacter.Results.First().Mangaography);
		}

		[Fact]
		public async Task SearchCharacter_EdwardQuerySecondPage_ShouldFindEdwards()
		{
			CharacterSearchResult returnedCharacter = await jikan.SearchCharacter("edward", 2);
			
			Assert.Contains("Edward", returnedCharacter.Results.Select(x => x.Name));
			Assert.Equal(50, returnedCharacter.Results.Count);
		}
	}
}