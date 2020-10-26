using FluentAssertions;
using JikanDotNet.Config;
using JikanDotNet.Exceptions;
using System;
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
			var jikan = new Jikan(new JikanClientOptions { Endpoint = "https://api.jikan.moe/v4-alpha" } );

			// When
			var bebop = await jikan.GetAnime(1);

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
			Anime bebop = await jikan.GetAnime(1);

			// Then
			bebop.MalId.Should().Be(1);
		}

		[Fact]
		public void JikanConstructor_WrongUrl_ShouldNotParseCorrectly()
		{
			// When
			var jikan = new Jikan(new JikanClientOptions { Endpoint = "http://google.com" });

			// When
			Func<Task<Anime>> func = jikan.Awaiting(x => x.GetAnime(1));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void JikanConstructor_WrongUrlNoSurpress_ShouldThrowJikanException()
		{
			// When
			var jikan = new Jikan(new JikanClientOptions { Endpoint = "http://google.com", SuppressException = false });

			// When
			Func<Task<Anime>> func = jikan.Awaiting(x => x.GetAnime(1));

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
	}
}
