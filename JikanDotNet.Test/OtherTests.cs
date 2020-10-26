using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class OtherTests
	{
		private readonly IJikan _jikan;

		public OtherTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetAnime_CorrectId_ShouldParseBaseJikanRequest()
		{
			// When
			BaseJikanRequest request = await _jikan.GetAnimeAsync(1);

			// Then
			request.Should().NotBeNull();
		}

		[Fact]
		public async Task GetAnime_CorrectId_ShouldParseAnimeAsBaseJikanRequest()
		{
			// When
			Anime bebop = await _jikan.GetAnimeAsync(1);

			// Then
			bebop.RequestCacheExpiry.Should().BeLessThan(1000000);
		}

		[Fact]
		public async Task GetAnime_CorrectId_ShouldParseCacheExpiration()
		{
			// When
			// Random id
			int randomId = DateTime.Now.Second * DateTime.Now.Minute;
			BaseJikanRequest request = await _jikan.GetAnimeAsync(randomId);

			// Then
			request.Should().NotBeNull();

			if (!request.RequestCached)
			{
				request.RequestCacheExpiry.Should().Be(86400);
			}
			else
			{
				request.RequestCacheExpiry.Should().BeLessThan(86400);
			}
		}
	}
}