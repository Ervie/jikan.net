using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class OtherTestsClass
	{
		private readonly IJikan jikan;

		public OtherTestsClass()
		{
			jikan = new Jikan();
		}

		[Fact]
		public async Task GetAnimeGetManga_CorrectId_ShouldUseIMalEntityInterface()
		{
			IMalEntity berserk = await jikan.GetManga(2);
			IMalEntity bebop = await jikan.GetAnime(1);

			List<IMalEntity> entities = new List<IMalEntity>();
			entities.Add(berserk);
			entities.Add(bebop);

			Assert.Contains(1, entities.Select(x => x.MalId));
		}

		[Fact]
		public async Task GetAnime_CorrectId_ShouldParseBaseJikanRequest()
		{
			BaseJikanRequest request = await jikan.GetAnime(1);

			Assert.NotNull(request);
		}

		[Fact]
		public async Task GetAnime_CorrectId_ShouldParseAnimeAsBaseJikanRequest()
		{
			Anime bebop = await jikan.GetAnime(1);

			Assert.True(bebop.RequestCacheExpiry < 43201);
		}

		[Fact]
		public async Task GetAnime_CorrectId_ShouldParseCacheExpiration()
		{
			// Random id
			int randomId = DateTime.Now.Second * DateTime.Now.Minute;
			BaseJikanRequest request = await jikan.GetAnime(randomId);

			Assert.NotNull(request);

			if (!request.RequestCached)
			{
				Assert.Equal(43200, request.RequestCacheExpiry);
			}
			else
			{
				Assert.True(request.RequestCacheExpiry < 43200);
			}
		}
	}
}