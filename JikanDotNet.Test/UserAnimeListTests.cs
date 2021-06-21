using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class UserAnimeListTests
	{
		private readonly IJikan _jikan;

		public UserAnimeListTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserAnimeList_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			Func<Task<UserAnimeList>> func = _jikan.Awaiting(x => x.GetUserAnimeList(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserAnimeList_Ervelan_ShouldParseErvelanAnimeList()
		{
			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan");

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.Count.Should().Be(300);
				animeList.Anime.Select(x => x.Title).Should().Contain("Akira");
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserAnimeList_InvalidUsernameWithExtension_ShouldThrowValidationException(string username)
		{
			// When
			Func<Task<UserAnimeList>> func = _jikan.Awaiting(x => x.GetUserAnimeList(username, UserAnimeListExtension.Watching));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData((UserAnimeListExtension)int.MaxValue)]
		[InlineData((UserAnimeListExtension)int.MinValue)]
		public async Task GetUserAnimeList_ErvelanWithInvalidExtension_ShouldThrowValidationException(UserAnimeListExtension userAnimeListExtension)
		{
			// When
			Func<Task<UserAnimeList>> func = _jikan.Awaiting(x => x.GetUserAnimeList("Ervelan", userAnimeListExtension));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData((UserAnimeListExtension)int.MaxValue)]
		[InlineData((UserAnimeListExtension)int.MinValue)]
		public async Task GetUserAnimeList_ErvelanWithValidPageWithInvalidExtension_ShouldThrowValidationException(UserAnimeListExtension userAnimeListExtension)
		{
			// When
			Func<Task<UserAnimeList>> func = _jikan.Awaiting(x => x.GetUserAnimeList("Ervelan", userAnimeListExtension, 1));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanWatching_ShouldParseErvelanAnimeWatchingList()
		{
			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", UserAnimeListExtension.Watching);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.First().WatchingStatus.Should().Be(UserAnimeListExtension.Watching);
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanDropped_ShouldParseErvelanDroppedList()
		{
			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", UserAnimeListExtension.Dropped);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.Count.Should().BeGreaterThan(5);
				animeList.Anime.Select(x => x.Title).Should().Contain("Coppelion");
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanCompleted_ShouldParseErvelanAnimeCompletedListWithPage()
		{
			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", UserAnimeListExtension.Completed, 1);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.First().WatchingStatus.Should().Be(UserAnimeListExtension.Completed);
				animeList.Anime.First().AiringStatus.Should().Be(AiringStatus.Completed);
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserAnimeList_InvalidUsernameSecondPage_ShouldThrowValidationException(string username)
		{
			// When
			Func<Task<UserAnimeList>> func = _jikan.Awaiting(x => x.GetUserAnimeList(username, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}


		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetUserAnimeList_ValidUsernameInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<UserAnimeList>> func = _jikan.Awaiting(x => x.GetUserAnimeList("Ervelan", page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSecondPage_ShouldParseErvelanAnimeListSecondPage()
		{
			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", 2);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.Count.Should().Be(300);
			}
		}

		[Fact]
		public async Task GetUserAnimeList_onrix_ShouldParseOnrixAnimeList()
		{
			// When
			var animeList = await _jikan.GetUserAnimeList("onrix");

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.Count.Should().Be(122);
			}
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserAnimeList_InvalidUsernameWithConfig_ShouldThrowValidationException(string username)
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				Query = "death"
			};

			// When
			Func<Task<UserAnimeList>> func = _jikan.Awaiting(x => x.GetUserAnimeList(username, searchConfig));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserAnimeList_NullSearchConfig_ShouldFindDNandDP()
		{
			// When
			Func<Task<UserAnimeList>> func = _jikan.Awaiting(x => x.GetUserAnimeList("Ervelan", null));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterWithDeath_ShouldFindDNandDP()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				Query = "death"
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			var titles = animeList.Anime.Select(x => x.Title);
			using (new AssertionScope())
			{
				titles.Should().NotBeNull();
				titles.Should().Contain("Death Note");
				titles.Should().Contain("Death Parade");
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterWithAkira_ShouldFindSingleResult()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				Query = "Akira"
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.Should().ContainSingle();
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterWithQwerty_ShouldNotFindResults()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				Query = "qwerty"
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.Should().BeEmpty();
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterWithEmptyString_ShouldReturnAllResults()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				Query = ""
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.Should().HaveCount(300);
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSortByTitle_ShouldFindZokuOwari()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				OrderBy = UserListAnimeSearchSortable.Title,
				SortDirection = SortDirection.Ascending
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.First().Title.Should().Be("Zoku Owarimonogatari");
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSortByScore_ShouldFindLoGH()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				OrderBy = UserListAnimeSearchSortable.Score,
				SortDirection = SortDirection.Descending
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.First().Title.Should().Contain("Ginga");
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanSortByScoreThenTitle_ShouldFindBaccanoOn3rdPlace()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				OrderBy = UserListAnimeSearchSortable.Score,
				OrderBy2 = UserListAnimeSearchSortable.Title,
				SortDirection = SortDirection.Descending
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.Skip(2).First().Title.Should().Be("Baccano!");
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterByKyoAni_ShouldFindClannadAndVEG()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				ProducerId = 2
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			var titles = animeList.Anime.Select(x => x.Title);
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				titles.Should().Contain("Clannad");
				titles.Should().Contain("Violet Evergarden");
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterByWrongProducerId_ShouldReturnAllResults()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				ProducerId = -1
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.Should().HaveCount(300);
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterBySummer2010_ShouldFindFairyTailAndMaidSama()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				Year = 2010,
				Season = Seasons.Summer
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			var titles = animeList.Anime.Select(x => x.Title);
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				titles.Should().Contain("Kaichou wa Maid-sama!");
				titles.Should().Contain("Fairy Tail");
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterBy2010_ShouldFindBakemonogatariAndAngelBeats()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				Year = 2010
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			var titles = animeList.Anime.Select(x => x.Title);
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				titles.Should().Contain("Angel Beats!");
				titles.Should().Contain("Bakemonogatari");
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterWithAngelBy2010_ShouldFindAngelBeats()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				Year = 2010,
				Query = "angel"
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				animeList.Anime.First().Title.Should().Be("Angel Beats!");
			}
		}

		[Fact]
		public async Task GetUserAnimeList_ErvelanFilterWithOneWithAiringStatus_ShouldFindOPAndNotOPM()
		{
			// Given
			var searchConfig = new UserListAnimeSearchConfig()
			{
				AiringStatus = UserListAnimeAiringStatus.Airing,
				Query = "one"
			};

			// When
			var animeList = await _jikan.GetUserAnimeList("Ervelan", searchConfig);

			// Then
			var titles = animeList.Anime.Select(x => x.Title);
			using (new AssertionScope())
			{
				animeList.Should().NotBeNull();
				titles.Should().Contain("One Piece");
				titles.Should().NotContain("One Punch-Man");
			}
		}
	}
}