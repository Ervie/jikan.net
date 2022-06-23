using FluentAssertions;
using JikanDotNet.Config;
using JikanDotNet.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class CustomEndpointTests
	{
		private readonly bool _isV4CustomEndpointReady = false;

		[SkippableFact]
		public async Task JikanConstructor_DefaultUrl_ShouldParseCorrectly()
		{
			// Given
			var jikan = new Jikan(new JikanClientConfiguration { Endpoint = "https://api.jikan.moe/v4/" });

			// When
			var bebop = await jikan.GetAnimeAsync(1);

			// Then
			bebop.Data.MalId.Should().Be(1);
		}

		[SkippableFact]
		public async Task JikanConstructor_CustomUrl_ShouldParseCorrectly()
		{
			Skip.IfNot(_isV4CustomEndpointReady);
			// Given
			var jikan = new Jikan(new JikanClientConfiguration { Endpoint = "https://seiyuu.moe:8000/v4-alpha/" });

			// When
			var bebop = await jikan.GetAnimeAsync(1);

			// Then
			bebop.Data.MalId.Should().Be(1);
		}

		[Fact]
		public void JikanConstructor_WrongUrl_ShouldNotParseCorrectly()
		{
			// When
			var jikan = new Jikan(new JikanClientConfiguration { Endpoint = "http://google.com" });

			// When
			Func<Task<BaseJikanResponse<Anime>>> func = jikan.Awaiting(x => x.GetAnimeAsync(1));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void JikanConstructor_WrongUrlNoSurpress_ShouldThrowJikanException()
		{
			// When
			var jikan = new Jikan(new JikanClientConfiguration { Endpoint = "http://google.com", SuppressException = false });

			// When
			Func<Task<BaseJikanResponse<Anime>>> func = jikan.Awaiting(x => x.GetAnimeAsync(1));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void JikanConstructor_NotAnUrl_ShouldNotParseCorrectly()
		{
			// When
			Func<Jikan> func = () => new Jikan(new JikanClientConfiguration { Endpoint = "Simple String", SuppressException = false });

			// Then
			func.Should().ThrowExactly<UriFormatException>();
		}
		
		
		[Fact]
		public async Task JikanConstructor_WithHttpClient_ShouldNotParseCorrectly()
		{
			// Given
			var client = new HttpClient {BaseAddress = new Uri("https://api.jikan.moe/v4/")};
			var jikan = new Jikan(new JikanClientConfiguration { SuppressException = false }, client);
			
			// When
			var bebop = await jikan.GetAnimeAsync(1);

			// Then
			bebop.Data.MalId.Should().Be(1);
		}
	}
}