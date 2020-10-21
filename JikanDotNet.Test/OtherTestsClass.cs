using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class OtherTestsClass
	{
		private readonly IJikan _jikan;

		public OtherTestsClass()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetAnimeGetManga_CorrectId_ShouldUseIMalEntityInterface()
		{
			// Given
			IMalEntity berserk = await _jikan.GetManga(2);
			IMalEntity bebop = await _jikan.GetAnime(1);

			List<IMalEntity> entities = new List<IMalEntity>
			{
				berserk,
				bebop
			};

			// When
			var ids = entities.Select(x => x.MalId);

			// Then
			ids.Should().Contain(1);
		}

		[Fact]
		public async Task GetAnime_CorrectId_ShouldParseBaseJikanRequest()
		{
			// When
			BaseJikanRequest request = await _jikan.GetAnime(1);

			// Then
			request.Should().NotBeNull();
		}

		[Fact]
		public async Task GetAnime_CorrectId_ShouldParseAnimeAsBaseJikanRequest()
		{
			// When
			Anime bebop = await _jikan.GetAnime(1);

			// Then
			bebop.RequestCacheExpiry.Should().BeLessThan(1000000);
		}

		[Fact]
		public async Task GetAnime_CorrectId_ShouldParseCacheExpiration()
		{
			// When
			// Random id
			int randomId = DateTime.Now.Second * DateTime.Now.Minute;
			BaseJikanRequest request = await _jikan.GetAnime(randomId);

			// Then
			request.Should().NotBeNull();

			if (!request.RequestCached)
			{
				request.RequestCacheExpiry.Should().Be(43200);
			}
			else
			{
				request.RequestCacheExpiry.Should().BeLessThan(43200);
			}
		}
	}
}