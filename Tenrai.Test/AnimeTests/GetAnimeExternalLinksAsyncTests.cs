using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.AnimeTests;

[Collection("TenraiTests")]
public class GetAnimeExternalLinksAsyncTests
{

	private readonly ITenrai _tenrai;

	public GetAnimeExternalLinksAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetAnimeExternalLinksAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeExternalLinksAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetAnimeExternalLinksAsync_BebopId_ShouldReturnBebopLinks()
	{
		// When
		var links = await _tenrai.GetAnimeExternalLinksAsync(1);

		// Then
		links.Data.Should().Contain(x => x.Name.Equals("Wikipedia") && x.Url.Equals("http://en.wikipedia.org/wiki/Cowboy_Bebop"));
	}
}