using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class UserAnimeListTestClass
	{
		private readonly IJikan jikan;

		public UserAnimeListTestClass()
		{
			jikan = new Jikan();
		}

		[Fact]
		public async Task GetUserAnimeList_Ervelan_ShouldParseErvelanAnimeList()
		{
			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan");

			Assert.NotNull(animeList);
			Assert.Equal(300, animeList.Anime.Count);
			Assert.Contains("Akira", animeList.Anime.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanWatching_ShouldParseErvelanAnimeWatchingList()
		{
			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", UserAnimeListExtension.Watching);

			Assert.NotNull(animeList);
			Assert.Equal(UserAnimeListExtension.Watching, animeList.Anime.First().WatchingStatus);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanDropped_ShouldParseErvelanDroppedList()
		{
			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", UserAnimeListExtension.Dropped);

			Assert.NotNull(animeList);
			Assert.True(animeList.Anime.Count > 5);
			Assert.Contains("Coppelion", animeList.Anime.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSecondPage_ShouldParseErvelanAnimeListSecondPage()
		{
			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", 2);

			Assert.NotNull(animeList);
			Assert.Equal(300, animeList.Anime.Count);
		}

		[Fact]
		public async Task GetUserAnimeList_onrix_ShouldParseOnrixAnimeList()
		{
			UserAnimeList animeList = await jikan.GetUserAnimeList("onrix");

			Assert.NotNull(animeList);
			Assert.Equal(122, animeList.Anime.Count);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterWithDeath_ShouldFindDNandDP()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				Query = "death"
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.Contains("Death Note", animeList.Anime.Select(x => x.Title));
			Assert.Contains("Death Parade", animeList.Anime.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterWithAkira_ShouldFindSingleResult()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				Query = "Akira"
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.Single(animeList.Anime);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterWithQwerty_ShouldNotFindResults()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				Query = "qwerty"
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.Empty(animeList.Anime);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterWithEmptyString_ShouldReturnAllResults()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				Query = ""
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.True(animeList.Anime.Count == 300);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSortByTitle_ShouldFindZokuOwari()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				OrderBy = UserListAnimeSearchSortable.Title,
				SortDirection = SortDirection.Ascending
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.Equal("Zoku Owarimonogatari", animeList.Anime.First().Title);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSortByScore_ShouldFindLoGH()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				OrderBy = UserListAnimeSearchSortable.Score,
				SortDirection = SortDirection.Descending
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.Contains("Ginga", animeList.Anime.First().Title);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSortByScoreThenTitle_ShouldFindBaccanoOn3rdPlace()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				OrderBy = UserListAnimeSearchSortable.Score,
				OrderBy2 = UserListAnimeSearchSortable.Title,
				SortDirection = SortDirection.Descending
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.Equal("Baccano!", animeList.Anime.Skip(2).First().Title);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterByKyoAni_ShouldFindClannadAndVEG()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				ProducerId = 2
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.Contains("Clannad", animeList.Anime.Select(x => x.Title));
			Assert.Contains("Violet Evergarden", animeList.Anime.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterByWrongProducerId_ShouldReturnAllResults()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				ProducerId = -1
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.True(animeList.Anime.Count == 300);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterBySummer2010_ShouldFindFairyTailAndMaidSama()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				Year = 2010,
				Season = Seasons.Summer
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.Contains("Kaichou wa Maid-sama!", animeList.Anime.Select(x => x.Title));
			Assert.Contains("Fairy Tail", animeList.Anime.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterBy2010_ShouldFindBakemonogatariAndAngelBeats()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				Year = 2010
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.Contains("Angel Beats!", animeList.Anime.Select(x => x.Title));
			Assert.Contains("Bakemonogatari", animeList.Anime.Select(x => x.Title));
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterWithAngelBy2010_ShouldFindAngelBeats()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				Year = 2010,
				Query = "angel"
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.Contains("Angel Beats!", animeList.Anime.First().Title);
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterWithOneWithAiringStatus_ShouldFindOPAndNotOPM()
		{
			UserListAnimeSearchConfig searchConfig = new UserListAnimeSearchConfig()
			{
				AiringStatus = UserListAnimeAiringStatus.Airing,
				Query = "one"
			};

			UserAnimeList animeList = await jikan.GetUserAnimeList("Ervelan", searchConfig);

			Assert.NotNull(animeList);
			Assert.Contains("One Piece", animeList.Anime.Select(x => x.Title));
			Assert.DoesNotContain("One Punch-Man", animeList.Anime.Select(x => x.Title));
		}
	}
}
