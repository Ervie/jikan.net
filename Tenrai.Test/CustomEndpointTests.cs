using FluentAssertions;
using Tenrai.Config;
using Tenrai.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests;

public class CustomEndpointTests
{
	[Fact]
	public async Task TenraiClientConstructor_WithHttpClient_ShouldNotParseCorrectly()
	{
		// Given
		var client = new HttpClient {BaseAddress = new Uri("https://api.jikan.moe/v4/")};
		var jikan = new TenraiClient(new TenraiClientConfiguration { SuppressException = false }, client);

		// When
		var bebop = await jikan.GetAnimeAsync(1);

		// Then
		bebop.Data.MalId.Should().Be(1);
	}
}