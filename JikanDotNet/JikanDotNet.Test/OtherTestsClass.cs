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
		public void ShouldUseIMalEntityInterface()
		{
			IMalEntity berserk = Task.Run(() => jikan.GetManga(2)).Result;
			IMalEntity bebop = Task.Run(() => jikan.GetAnime(1)).Result;

			List<IMalEntity> entities = new List<IMalEntity>();
			entities.Add(berserk);
			entities.Add(bebop);

			Assert.Contains(1, entities.Select(x => x.MalId));
		}

		[Fact]
		public void ShouldParseBaseJikanRequest()
		{
			BaseJikanRequest request = Task.Run(() => jikan.GetAnime(1)).Result;

			Assert.NotNull(request);
		}

		[Fact]
		public void ShouldParseAnimeAsBaseJikanRequest()
		{
			Anime bebop = Task.Run(() => jikan.GetAnime(1)).Result;

			Assert.True(bebop.RequestCacheExpiry < 43201);
		}

		[Fact]
		public void ShouldParseCacheExpiration()
		{
			// Random id
			int randomId = DateTime.Now.Second * DateTime.Now.Minute;
			BaseJikanRequest request = Task.Run(() => jikan.GetAnime(randomId)).Result;

			Assert.NotNull(request);

			if (!request.RequestCached)
			{
				request = Task.Run(() => jikan.GetAnime(randomId)).Result;
			}
			
			Assert.True(request.RequestCached);
		}
	}
}