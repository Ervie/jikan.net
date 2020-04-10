using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
    public class TopTestClass
    {
		private readonly IJikan jikan;

		public TopTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public async Task GetAnimeTop_NoParameter_ShouldParseTopAnime()
		{
			AnimeTop top = await jikan.GetAnimeTop();

			Assert.NotNull(top);
		}

		[Fact]
		public async Task GetAnimeTop_NoParameter_ShouldParseFMA()
		{
			AnimeTop top = await jikan.GetAnimeTop();

			Assert.Equal("Fullmetal Alchemist: Brotherhood", top.Top.First().Title);
		}

		[Fact]
		public async Task GetAnimeTop_NoParameter_ShouldParseFMAEpisodes()
		{
			AnimeTop top = await jikan.GetAnimeTop();

			Assert.Equal(64, top.Top.First().Episodes);
		}

		[Fact]
		public async Task GetAnimeTop_NoParameter_ShouldParseLOGHType()
		{
			AnimeTop top = await jikan.GetAnimeTop();

			Assert.Equal("OVA", top.Top.Skip(4).First().Type);
		}

		[Fact]
		public async Task GetAnimeTop_FirstPageMovies_ShouldParseKimiNoNaWaMovieList()
		{
			AnimeTop top = await jikan.GetAnimeTop(1, TopAnimeExtension.TopMovies);

			Assert.Equal("Kimi no Na wa.", top.Top.First().Title);
		}

		[Fact]
		public async Task GetAnimeTop_FirstPagePopularity_ShouldParseDeathNotePopularityList()
		{
			AnimeTop top = await jikan.GetAnimeTop(1, TopAnimeExtension.TopPopularity);

			Assert.Equal("Death Note", top.Top.First().Title);
		}

		[Fact]
		public async Task GetAnimeTop_FirstPageOva_ShouldParseLOGHOvaList()
		{
			AnimeTop top = await jikan.GetAnimeTop(1, TopAnimeExtension.TopOva);

			Assert.Equal("Ginga Eiyuu Densetsu", top.Top.First().Title);
		}

		[Fact]
		public async Task GetAnimeTop_FirstPageMovies_ShouldParseLOGHOAiringStartOvaList()
		{
			AnimeTop top = await jikan.GetAnimeTop(1, TopAnimeExtension.TopOva);

			Assert.Equal("Jan 1988", top.Top.First().AiringStart);
		}

		[Fact]
		public async Task GetAnimeTop_SecondPage_ShouldParseAnimeSecondPage()
		{
			AnimeTop top = await jikan.GetAnimeTop(2);

			Assert.Contains("Ping Pong the Animation", top.Top.Select(x => x.Title));
			Assert.Contains("Yojouhan Shinwa Taikei", top.Top.Select(x => x.Title));
		}


		[Fact]
		public async Task GetMangaTop_NoParameter_ShouldParseTopManga()
		{
			MangaTop top = await jikan.GetMangaTop();

			Assert.NotNull(top);
		}

		[Fact]
		public async Task GetMangaTop_NoParameter_ShouldParseBerserk()
		{
			MangaTop top = await jikan.GetMangaTop();

			Assert.Equal("Berserk", top.Top.First().Title);
		}

		[Fact]
		public async Task GetMangaTop_NoParameter_ShouldParseBerserkStartDate()
		{
			MangaTop top = await jikan.GetMangaTop();

			Assert.Equal("Aug 1989", top.Top.First().PublishingStart);
		}

		[Fact]
		public async Task GetMangaTop_FirstPageNovels_ShouldParseMonogatariTitle()
		{
			MangaTop top = await jikan.GetMangaTop(1, TopMangaExtension.TopNovel);

			Assert.Equal("Monogatari Series: First Season", top.Top.First().Title);
		}

		[Fact]
		public async Task GetMangaTop_FirstPagePopularity_ShouldParseNarutoPopularity()
		{
			MangaTop top = await jikan.GetMangaTop(1, TopMangaExtension.TopPopularity);

			Assert.Equal("Naruto", top.Top.First().Title);
		}

		[Fact]
		public async Task GetMangaTop_FirstPagePopularity_ShouldParseNarutoTypePopularity()
		{
			MangaTop top = await jikan.GetMangaTop(1, TopMangaExtension.TopPopularity);

			Assert.Equal("Manga", top.Top.First().Type);
		}

		[Fact]
		public async Task GetPeopleTop_NoParameters_ShouldParseKanaHanazawa()
		{
			PeopleTop top = await jikan.GetPeopleTop();

			Assert.Equal("Hanazawa, Kana", top.Top.First().Name);
			Assert.Equal("花澤 香菜", top.Top.First().NameKanji);
			Assert.Equal(185, top.Top.First().MalId);
			Assert.Equal(1989, top.Top.First().Birthday.Value.Year);
			Assert.True(top.Top.First().Favorites > 60000);
		}

		[Fact]
		public async Task GetPeopleTop_NoParameters_ShouldParseHiroshiKamiya()
		{
			PeopleTop top = await jikan.GetPeopleTop();

			Assert.Equal("Kamiya, Hiroshi", top.Top.Skip(1).First().Name);
			Assert.Equal("神谷 浩史", top.Top.Skip(1).First().NameKanji);
			Assert.Equal(118, top.Top.Skip(1).First().MalId);
			Assert.Equal(1975, top.Top.Skip(1).First().Birthday.Value.Year);
			Assert.True(top.Top.Skip(1).First().Favorites > 50000);
		}

		[Fact]
		public async Task GetPeopleTop_SecondPage_ShouldFindKentarouMiura()
		{
			PeopleTop top = await jikan.GetPeopleTop(2);

			Assert.Contains("Miura, Kentarou", top.Top.Select(x => x.Name));
		}

		[Fact]
		public async Task GetCharactersTop_NoParameters_ShouldParseLelouchLamperouge()
		{
			CharactersTop top = await jikan.GetCharactersTop();

			Assert.Equal("Lamperouge, Lelouch", top.Top.First().Name);
			Assert.Equal("ルルーシュ・ランペルージ", top.Top.First().NameKanji);
			Assert.Equal(417, top.Top.First().MalId);
			Assert.True(top.Top.First().Favorites > 85000);
		}

		[Fact]
		public async Task GetCharactersTop_NoParameters_ShouldParseLLawliet()
		{
			CharactersTop top = await jikan.GetCharactersTop();

			Assert.Equal("Lawliet, L", top.Top.Skip(1).First().Name);
			Assert.Equal("エル ローライト", top.Top.Skip(1).First().NameKanji);
			Assert.Equal(71, top.Top.Skip(1).First().MalId);
			Assert.True(top.Top.Skip(1).First().Favorites > 65000);
		}

		[Fact]
		public async Task GetCharactersTop_NoParameters_ShouldParseLuffyAnimeography()
		{
			CharactersTop top = await jikan.GetCharactersTop();

			Assert.Equal("Monkey D., Luffy", top.Top.Skip(2).First().Name);
			Assert.Contains("One Piece", top.Top.Skip(2).First().Animeography.Select(x => x.Name));
		}

		[Fact]
		public async Task GetCharactersTop_SecondPage_ShouldFindTachibanaKanade()
		{
			CharactersTop top = await jikan.GetCharactersTop(2);

			Assert.Contains("Tachibana, Kanade", top.Top.Select(x => x.Name));
		}

	}
}
