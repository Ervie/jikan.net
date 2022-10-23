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