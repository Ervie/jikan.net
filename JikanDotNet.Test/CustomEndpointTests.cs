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
		private bool _isV4CustomeEndpointReady = false;

		[SkippableFact]
		public async Task JikanConstructor_DefaultUrl_ShouldParseCorrectly()
		{
			Skip.IfNot(_isV4CustomeEndpointReady);
			// Given
			var jikan = new Jikan(new JikanClientOptions { Endpoint = "https://api.jikan.moe/v4-alpha/" } );

			// When
			var bebop = await jikan.GetAnimeAsync(1);

			// Then
			bebop.MalId.Should().Be(1);
		}

		[Fact]
		public async Task JikanConstructor_CustomUrl_ShouldParseCorrectly()
		{
			Skip.IfNot(_isV4CustomeEndpointReady);
			// Given
			var jikan = new Jikan(new JikanClientOptions { Endpoint = "https://seiyuu.moe:8000/v4-alpha/" });

			// When
			Anime bebop = await jikan.GetAnimeAsync(1);

			// Then
			bebop.MalId.Should().Be(1);
		}

		[Fact]
		public void JikanConstructor_WrongUrl_ShouldNotParseCorrectly()
		{
			// When
			var jikan = new Jikan(new JikanClientOptions { Endpoint = "http://google.com" });

			// When
			Func<Task<Anime>> func = jikan.Awaiting(x => x.GetAnimeAsync(1));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void JikanConstructor_WrongUrlNoSurpress_ShouldThrowJikanException()
		{
			// When
			var jikan = new Jikan(new JikanClientOptions { Endpoint = "http://google.com", SuppressException = false });

			// When
			Func<Task<Anime>> func = jikan.Awaiting(x => x.GetAnimeAsync(1));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void JikanConstructor_NotAnUrl_ShouldNotParseCorrectly()
		{
			// When
			Func<Jikan> func = () => new Jikan(new JikanClientOptions { Endpoint = "Simple String", SuppressException = false });

			// Then
			func.Should().ThrowExactly<UriFormatException>();
		}

		[Fact]
		public void JikanConstructor_Empty_ShouldNotParseCorrectly()
		{
			// When
			Func<Jikan> func = () => new Jikan(new JikanClientOptions { Endpoint = string.Empty, SuppressException = false });

			// Then
			func.Should().ThrowExactly<UriFormatException>();
		}

		[Fact]
		public async Task JikanConstructorHttpClient_CorrectConfiguration_ShouldParseCorrectly()
		{
			// Given
			var httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://seiyuu.moe:8000/v3/")
			};
			var jikan = new Jikan(httpClient, false);

			// When
			Anime bebop = await jikan.GetAnime(1);

			// Then
			bebop.MalId.Should().Be(1);
		}

		[Fact]
		public void JikanConstructorHttpClient_IncorrectConfiguration_ShouldThrowJikanException()
		{
			// Given
			var httpClient = new HttpClient
			{
				BaseAddress = new Uri("https://google.com")
			};
			var jikan = new Jikan(httpClient, false);

			// When
			Func<Task<Anime>> func = jikan.Awaiting(x => x.GetAnime(1));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}
	}
}
